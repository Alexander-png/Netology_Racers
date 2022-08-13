using Cars_5_5.CarComponents;
using Cars_5_5.CarComponents.Assistance;
using UnityEngine;

namespace Cars_5_5.Input.Base
{
    public abstract class BaseCarInput : MonoBehaviour
    {
        private CarObserver _carObserver;
        public CarObserver CarObserver
        { 
            get
            {
                if (_carObserver == null)
                {
                    FindCarObserver();
                }
                return _carObserver;
            }
        }
        
        public WheelBehaviour WheelBehaviour => _carObserver.CarWheels;

        private void FindCarObserver()
        {
            _carObserver = GetComponent<CarObserver>();
        }
    }
}
