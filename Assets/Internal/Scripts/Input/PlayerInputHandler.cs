using Cars_5_5.CarComponents;
using Cars_5_5.Input.Base;
using System;
using UnityEngine.InputSystem;

namespace Cars_5_5.Input
{
    public class PlayerInputHandler : BaseCarInput
    {
        //private void FixedUpdate()
        //{
        //    UnityEngine.Debug.Log(CarObserver.SignedCarSpeed);
        //}

        private void OnAcceleration(InputValue value)
        {
            WheelBehaviour.VerticalAxis = value.Get<float>();
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
