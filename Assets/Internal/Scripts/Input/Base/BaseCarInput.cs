using Cars_5_5.CarComponents;
using Cars_5_5.CarComponents.Assistance;
using UnityEngine;

namespace Cars_5_5.Input.Base
{
    public abstract class BaseCarInput : MonoBehaviour
    {
        [SerializeField]
        private bool _inputEnabled = true;

        private CarObserver _carObserver;
        public CarObserver CarObserver => _carObserver;
        public WheelBehaviour WheelBehaviour => _carObserver.CarWheels;

        public bool InputEnabled
        {
            get => _inputEnabled;
            set
            {
                _inputEnabled = value;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _carObserver = GetComponent<CarObserver>();
        }
#endif
    }
}
