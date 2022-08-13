using Cars_5_5.CarComponents;
using Cars_5_5.CarComponents.Assistance;
using UnityEngine;

namespace Cars_5_5.Input.Base
{
    public abstract class BaseCarInput : MonoBehaviour
    {
        [SerializeField]
        private float acceleratorToBrakeSwitchThreshold = 0.5f;
        [SerializeField]
        private bool _inputEnabled = true;

        private CarObserver _carObserver;
        public CarObserver CarObserver => _carObserver;
        public WheelBehaviour WheelBehaviour => _carObserver.CarWheels;

        public bool InputEnabled
        {
            get => _inputEnabled;
            set
            {
                _inputEnabled = value;
            }
        }

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
