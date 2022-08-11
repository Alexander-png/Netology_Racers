using System.Collections.Generic;
using UnityEngine;
using Cars_4_4.CarComponents.Assistance;

namespace Cars_4_4.CarComponents
{
    public class WheelBehaviour : MonoBehaviour
    {
        [SerializeField]
        private EngineBehaviour _engine;
        [SerializeField]
        private SteeringBehaviour _steering;
        [SerializeField]
        private BrakeBehaviour _brakes;
        [SerializeField]
        private List<AxleInfo> _axleInfos;

        private int _axleCount = 0;

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

        public float AcceleratorPosition
        {
            get;
            set;
        }

        public float BrakeAxis
        {
            get;
            set;
        }

        public bool OnHandBrake
        {
            get;
            set;
        }

        public float SteeringAxis
        {
            get => _steering.Axis;
            set
            {

                _steering.Axis = value;
            }
        }

        public void FixedUpdate()
        {
            MovementLogic();
        }

        private void MovementLogic()
        {
            float torgue = _engine.GetMotorTorgue(AcceleratorPosition, _axleCount);
            float brake = _brakes.GetBrakeTorgue(BrakeAxis, _axleCount);
            float steering = _steering.CurrentSteeringAngle;

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
                        info.LeftWheel.brakeTorque = _brakes.HandBrakeTorgue;
                        info.RightWheel.brakeTorque = _brakes.HandBrakeTorgue;
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

#if UNITY_EDITOR
        private void OnValidate()
        {
            _engine = GetComponent<EngineBehaviour>();
            _brakes = GetComponent<BrakeBehaviour>();
            _steering = GetComponent<SteeringBehaviour>();
        }
#endif
    }
}

