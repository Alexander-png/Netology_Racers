using Cars_4_4.CarComponents;
using Cars_4_4.Input.Base;
using System;
using UnityEngine.InputSystem;

namespace Cars_4_4.Input
{
    public class PlayerInputHandler : BaseCarInput
    {
        private float _verticalAxis;

        private void OnAcceleration(InputValue value)
        {
            if (value != null)
            {
                _verticalAxis = value.Get<float>();
            }
            else
            {
                _verticalAxis = 0;
            }
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

        private void OnSteering(InputValue value)
        {
            WheelBehaviour.SteeringAxis = value.Get<float>();
        }

        private void OnHandbrake(InputValue value)
        {
            WheelBehaviour.OnHandBrake = Convert.ToBoolean(value.Get<float>());
        }
    }
}
