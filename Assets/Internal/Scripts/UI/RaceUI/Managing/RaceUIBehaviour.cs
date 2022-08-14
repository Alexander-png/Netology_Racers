using Cars_5_5.Observers;
using Cars_5_5.UI.Base;
using Cars_5_5.UI.RaceUI.LeaderTableElements;
using System;
using UnityEngine;

namespace Cars_5_5.UI.RaceUI.Managing
{
    public class RaceUIBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool _startRaceImmediate = false;

        [SerializeField, Space(15)]
        private TimeComponent _raceTimer;
        [SerializeField]
        private StartRaceInviter _raceInviter;
        [SerializeField]
        private StartRaceCountDown _countDown;
        [SerializeField]
        private SpeedometerComponent _speedometer;
        [SerializeField]
        private LapCounterComponent _lapCounter;
        [SerializeField]
        private LeaderTable _leaderBoard;
        [SerializeField]
        private AddRecordDialog _addRecordDialog;

        private IRaceObserver _raceObserver;

        public void SetRaceObserver(IRaceObserver observer)
        {
            _raceObserver = observer;
        }

        public void RacePreStart()
        {
            if (_startRaceImmediate)
            {
                SetUIElementVisible(_raceInviter, false);
                SetUIElementVisible(_countDown, false);
                SetUIElementVisible(_addRecordDialog, false);
                SetUIElementVisible(_leaderBoard, false);
                SetUIElementVisible(_lapCounter, false);

                OnCountDownElapsed(null, null);
            }
            else
            {
                SetUIElementVisible(_raceInviter, true);
                SetUIElementVisible(_countDown, false);
                SetUIElementVisible(_lapCounter, false);
                SetUIElementVisible(_raceTimer, false);
                SetUIElementVisible(_speedometer, false);
                SetUIElementVisible(_addRecordDialog, false);
                SetUIElementVisible(_leaderBoard, false);

                _raceInviter.StartAccept += OnRaceStartAccept;
            }
        }

        private void OnDisable()
        {
            _raceInviter.StartAccept -= OnRaceStartAccept;
            _countDown.Elapsed -= OnCountDownElapsed;
        }

        public void OnLapPassedByPlayer()
        {
            _lapCounter.IncreaseLapCount();
            if (_lapCounter.CurrentLap > 1)
            {
                _raceTimer.OnLapPassed();
            }
        }

        private void OnRaceStartAccept(object sender, EventArgs e)
        {
            SetUIElementVisible(_raceInviter, false);

            _countDown.Elapsed += OnCountDownElapsed;
            _countDown.StartCountDown();
        }

        private void OnCountDownElapsed(object sender, EventArgs e)
        {
            _raceObserver.OnStartCountDownElapsed(this, EventArgs.Empty);

            SetRaceUIState(true);
        }

        public void OnPlayerFinished()
        {
            SetRaceUIState(false);
            ShowAddRecordDialog();
        }

        private void ShowAddRecordDialog()
        {
            SetUIElementVisible(_addRecordDialog, true);
            _addRecordDialog.RecordAdded += OnRecordAdded;
            _addRecordDialog.BestTimeLap = _raceTimer.BestTime;
        }

        private void OnRecordAdded(object sender, EventArgs e)
        {
            SetUIElementVisible(_addRecordDialog, false);

            SetUIElementVisible(_leaderBoard, true);
            _leaderBoard.OnRaceRestartSelectedInMenu += _raceObserver.OnRaceRestart;
        }

        private void SetRaceUIState(bool inRace)
        {
            SetUIElementVisible(_raceTimer, inRace);
            SetUIElementVisible(_lapCounter, inRace);
            SetUIElementVisible(_speedometer, inRace);

            if (inRace)
            {
                _raceTimer.StartLapTimer();
                _lapCounter.ResetCounter();
            }
            else
            {
                _raceTimer.StopLapTimer();
            }
        }

        private void SetUIElementVisible(BaseUIElement element, bool value)
        {
            if (element != null)
            {
                element.SetVisible(value);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("UI element is null");
#endif
            }
        }

        public void SetLapCount(int value) => _lapCounter.SetLapCount(value);
    }
}
