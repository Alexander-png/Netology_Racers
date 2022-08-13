using UnityEngine;

namespace Cars_5_5.CarComponents.Assistance
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider LeftWheel;
        public WheelCollider RightWheel;
        public bool IsMotor;
        public bool HasBrake;
        public bool HasHandbrake;
        public bool IsSteering;
    }
}
