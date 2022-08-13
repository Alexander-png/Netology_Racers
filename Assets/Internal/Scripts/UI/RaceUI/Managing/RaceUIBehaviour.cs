using Cars_5_5.UI.Base;
using System;
using UnityEngine;

namespace Cars_5_5.UI.RaceUI.Managing
{
    public class RaceUIBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool _startRaceImmediate = false;

        private TimeComponent _raceTimer;
        private StartRaceInviter _raceInviter;
        private StartRaceCountDown _countDown;
        private SpeedometerComponent _speedometer;
        private LapCounterComponent _lapCounter;

        public EventHandler RaceStarted;
        public EventHandler RaceEnded;

        public void RacePreStart()
        {
            if (_startRaceImmediate)
            {
                SetUIElementVisible(_raceInviter, false);
                SetUIElementVisible(_countDown, false);
                if (SetUIElementVisible(_lapCounter, false))
                {
                    _lapCounter.AllLapsPassed += OnRaceEnded;
                }
                OnRaceStarted(null, null);
            }
            else
            {
                if (SetUIElementVisible(_raceInviter, true))
                {
                    _raceInviter.StartAccept += OnRaceStartAccept;
                }
                if (SetUIElementVisible(_countDown, false))
                {
                    _countDown.Elapsed += OnRaceStarted;
                }
                if (SetUIElementVisible(_lapCounter, false))
                {
                    _lapCounter.AllLapsPassed += OnRaceEnded;
                }
                SetUIElementVisible(_raceTimer, false);
                SetUIElementVisible(_speedometer, false);
            }
        }

        private void OnDisable()
        {
            if (_raceInviter != null)
            {
                _raceInviter.StartAccept -= OnRaceStartAccept;
            }
            if (_countDown != null)
            {
                _countDown.Elapsed -= OnRaceStarted;
            }
            if (_lapCounter != null)
            {
                _lapCounter.AllLapsPassed -= OnRaceEnded;
            }
            RaceStarted = null;
            RaceEnded = null;
        }

        public void OnLapPassedByPlayer()
        {
            _lapCounter.IncreaseLapCount();
            if (_lapCounter.PassedLapsCount > 1)
            {
                _raceTimer.OnLapPassed();
            }
        }

        private void OnRaceStartAccept(object sender, EventArgs e)
        {
            SetUIElementVisible(_raceInviter, false);
            _countDown.StartCountDown();
        }

        private void OnRaceStarted(object sender, EventArgs e)
        {
            RaceStarted?.Invoke(this, EventArgs.Empty);
            if (SetUIElementVisible(_raceTimer, true))
            {
                _raceTimer.StartLapTimer();
            }
            if (SetUIElementVisible(_lapCounter, true))
            {
                _lapCounter.OnRaceStarted();
            }
            SetUIElementVisible(_speedometer, true);
        }

        private void OnRaceEnded(object sender, EventArgs e)
        {
            _raceTimer.StopLapTimer();
            RaceEnded?.Invoke(this, EventArgs.Empty);
        }

        private bool SetUIElementVisible(BaseUIElement element, bool value)
        {
            if (element != null)
            {
                element.SetVisible(value);
            }
            return element != null;
        }

        public int GetLapCount() => _lapCounter?.LapsCount ?? 0;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _raceTimer = FindObjectOfType<TimeComponent>();
            _raceInviter = FindObjectOfType<StartRaceInviter>();
            _countDown = FindObjectOfType<StartRaceCountDown>();
            _speedometer = FindObjectOfType<SpeedometerComponent>();
            _lapCounter = FindObjectOfType<LapCounterComponent>();
        }
#endif
    }
}
