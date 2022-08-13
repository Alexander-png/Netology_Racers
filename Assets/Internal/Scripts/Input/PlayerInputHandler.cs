using Cars_5_5.CarComponents;
using Cars_5_5.Input.Base;
using System;
using UnityEngine.InputSystem;

namespace Cars_5_5.Input
{
    public class PlayerInputHandler : BaseCarInput
    {
        private float _verticalAxis;

        private void OnAcceleration(InputValue value)
        {
            _verticalAxis = value.Get<float>();
        }

        private void OnSteering(InputValue value)
        {
            WheelBehaviour.SteeringAxis = value.Get<float>();
        }

        private void OnHandbrake(InputValue value)
        {
            WheelBehaviour.OnHandBrake = Convert.ToBoolean(value.Get<float>());
        }

        private void FixedUpdate()
        {
            AcceleratorLogic();
        }

        private void AcceleratorLogic()
        {
            if (_verticalAxis != 0)
            {
                float currentSpeed = CarObserver.AbsoluteCarSpeed;
                WheelBehaviour.AcceleratorPosition = GetRealAcceleratorPosition(_verticalAxis, currentSpeed);
                WheelBehaviour.BrakeAxis = GetRealBrakeAxisPosition(_verticalAxis, currentSpeed);
            }
            else
            {
                WheelBehaviour.AcceleratorPosition = 0;
                WheelBehaviour.BrakeAxis = 0;
            }
        }
    }
}
