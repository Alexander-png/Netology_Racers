using UnityEngine;

namespace Cars_5_5.CarComponents
{
    public class EngineBehaviour : MonoBehaviour
    {
        [SerializeField, Space]
        private float _motorTorque;

        public float MotorTorque => _motorTorque;

        public float GetMotorTorgue(float acceleratiorPosition, int axleCount)
        {
            return (_motorTorque * acceleratiorPosition) / axleCount;
        }
    }
}
