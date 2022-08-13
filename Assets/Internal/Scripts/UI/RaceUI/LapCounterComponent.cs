using Cars_5_5.UI.Base;
using System;
using TMPro;
using UnityEngine;

namespace Cars_5_5.UI.RaceUI
{
    public class LapCounterComponent : BaseUIElement
    {
        [SerializeField]
        private TMP_Text _lapsPassedCountText;
        [SerializeField]
        private TMP_Text _lapsToPassCountText;

        private int _laps;
        private int _currentLap = 0;

        public int LapsCount => _laps;
        public int CurrentLap => _currentLap;

        public void OnRaceStarted()
        {
            _currentLap = 0;
            _lapsPassedCountText.text = "1";
        }

        public override void SetVisible(bool value)
        {
            gameObject.SetActive(value);
        }

        public void IncreaseLapCount()
        {
            _currentLap++;
            _lapsPassedCountText.text = _currentLap.ToString();
        }

        public void SetLapCount(int value)
        {
            _laps = value;
            _lapsToPassCountText.text = _laps.ToString();
        }
    }
}
