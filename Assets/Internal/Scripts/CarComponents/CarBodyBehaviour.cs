using Cars_5_5.CarComponents.Assistance;
using UnityEngine;

namespace Cars_5_5.CarComponents
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

        public void SetCenterOfMass()
        {
            _carBody.centerOfMass = _centerOfMass;
        }

        private void MoveDownForceBody()
        {
            if (_downForceBody != null)
            {
                _downForceBody.transform.localPosition = _downForcePoint;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_carBody.worldCenterOfMass, 0.3f);

            Gizmos.color = Color.red;
            Gizmos.DrawCube(_downForceBody.transform.position, new Vector3(0.2f, 0.2f, 0.2f));
        }

        private void OnValidate()
        {
            SetCenterOfMass();
            _downForceBody = transform.Find("DownForcePoint").GetComponent<DownForcePointMarker>();
            MoveDownForceBody();
        }
#endif
    }
}
