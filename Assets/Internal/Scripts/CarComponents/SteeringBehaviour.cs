using UnityEngine;

namespace Cars_5_5.CarComponents
{
    public class SteeringBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _steeringSensivity;
        [SerializeField]
        private float _maxSteeringAngle;
        [SerializeField]
        private float _baseSteeringReturnSpeed;

        public float MaxSteeringAngle => _maxSteeringAngle;

        public float CurrentSteeringAngle { get; private set; }
        public float Axis { get; set; }

        private void FixedUpdate()
        {
            UpdateSteeringAngle();
        }

        private void UpdateSteeringAngle()
        {
            float fixedTime = Time.fixedDeltaTime;
            if (Axis == 0f && CurrentSteeringAngle != 0f)
            {
                CurrentSteeringAngle += (CurrentSteeringAngle > 0f ? -fixedTime : fixedTime) * _baseSteeringReturnSpeed;
            }
            else
            {
                float targetSteeringAngle = _maxSteeringAngle * Axis;
                if (Mathf.Abs(CurrentSteeringAngle - targetSteeringAngle) > 0.001f)
                {
                    if (Mathf.Abs(targetSteeringAngle) < Mathf.Abs(CurrentSteeringAngle))
                    {
                        CurrentSteeringAngle += (CurrentSteeringAngle > 0f ? -fixedTime : fixedTime) * _baseSteeringReturnSpeed;
                    }
                    else
                    {
                        CurrentSteeringAngle = Mathf.Clamp(CurrentSteeringAngle + Axis * (_steeringSensivity * fixedTime), -_maxSteeringAngle, _maxSteeringAngle);
                    }
                }  
            }
        }

        public void SetMaxAngle(float value)
        {
            _maxSteeringAngle = value;
        }
    }
}
