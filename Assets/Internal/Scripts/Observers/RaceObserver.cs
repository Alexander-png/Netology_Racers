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
        [SerializeField]
        private int _laps;

        private Dictionary<CarObserver, int> _lapsPassedByCar = new Dictionary<CarObserver, int>();
        private RaceUIBehaviour _raceUIBehaviour;

        private void Start()
        {
            Initialize();

            if (_raceUIBehaviour != null)
            {
                _raceUIBehaviour.StartCountDownElapsed += OnStartCountDownElapsed;
            }
            RacePreStart();
        }

        private void Initialize()
        {
            _raceUIBehaviour = GetComponent<RaceUIBehaviour>();

            _lapsPassedByCar = new Dictionary<CarObserver, int>();
            _driveableCarsOnMap = FindObjectsOfType<BaseCarInput>();
            Array.ForEach(_driveableCarsOnMap, car => _lapsPassedByCar.Add(car.CarObserver, 0));
        }

        public void RacePreStart()
        {
            SetCarsHandlingEnabled(false);
            _raceUIBehaviour.SetLapCount(_laps);
            _raceUIBehaviour.RacePreStart();
        }

        public void OnCarReachedStartLine(BaseCarInput driveableCar)
        {
            if (driveableCar is PlayerInputHandler)
            {
                _raceUIBehaviour.OnLapPassedByPlayer();
            }

            if (IsCarFinishedRace(driveableCar.CarObserver))
            {
                if (driveableCar is PlayerInputHandler)
                {
                    OnPlayerFinished();
                }
                driveableCar.WheelBehaviour.InputEnabled = false;
            }
            else
            {
                _lapsPassedByCar[driveableCar.CarObserver] += 1;
            }
        }

        private bool IsCarFinishedRace(CarObserver car)
        {
            return _lapsPassedByCar[car] == _laps;
        }

        private void OnStartCountDownElapsed(object sender, EventArgs e)
        {
            SetCarsHandlingEnabled(true);
        }

        private void OnPlayerFinished()
        {
            _raceUIBehaviour.OnPlayerFinished();
        }

        private void SetCarsHandlingEnabled(bool value)
        {
            Array.ForEach(_driveableCarsOnMap, car => car.WheelBehaviour.InputEnabled = value);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Initialize();
        }
#endif
    }
}
