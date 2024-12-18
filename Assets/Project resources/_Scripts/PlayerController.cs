using Unity.Netcode.Components;
using UnityEngine;

namespace AirHockey
{
    public class PlayerController : NetworkTransform
    {
        private Camera _camera;
        private Plane _plane = new Plane(Vector3.up, 0);


        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0) && IsOwner)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (_plane.Raycast(ray, out float dis))
                {
                    transform.position = ray.GetPoint(dis);
                }
            }
        }
    }
}
