using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AirHockey
{
    public class PlayerController : NetworkBehaviour
    {
        private IMovable _movable;
        private bool _isMoving;
        private Plane _plane = new Plane(Vector3.up, 0);



        private void Start()
        {
            _movable = DistributionOfPlayers.Instance.GetMovable(OwnerClientId);
            //DisableOtherRpc();
        }


        private void Update()
        {
            if (_isMoving)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Using Camera.main coz of scene transitions bug
                _plane.Raycast(ray, out float dis);
                var point = ray.GetPoint(dis);
                _movable.MoveToRpc(point);
            }
        }

        [Rpc(SendTo.NotMe)]
        private void DisableOtherRpc()
        {
            gameObject.SetActive(false);
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
            }
        }
    }
}
