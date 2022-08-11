using Cars_4_4.CarComponents.Assistance;
using UnityEngine;

namespace Cars_4_4.CarComponents
{
    public class CarBodyBehaviour : MonoBehaviour
    {
        private DownForcePointMarker _downForceBody;

        [SerializeField]
        private Rigidbody _carBody;

        [SerializeField]
        private float _downForce;
        [SerializeField]
        private Vector3 _downForcePoint;

        [SerializeField]
        private Vector3 _centerOfMass;

        public float AbsoluteSpeed => _carBody.velocity.sqrMagnitude;
        public float SignedSpeed => transform.InverseTransformDirection(_carBody.velocity).z;

        private void Start()
        {
            SetCenterOfMass();
            MoveDownForceBody();
        }

        private void FixedUpdate()
        {
            ApplyDownforce();
        }

        private void ApplyDownforce()
        {
            if (_carBody != null)
            {
                float force = _downForce * AbsoluteSpeed;
                _carBody.AddForceAtPosition(force * Vector3.down, _downForceBody.transform.position);
            }
        }

        private void MoveDownForceBody()
        {
            if (_downForceBody != null)
            {
                _downForceBody.transform.position = transform.position + _downForcePoint;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + _carBody.centerOfMass, 0.3f);

            Gizmos.color = Color.red;
            Vector3 drawPosition = transform.position + _downForcePoint;
            Gizmos.DrawCube(drawPosition, new Vector3(0.2f, 0.2f, 0.2f));
        }

        private void OnValidate()
        {
            SetCenterOfMass();
            _downForceBody = transform.Find("DownForcePoint").GetComponent<DownForcePointMarker>();
        }
#endif

        public void SetCenterOfMass()
        {
            _carBody.centerOfMass = _centerOfMass;
        }

        public void SetDownForcePoint(Vector3 newValue)
        {
            _downForcePoint = newValue;
        }
    }
}
