using Cars_5_5.CarComponents.Assistance;
using Cars_5_5.Input;
using Cars_5_5.Input.Base;
using Cars_5_5.UI.RaceUI.Managing;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_5_5.Observers
{
    public class RaceObserver : MonoBehaviour
    {
        [SerializeField]
        private BaseCarInput[] _driveableCarsOnMap;

        private Dictionary<CarObserver, int> _lapsPassedByCar = new Dictionary<CarObserver, int>();

        private int _lapsToPass;

        private RaceUIBehaviour _raceUIBehaviour;

        private void Start()
        {
            if (_raceUIBehaviour != null)
            {
                _raceUIBehaviour.RaceStarted += OnRaceStarted;
                _raceUIBehaviour.RaceEnded += OnRaceEnded;
            }
            RacePreStart();
        }

        public void RacePreStart()
        {
            EnableCarsHandling(false);
            _raceUIBehaviour.RacePreStart();
            _lapsToPass = _raceUIBehaviour.GetLapCount();
        }

        public void OnCarReachedStartLine(BaseCarInput driveableCar)
        {
            if (driveableCar is PlayerInputHandler)
            {
                _raceUIBehaviour.OnLapPassedByPlayer();
            }

            if (IsCarFinishedRace(driveableCar.CarObserver))
            {
                driveableCar.WheelBehaviour.InputEnabled = false;
            }
            else
            {
                _lapsPassedByCar[driveableCar.CarObserver] += 1;
            }
        }

        private bool IsCarFinishedRace(CarObserver car)
        {
            return _lapsPassedByCar[car] == _lapsToPass;
        }

        private void OnRaceStarted(object sender, EventArgs e)
        {
            EnableCarsHandling(true);
        }

        private void OnRaceEnded(object sender, EventArgs e)
        {
            
        }

        private void EnableCarsHandling(bool value)
        {
            Array.ForEach(_driveableCarsOnMap, car => car.WheelBehaviour.InputEnabled = value);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _driveableCarsOnMap = FindObjectsOfType<BaseCarInput>();
            Array.ForEach(_driveableCarsOnMap, car => _lapsPassedByCar.Add(car.CarObserver, 0));
            _raceUIBehaviour = GetComponent<RaceUIBehaviour>();
        }
#endif
    }
}
