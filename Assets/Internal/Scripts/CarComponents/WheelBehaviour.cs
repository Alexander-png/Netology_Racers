using System.Collections.Generic;
using UnityEngine;
using Cars_5_5.CarComponents.Assistance;

namespace Cars_5_5.CarComponents
{
    public class WheelBehaviour : MonoBehaviour
    {
        [SerializeField]
        private CarObserver _carObserver;
        [SerializeField]
        private float _acceleratorToBrakeSwitchThreshold = 0.5f;
        [SerializeField]
        private List<AxleInfo> _axleInfos;
        [SerializeField]
        private bool _inputEnabled = true;

        private int _axleCount = 0;

        private float _acceleratorPosition;
        private float _brakePosition;
        

        public bool InputEnabled
        {
            get => _inputEnabled;
            set => _inputEnabled = value;
        }

        private void Start()
        {
            foreach (var axle in _axleInfos)
            {
                if (axle.IsMotor)
                {
                    _axleCount++;
                }
            }
        }

        public float VerticalAxis
        {
            get;
            set;
        }

        public float SteeringAxis
        {
            get => _carObserver.CarSteering.Axis;
            set
            {
                _carObserver.CarSteering.Axis = value;
            }
        }

        public bool OnHandBrake
        {
            get;
            set;
        }

        public void FixedUpdate()
        {
            AcceleratorLogic();
            MovementLogic();
        }

        private void AcceleratorLogic()
        {
            if (VerticalAxis != 0)
            {
                float currentSpeed = _carObserver.AbsoluteCarSpeed;
                _acceleratorPosition = GetRealAcceleratorPosition(VerticalAxis, currentSpeed);
                _brakePosition = GetRealBrakeAxisPosition(VerticalAxis, currentSpeed);
            }
            else
            {
                _acceleratorPosition = 0;
                _brakePosition = 0;
            }
        }

        private void MovementLogic()
        {
            float torgue;
            float brake;
            float steering = _carObserver.CarSteering.CurrentSteeringAngle;
            if (InputEnabled)
            {
                torgue = _carObserver.CarEngine.GetMotorTorgue(_acceleratorPosition, _axleCount);
                brake = _carObserver.CarBrakes.GetBrakeTorgue(_brakePosition, _axleCount);
            }
            else
            {
                torgue = 0;
                brake = 0;
            }

            foreach (AxleInfo axle in _axleInfos)
            {
                if (axle.IsSteering)
                {
                    axle.LeftWheel.steerAngle = steering;
                    axle.RightWheel.steerAngle = steering;
                }
                if (axle.IsMotor)
                {
                    axle.LeftWheel.motorTorque = torgue;
                    axle.RightWheel.motorTorque = torgue;
                }
                if (axle.HasBrake)
                {
                    axle.LeftWheel.brakeTorque = brake;
                    axle.RightWheel.brakeTorque = brake;
                }
                if (axle.HasHandbrake)
                {
                    if (OnHandBrake)
                    {
                        axle.LeftWheel.brakeTorque = _carObserver.CarBrakes.HandBrakeTorgue;
                        axle.RightWheel.brakeTorque = _carObserver.CarBrakes.HandBrakeTorgue;
                    }
                    else if (axle.HasBrake)
                    {
                        axle.LeftWheel.brakeTorque = brake;
                        axle.RightWheel.brakeTorque = brake;
                    }
                    else
                    {
                        axle.LeftWheel.brakeTorque = 0;
                        axle.RightWheel.brakeTorque = 0;
                    }
                }
            }
        }

        protected float GetRealAcceleratorPosition(float axis, float currentSpeed)
        {
            if (_carObserver.SignedCarSpeed > 0)
            {
                if (axis > 0)
                {
                    return axis;
                }
                else if (currentSpeed <= _acceleratorToBrakeSwitchThreshold && axis < 0)
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
                else if (currentSpeed <= _acceleratorToBrakeSwitchThreshold && axis > 0)
                {
                    return axis;
                }
            }
            return 0;
        }

        protected float GetRealBrakeAxisPosition(float axis, float currentSpeed)
        {
            if (_carObserver.SignedCarSpeed > 0)
            {
                if (currentSpeed >= _acceleratorToBrakeSwitchThreshold && axis < 0)
                {
                    return -axis;
                }
            }
            else
            {
                if (currentSpeed >= _acceleratorToBrakeSwitchThreshold && axis > 0)
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

