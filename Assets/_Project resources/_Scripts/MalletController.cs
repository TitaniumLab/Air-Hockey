using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    [RequireComponent(typeof(Rigidbody))]
    public class MalletController : NetworkBehaviour
    {
        private Rigidbody _rb;
        private IMovementController _controller;
        private bool _isLocalGame;
        [SerializeField] private float _moveSpeed = 10.0f;
        [SerializeField] private float _minDis = 0.1f;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }


        public void Init(IMovementController controller)
        {
            _controller = controller;
            _isLocalGame = NetworkManager.Singleton == null;
            if (_isLocalGame)
            {
                _controller.OnStartMoving += StartMoving;
                _controller.OnMoveTo += MoveTo;
                _controller.OnStopMoving += StopMoving;
            }
            else
            {
                _controller.OnStartMoving += StartMovingRpc;
                _controller.OnMoveTo += MoveToRpc;
                _controller.OnStopMoving += StopMovingRpc;
            }
        }


        public override void OnDestroy()
        {
            if (_controller != null)
            {
                _controller.OnStartMoving -= StartMoving;
                _controller.OnMoveTo -= MoveTo;
                _controller.OnStopMoving -= StopMoving;

                _controller.OnStartMoving -= StartMovingRpc;
                _controller.OnMoveTo -= MoveToRpc;
                _controller.OnStopMoving -= StopMovingRpc;
            }
        }


        #region Movement
        public void StartMoving()
        {
            _rb.isKinematic = false;
        }


        public void MoveTo(Vector3 point)
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
            _rb.isKinematic = true;
        }
        #endregion


        #region RpcMovement
        [Rpc(SendTo.Owner)]
        public void StartMovingRpc()
        {
            StartMoving();
        }


        [Rpc(SendTo.Owner)]
        public void MoveToRpc(Vector3 point)
        {
            MoveTo(point);
        }


        [Rpc(SendTo.Owner)]
        public void StopMovingRpc()
        {
            StopMoving();
        }
        #endregion
    }
}
