using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AirHockey
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private Rigidbody _playerPrefab;
        private float _moveSpeed = 1.0f;
        private float _minDis = 0.1f;
        private Rigidbody _rb;
        private bool _isMoving;
        private Plane _plane = new Plane(Vector3.up, 0);



        private void Awake()
        {
            DistributionOfPlayers.OnMatchStart += SpawnPlayerPrefab;
            Debug.Log(NetworkManager.LocalClientId);
        }


        public override void OnDestroy()
        {
            base.OnDestroy();
            DistributionOfPlayers.OnMatchStart -= SpawnPlayerPrefab;
        }

        private void Update()
        {
            // IsOwner prevents small glitch when movement button pressed
            if (_isMoving)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Using Camera.main coz of scene transitions bug
                _plane.Raycast(ray, out float dis);
                var direction = ray.GetPoint(dis) - _rb.transform.position;
                if (direction.magnitude > _minDis)
                {
                    _rb.linearVelocity = direction.normalized * _moveSpeed;
                }
                else
                {
                    _rb.linearVelocity = Vector3.zero;
                }
            }
        }


        private void SpawnPlayerPrefab(Vector3 position)
        {
            _rb = Instantiate(_playerPrefab, position, Quaternion.identity, transform);
            var netObj = _rb.GetComponent<NetworkObject>();
            netObj.Spawn();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _isMoving = true;
            }
            else if (context.canceled)
            {
                _isMoving = false;
                _rb.linearVelocity = Vector3.zero;
            }
        }
    }
}
