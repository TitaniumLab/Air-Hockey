using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    [RequireComponent(typeof(Rigidbody))]
    public class PuckController : NetworkBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _velocityDamping = 0.9f;
        [SerializeField] private float _maximumSpeed = 40f;
        private Vector3 _lastDir = Vector3.zero;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsOwner)
            {
                var newVel = collision.relativeVelocity.magnitude * _velocityDamping;
                newVel = math.clamp(newVel, 0, _maximumSpeed);
                if (_lastDir != Vector3.zero)
                {
                    var direction = Vector3.Reflect(_lastDir, collision.contacts[0].normal).normalized;
                    _lastDir = direction * newVel;
                }
                else
                {
                    _lastDir = collision.contacts[0].normal * newVel;
                }
                _rb.linearVelocity = _lastDir;

            }
        }
    }
}
