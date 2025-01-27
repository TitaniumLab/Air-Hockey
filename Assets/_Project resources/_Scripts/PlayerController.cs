using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AirHockey
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private NetworkObject _malletPrefab;
        private IMovable _movable;
        private bool _isMoving;
        private Plane _plane = new Plane(Vector3.up, 0);
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();

        }


        public override void OnNetworkSpawn()
        {

            Debug.Log(IsOwner);
            base.OnNetworkSpawn();
        }


        private void Start()
        {
            _playerInput.enabled = IsOwner;
            Debug.Log(IsOwner);
            if (IsOwner)
            {
                //var sessionOwner = NetworkManager.CurrentSessionOwner;
                ////var pos = DistributionOfPlayers.Instance.GetSpawnPosition(OwnerClientId);
                //var netObj = Instantiate(_malletPrefab, pos, Quaternion.identity);
                //netObj.Spawn();
                //_movable = netObj.GetComponent<IMovable>();
                //if (sessionOwner != OwnerClientId)
                //{
                //    netObj.ChangeOwnership(sessionOwner); // In distributed authority SpawnWithOwnership(sessionOwner) throw exception
                //}
            }
        }

        public void Init(IMovable movable)
        {
            _movable = movable;
        }


        public override void OnNetworkDespawn()
        {
            _playerInput.enabled = false;
            base.OnNetworkDespawn();
        }


        private void Update()
        {
            if (_isMoving)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Using Camera.main coz of scene transitions bug
                _plane.Raycast(ray, out float dis);
                var point = ray.GetPoint(dis);
                _movable.MoveToRpc(point);
                Debug.Log("CLick");
            }
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    {
                        _movable.StartMovingRpc();
                        _isMoving = true;
                    }
                    break;
                case InputActionPhase.Canceled:
                    {
                        _isMoving = false;
                        _movable.StopMovingRpc();
                    }
                    break;

            }
            //if (context.started)
            //{

            //}
            //else if (context.canceled)
            //{
            //    _isMoving = false;
            //}
        }
    }
}
