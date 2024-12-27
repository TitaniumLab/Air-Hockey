using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    [RequireComponent(typeof(Rigidbody))]
    public class MalletController : NetworkBehaviour, IMovable
    {

        private Rigidbody _rb;
        private float _moveSpeed = 10.0f;
        private float _minDis = 0.1f;


        private void Awake()
        {
            //Debug.LogWarning(OwnerClientId);
            _rb = GetComponent<Rigidbody>();
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

        public void StopMoving()
        {
            _rb.linearVelocity = Vector3.zero;
        }
    }
}
