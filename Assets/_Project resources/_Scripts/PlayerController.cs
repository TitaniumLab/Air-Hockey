using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AirHockey
{
    public class PlayerController : NetworkBehaviour, IMovementController
    {
        [SerializeField] private NetworkObject _malletPrefab;
        private bool _isMoving;
        private Plane _plane = new Plane(Vector3.up, 0);
        public event Action OnStartMoving;
        public event Action<Vector3> OnMoveTo;
        public event Action OnStopMoving;



        private void Start()
        {
            var playerInput = GetComponent<PlayerInput>();
            playerInput.enabled = IsOwner || !NetworkManager.Singleton.IsApproved;
        }


        private void Update()
        {
            if (_isMoving)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Using Camera.main coz of scene transitions bug
                _plane.Raycast(ray, out float dis);
                var point = ray.GetPoint(dis);
                OnMoveTo?.Invoke(point);
            }
        }



        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    {
                        OnStartMoving?.Invoke();
                        _isMoving = true;
                    }
                    break;
                case InputActionPhase.Canceled:
                    {
                        _isMoving = false;
                        OnStopMoving?.Invoke();
                    }
                    break;

            }
        }
    }
}
