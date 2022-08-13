using Cars_5_5.CarComponents.Assistance;
using Cars_5_5.Input;
using Cars_5_5.Input.Base;
using Cars_5_5.UI.RaceUI;
using Cars_5_5.UI.RaceUI.Managing;
using UnityEngine;

namespace Cars_5_5.Observers
{
    public class RaceObserver : MonoBehaviour
    {
        [SerializeField]
        private CarObserver[] _carsOnMap;

        private RaceUIPresenter _raceUIPresenter;

        public void OnCarReachedStartLine(BaseCarInput driveableCar)
        {
            if (driveableCar is PlayerInputHandler)
            {
                _raceUIPresenter.OnLapPassedByPlayer();
            }
            else if (driveableCar is BotInputHandler)
            {

            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _carsOnMap = FindObjectsOfType<CarObserver>();
            _raceUIPresenter = GetComponent<RaceUIPresenter>();
        }
#endif
    }
}
