using UnityEngine;

namespace Cars_5_5.CarComponents.Assistance
{
    public class CarObserver : MonoBehaviour
    {
        private EngineBehaviour _carEngine;
        private WheelBehaviour _carWheels;
        private SteeringBehaviour _carSteering;
        private CarBodyBehaviour _carBody;

        public EngineBehaviour CarEngine => _carEngine;
        public WheelBehaviour CarWheels => _carWheels;
        public SteeringBehaviour CarSteering => _carSteering;
        public CarBodyBehaviour CarBody => _carBody;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _carEngine = GetComponent<EngineBehaviour>();
            _carWheels = GetComponent<WheelBehaviour>();
            _carSteering = GetComponent<SteeringBehaviour>();
            _carBody = GetComponent<CarBodyBehaviour>();
        }
#endif

        public float AbsoluteCarSpeed => _carBody.AbsoluteSpeed;
        public float SignedCarSpeed => _carBody.SignedSpeed;
    }
}
