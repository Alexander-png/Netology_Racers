using UnityEngine;

namespace Cars_5_5.UI.RaceUI.Managing
{
    public class RaceUIPresenter : MonoBehaviour
    {
        private TimeComponent _timer;

        private int _lapsPassed = -1;

        private void Start()
        {
            if (_timer != null)
            {
                _timer.StartLapTimer();
            }
        }

        public void OnLapPassedByPlayer()
        {
            if (_lapsPassed >= 0)
            {
                _timer.OnLapPassed();
            }
            _lapsPassed++;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _timer = FindObjectOfType<TimeComponent>();
        }
#endif
    }
}
