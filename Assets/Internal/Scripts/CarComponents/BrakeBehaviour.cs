using UnityEngine;

namespace Cars_5_5.CarComponents
{
    public class BrakeBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _maxBrakeTorque;
        [SerializeField]
        private float _handBrakeTorgue;

        public float MaxBrakeTorque => _maxBrakeTorque;
        public float HandBrakeTorgue => _handBrakeTorgue;

        public float GetBrakeTorgue(float brakePosition, int axleCount)
        {
            return (_maxBrakeTorque * brakePosition) / axleCount;
        }

        public void SetMaxTorgue(float value)
        {
            _maxBrakeTorque = value;
        }
    }
}
