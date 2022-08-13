using Cars_5_5.UI.Base;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Cars_5_5.UI.RaceUI
{
    public class StartRaceCountDown : BaseUIElement
    {
        private TMP_Text _countDownText;

        [SerializeField]
        private int _startCountDownValue = 3;
        [SerializeField]
        private float _keepTextAfterCountDownTime = 1.5f;

        public EventHandler Elapsed;

        private void OnDisable()
        {
            Elapsed = null;
        }

        public void StartCountDown()
        {
            gameObject.SetActive(true);
            StartCoroutine(CountDownCoroutine());
        }

        private IEnumerator CountDownCoroutine()
        {
            _countDownText.enabled = true;
            int _currentCountDownValue = _startCountDownValue;
            while (_currentCountDownValue > 0)
            {
                _countDownText.text = _currentCountDownValue.ToString();
                yield return new WaitForSeconds(1f);
                _currentCountDownValue--;
            }
            _countDownText.text = "GO!";
            Elapsed?.Invoke(this, EventArgs.Empty);

            StartCoroutine(KeepCountDownTextAfterElapsed());
        }

        private IEnumerator KeepCountDownTextAfterElapsed()
        {
            yield return new WaitForSeconds(_keepTextAfterCountDownTime);
            _countDownText.enabled = false;
        }

        public override void SetVisible(bool value)
        {
            _countDownText.enabled = value;
            if (!value)
            {
                Elapsed = null;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _countDownText = GetComponent<TMP_Text>();
        }
#endif
    }
}
