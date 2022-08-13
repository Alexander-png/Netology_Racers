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

        private int _axleCount = 0;

        private float _acceleratorPosition;
        private float _brakePosition;

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
            float torgue = _carObserver.CarEngine.GetMotorTorgue(_acceleratorPosition, _axleCount);
            float brake = _carObserver.CarBrakes.GetBrakeTorgue(_brakePosition, _axleCount);
            float steering = _carObserver.CarSteering.CurrentSteeringAngle;

            foreach (AxleInfo info in _axleInfos)
            {
                if (info.IsSteering)
                {
                    info.LeftWheel.steerAngle = steering;
                    info.RightWheel.steerAngle = steering;
                }
                if (info.IsMotor)
                {
                    info.LeftWheel.motorTorque = torgue;
                    info.RightWheel.motorTorque = torgue;
                }
                if (info.HasBrake)
                {
                    info.LeftWheel.brakeTorque = brake;
                    info.RightWheel.brakeTorque = brake;
                }
                if (info.HasHandbrake)
                {
                    if (OnHandBrake)
                    {
                        info.LeftWheel.brakeTorque = _carObserver.CarBrakes.HandBrakeTorgue;
                        info.RightWheel.brakeTorque = _carObserver.CarBrakes.HandBrakeTorgue;
                    }
                    else if (info.HasBrake)
                    {
                        info.LeftWheel.brakeTorque = brake;
                        info.RightWheel.brakeTorque = brake;
                    }
                    else
                    {
                        info.LeftWheel.brakeTorque = 0;
                        info.RightWheel.brakeTorque = 0;
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

