using Cars_5_5.UI.Base;
using System;
using TMPro;
using UnityEngine;

namespace Cars_5_5.UI.RaceUI
{
    public class LapCounterComponent : BaseUIElement
    {
        [SerializeField]
        private int _laps;
        [SerializeField]
        private TMP_Text _lapsPassedCountText;
        [SerializeField]
        private TMP_Text _lapsToPassCountText;

        private int _passedLapsCount = 0;
        
        public EventHandler AllLapsPassed;

        public int LapsCount => _laps;
        public int PassedLapsCount => _passedLapsCount;

        public void OnRaceStarted()
        {
            _lapsPassedCountText.text = "1";
            _lapsToPassCountText.text = _laps.ToString();
        }

        public override void SetVisible(bool value)
        {
            gameObject.SetActive(value);
        }

        public void IncreaseLapCount()
        {
            if (_passedLapsCount == _laps)
            {
                AllLapsPassed?.Invoke(this, EventArgs.Empty);
                return;
            }
            _passedLapsCount++;
            _lapsPassedCountText.text = _passedLapsCount.ToString();
        }

        private void OnDisable()
        {
            AllLapsPassed = null;
        }
    }
}
