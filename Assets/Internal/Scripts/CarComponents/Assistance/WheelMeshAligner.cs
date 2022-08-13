using UnityEngine;

namespace Cars_5_5.CarComponents.Assistance
{
    public class WheelMeshAligner : MonoBehaviour
    {
        [SerializeField]
        private WheelCollider _wheelCollider;
        [SerializeField]
        private Transform _wheelMesh;

        private void FixedUpdate()
        {
            MoveWheelMeshToCollider();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _wheelCollider = GetComponent<WheelCollider>();
            MoveWheelMeshToCollider();
        }
#endif

        public void MoveWheelMeshToCollider()
        {
            if (_wheelMesh == null)
            {
#if UNITY_EDITOR
                Debug.Log("No wheel mesh is attached to this wheel collider");
#endif
                return;
            }
            _wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            _wheelMesh.position = position;
            _wheelMesh.rotation = rotation;
        }
    }
}
