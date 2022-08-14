using Cars_5_5.BotAssistance;
using Cars_5_5.CarComponents.Assistance;
using Cars_5_5.Input;
using Cars_5_5.Input.Base;
using Cars_5_5.UI.RaceUI.Managing;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_5_5.Observers
{
    public interface IRaceObserver
    {
        void OnStartCountDownElapsed(object sender, EventArgs e);
        void OnRaceRestart(object sender, EventArgs e);
    }

    public struct CarStartPosition
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }

    public class RaceObserver : MonoBehaviour, IRaceObserver
    {
        [SerializeField]
        private BaseCarInput[] _driveableCarsOnMap;
        [SerializeField]
        private int _laps;
        [SerializeField]
        private WaypointComponent _firstWaypoint;

        private Dictionary<CarObserver, int> _lapsPassedByCar = new Dictionary<CarObserver, int>();
        private Dictionary<CarObserver, CarStartPosition> _carStartPositions = new Dictionary<CarObserver, CarStartPosition>();
        private RaceUIBehaviour _raceUIBehaviour;

        private void Start()
        {
            Initialize();

            RacePreStart();
        }

        private void Initialize()
        {
            _raceUIBehaviour = GetComponent<RaceUIBehaviour>();
            _raceUIBehaviour.SetRaceObserver(this);
            FindCarsOnTrack();
            FindFirstWaypoint();
            GiveFirstWaypointToBots();
        }

        private void FindCarsOnTrack()
        {
            _driveableCarsOnMap = FindObjectsOfType<BaseCarInput>();

            _lapsPassedByCar = new Dictionary<CarObserver, int>();
            _carStartPositions = new Dictionary<CarObserver, CarStartPosition>();

            foreach (BaseCarInput car in _driveableCarsOnMap)
            {
                _lapsPassedByCar.Add(car.CarObserver, 0);
                _carStartPositions.Add(car.CarObserver, new CarStartPosition() { Position = car.transform.position, Rotation = car.transform.rotation });
            }
        }

        private void FindFirstWaypoint()
        {
            WaypointComponent[] waypointsOnMap = FindObjectsOfType<WaypointComponent>();
            foreach(var waypoint in waypointsOnMap)
            {
                if (waypoint.IsFirst)
                {
                    _firstWaypoint = waypoint;
                    break;
                }
            }
        }

        private void GiveFirstWaypointToBots()
        {
            foreach (BaseCarInput car in _driveableCarsOnMap)
            {
                if (car is BotInputHandler bot)
                {
                    bot.SetFirstWaypoint(_firstWaypoint);
                }
            }

        }

        public void OnRaceRestart(object sender, EventArgs e)
        {
            RacePreStart();
            StopAllCars();
            ResetLapsPassedCounters();
            MoveCarsToStartPositions();
        }

        public void RacePreStart()
        {
            SetCarsHandlingEnabled(false);
            _raceUIBehaviour.SetLapCount(_laps);
            _raceUIBehaviour.RacePreStart();
        }

        private void StopAllCars()
        {
            foreach (BaseCarInput car in _driveableCarsOnMap)
            {
                car.CarObserver.CarBody.StopImmediately();
            }
        }

        private void ResetLapsPassedCounters()
        {
            foreach (BaseCarInput car in _driveableCarsOnMap)
            {
                _lapsPassedByCar[car.CarObserver] = 0;
            }
        }

        private void MoveCarsToStartPositions()
        {
            foreach (BaseCarInput car in _driveableCarsOnMap)
            {
                CarStartPosition startPosition = _carStartPositions[car.CarObserver];
                car.transform.position = startPosition.Position;
                car.transform.transform.rotation = startPosition.Rotation;
            }
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

        public void OnStartCountDownElapsed(object sender, EventArgs e)
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
