using UnityEngine;

namespace OrbitalCamera
{
    [ExecuteInEditMode]
    public class OrbitalCamera : MonoBehaviour
    {
        // Input
        [SerializeField] private Transform _camera;
        [SerializeField] private float _outerOrbitRadius = 1;
        [SerializeField, Range(-1, +1)] private float _offset;
        [SerializeField] private float _rotation;
        [SerializeField, Range(-89, +89)] private float _inclination;

        // Output
        private Vector3 _right;
        private Vector3 _forward;
        private float _innerOrbitRadius;
        private Vector3 _innerOrbitCenter;
        private Vector3 _outerOrbitCenter;

        private void Update()
        {
            _rotation += Input.GetAxis("Mouse X");
            _inclination = Mathf.Clamp(_inclination + -Input.GetAxis("Mouse Y"), -89, +89);

            _outerOrbitCenter = transform.position;
            var rotation = Quaternion.AngleAxis(_rotation, Vector3.up);
            _right = rotation * Vector3.right;
            var inclination = Quaternion.AngleAxis(_inclination, _right);
            _forward = inclination * rotation * Vector3.forward;
            _innerOrbitCenter = _outerOrbitCenter + _outerOrbitRadius * _offset * _right;
            _innerOrbitRadius = _outerOrbitRadius * Mathf.Sin(Mathf.Acos(_offset));
        }

        private void LateUpdate()
        {
            _camera.forward = _forward;
            _camera.position = _innerOrbitCenter - _forward * _innerOrbitRadius;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(_outerOrbitCenter, _right, Color.red);
            Debug.DrawRay(_innerOrbitCenter, _forward, Color.blue);
            Gizmos.Circle(_outerOrbitCenter, _outerOrbitRadius, Vector3.up);
            Gizmos.Circle(_innerOrbitCenter, _innerOrbitRadius, _right);
        }
    }
}