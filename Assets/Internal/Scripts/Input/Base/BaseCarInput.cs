using Cars_4_4.CarComponents;
using Cars_4_4.CarComponents.Assistance;
using UnityEngine;

namespace Cars_4_4.Input.Base
{
    public abstract class BaseCarInput : MonoBehaviour
    {
        [SerializeField]
        private float acceleratorToBrakeSwitchThreshold = 0.5f;

        private CarObserver _carObserver;
        public CarObserver CarObserver => _carObserver;

        public WheelBehaviour WheelBehaviour => _carObserver.CarWheels;

        protected float GetRealAcceleratorPosition(float axis, float currentSpeed)
        {
            if (CarObserver.SignedCarSpeed > 0)
            {
                if (axis > 0)
                {
                    return axis;
                }
                else if (currentSpeed <= acceleratorToBrakeSwitchThreshold && axis < 0)
                {
                    return axis;
                }
            }
            else
            {
                if (axis < 0)
                {
                    return axis;
                }
                else if (currentSpeed <= acceleratorToBrakeSwitchThreshold && axis > 0)
                {
                    return axis;
                }
            }
            return 0;
        }

        protected float GetRealBrakeAxisPosition(float axis, float currentSpeed)
        {
            if (CarObserver.SignedCarSpeed > 0)
            {
                if (currentSpeed >= acceleratorToBrakeSwitchThreshold && axis < 0)
                {
                    return -axis;
                }
            }
            else
            {
                if (currentSpeed >= acceleratorToBrakeSwitchThreshold && axis > 0)
                {
                    return axis;
                }
            }
            return 0;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _carObserver = GetComponent<CarObserver>();
        }
#endif
    }
}
