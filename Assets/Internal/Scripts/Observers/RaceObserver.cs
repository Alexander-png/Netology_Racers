using Cars_5_5.CarComponents.Assistance;
using Cars_5_5.Input;
using Cars_5_5.Input.Base;
using Cars_5_5.UI.RaceUI.Managing;
using System;
using UnityEngine;

namespace Cars_5_5.Observers
{
    public class RaceObserver : MonoBehaviour
    {
        [SerializeField]
        private BaseCarInput[] _driveableCarsOnMap;

        private RaceUIBehaviour _raceUIBehaviour;

        private void Start()
        {
            if (_raceUIBehaviour != null)
            {
                _raceUIBehaviour.RaceStarted += OnRaceStarted; 
            }
            RacePreStart();
        }

        public void RacePreStart()
        {
            EnableCarsHandling(false);
            _raceUIBehaviour.RacePreStart();
        }

        public void OnCarReachedStartLine(BaseCarInput driveableCar)
        {
            if (driveableCar is PlayerInputHandler)
            {
                _raceUIBehaviour.OnLapPassedByPlayer();
            }
            else if (driveableCar is BotInputHandler)
            {

            }
        }

        private void OnRaceStarted(object sender, EventArgs e)
        {
            EnableCarsHandling(true);
        }

        private void EnableCarsHandling(bool value)
        {
            Array.ForEach(_driveableCarsOnMap, car => car.InputEnabled = value);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _driveableCarsOnMap = FindObjectsOfType<BaseCarInput>();
            _raceUIBehaviour = GetComponent<RaceUIBehaviour>();
        }
#endif
    }
}
