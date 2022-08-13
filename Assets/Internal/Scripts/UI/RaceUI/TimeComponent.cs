using Cars_5_5.UI.Base;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Cars_5_5.UI.RaceUI
{
    public class TimeComponent : BaseUIElement
    {
        [SerializeField]
        private string _timeFormat = @"mm\:ss\:fff";

        [SerializeField]
        private TMP_Text _bestTimeText;
        [SerializeField]
        private TMP_Text _lapTimeText;
        
        private Coroutine _updateTimerCoroutine;

        private TimeSpan _originTime;
        private TimeSpan _bestTime = TimeSpan.MinValue;
        private TimeSpan _lapTime;

        public TimeSpan LapTime => _lapTime;
        public TimeSpan BestTime => _bestTime;

        public void StartLapTimer()
        {
            _originTime = DateTime.Now.TimeOfDay;
            _bestTimeText.text = TimeSpan.Zero.ToString(_timeFormat);
            _updateTimerCoroutine = StartCoroutine(UpdateTimerValueCoroutine());
        }

        public void StopLapTimer()
        {
            StopCoroutine(_updateTimerCoroutine);
        }

        public override void SetVisible(bool value)
        {
            _bestTimeText.transform.parent.gameObject.SetActive(value);
            _lapTimeText.transform.parent.gameObject.SetActive(value);
        }

        public void OnLapPassed()
        {
            if (_bestTime == TimeSpan.MinValue || IsLapTimeBetter())
            {
                _bestTime = _lapTime;
                _bestTimeText.text = _bestTime.ToString(_timeFormat);
            }
            ResetLapTimer();
        }

        private bool IsLapTimeBetter()
        {            
            return _lapTime.Ticks < _bestTime.Ticks;
        }

        private void ResetLapTimer()
        {
            _originTime = DateTime.Now.TimeOfDay;
        }

        private void OnDisable()
        {
            if (_updateTimerCoroutine != null)
            {
                StopCoroutine(_updateTimerCoroutine);
            }
        }

        private IEnumerator UpdateTimerValueCoroutine()
        {
            while (true)
            {
                _lapTime = DateTime.Now.TimeOfDay - _originTime;
                _lapTimeText.text = _lapTime.ToString(_timeFormat);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
