using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    [RequireComponent(typeof(Rigidbody))]
    public class MalletController : NetworkBehaviour, IMovable
    {
        private Rigidbody _rb;
        [SerializeField] private float _moveSpeed = 10.0f;
        [SerializeField] private float _minDis = 0.1f;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        [Rpc(SendTo.Owner)]
        public void StartMovingRpc()
        {
            _rb.isKinematic = false;
        }

        [Rpc(SendTo.Owner)]
        public void MoveToRpc(Vector3 point)
        {
            var direction = point - transform.position;
            if (direction.magnitude > _minDis)
            {
                _rb.linearVelocity = direction.normalized * _moveSpeed;
            }
            else
            {
                _rb.linearVelocity = Vector3.zero;
            }
        }


        [Rpc(SendTo.Owner)]
        public void StopMovingRpc()
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.isKinematic = true;
        }
    }
}
