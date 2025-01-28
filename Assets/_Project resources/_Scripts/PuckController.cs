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
        [SerializeField] private float _minSpeed = 10f;
        [SerializeField] private float _maxSpeed = 40f;
        private Vector3 _lastDir = Vector3.zero;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }


        private void OnDisable()
        {
            if (IsSessionOwner)
            {
                _rb.linearVelocity = Vector3.zero;
                NetworkObject.Despawn();

            }

        }


        private void OnCollisionEnter(Collision collision)
        {
            if (IsOwner || !NetworkManager.IsApproved)
            {
                var newVel = collision.relativeVelocity.magnitude * _velocityDamping;
                newVel = math.clamp(newVel, _minSpeed, _maxSpeed);
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
