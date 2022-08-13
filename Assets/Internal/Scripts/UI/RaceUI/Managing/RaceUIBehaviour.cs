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

        private int _lapsPassed = -1;

        public EventHandler RaceStarted;

        public void RacePreStart()
        {
            if (_startRaceImmediate)
            {
                SetUIElementVisible(_raceInviter, false);
                SetUIElementVisible(_countDown, false);
                OnStartCoundownElapsed(null, null);
            }
            else
            {
                if (SetUIElementVisible(_raceInviter, true))
                {
                    _raceInviter.StartAccept += OnRaceStartAccept;
                }
                if (SetUIElementVisible(_countDown, false))
                {
                    _countDown.Elapsed += OnStartCoundownElapsed;
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
                _countDown.Elapsed -= OnStartCoundownElapsed;
            }
            RaceStarted = null;
        }

        public void OnLapPassedByPlayer()
        {
            if (_lapsPassed >= 0)
            {
                _raceTimer.OnLapPassed();
            }
            _lapsPassed++;
        }

        private void OnRaceStartAccept(object sender, EventArgs e)
        {
            SetUIElementVisible(_raceInviter, false);
            _countDown.StartCountDown();
        }

        private void OnStartCoundownElapsed(object sender, EventArgs e)
        {
            RaceStarted?.Invoke(this, EventArgs.Empty);
            if (_raceTimer != null)
            {
                _raceTimer.StartLapTimer();
            }
            SetUIElementVisible(_speedometer, true);
        }

        private bool SetUIElementVisible(BaseUIElement element, bool value)
        {
            if (element != null)
            {
                element.SetVisible(value);
            }
            return element != null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _raceTimer = FindObjectOfType<TimeComponent>();
            _raceInviter = FindObjectOfType<StartRaceInviter>();
            _countDown = FindObjectOfType<StartRaceCountDown>();
            _speedometer = FindObjectOfType<SpeedometerComponent>();
        }
#endif
    }
}
