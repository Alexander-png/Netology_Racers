using UnityEngine;

namespace Cars_5_5.CarComponents.Assistance
{
    public class CarObserver : MonoBehaviour
    {
        private EngineBehaviour _carEngine;
        private WheelBehaviour _carWheels;
        private SteeringBehaviour _carSteering;
        private BrakeBehaviour _carBrakes;
        private CarBodyBehaviour _carBody;

        public EngineBehaviour CarEngine => _carEngine;
        public WheelBehaviour CarWheels => _carWheels;
        public SteeringBehaviour CarSteering => _carSteering;
        public BrakeBehaviour CarBrakes => _carBrakes;
        public CarBodyBehaviour CarBody => _carBody;

        public void SetMaxMotorTorgue(float value)
        {
            CarEngine.SetMaxTorgue(value);
        }

        public void SetMaxBrakeTorgue(float value)
        {
            CarBrakes.SetMaxTorgue(value);
        }

        public void SetMaxSteeringAngle(float value)
        {
            CarSteering.SetMaxAngle(value);
        }

        public void SetCarMass(float value)
        {
            CarBody.SetMass(value);
        }

        public void SetDownforce(float value)
        {
            CarBody.SetDownforce(value);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _carEngine = GetComponent<EngineBehaviour>();
            _carWheels = GetComponent<WheelBehaviour>();
            _carSteering = GetComponent<SteeringBehaviour>();
            _carBrakes = GetComponent<BrakeBehaviour>();
            _carBody = GetComponent<CarBodyBehaviour>();
        }
#endif

        public float AbsoluteCarSpeed => _carBody.AbsoluteSpeed;
        public float SignedCarSpeed => _carBody.SignedSpeed;
    }
}
