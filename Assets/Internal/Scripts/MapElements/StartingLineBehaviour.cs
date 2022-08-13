using Cars_5_5.Input.Base;
using Cars_5_5.Observers;
using UnityEngine;

namespace Cars_5_5.MapElements
{
    public class StartingLineBehaviour : MonoBehaviour
    {
        private RaceObserver _raceObserver;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BaseCarInput car))
            {
                _raceObserver.OnCarReachedStartLine(car);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _raceObserver = FindObjectOfType<RaceObserver>();
        }
#endif
    }
}
