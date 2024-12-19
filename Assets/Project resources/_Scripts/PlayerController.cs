using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirHockey
{
    public class PlayerController : NetworkTransform
    {
        private Camera _camera;
        private Plane _plane = new Plane(Vector3.up, 0);



        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += SetCamera;
            _camera = Camera.main;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            SceneManager.sceneLoaded -= SetCamera;
        }

        private void SetCamera(Scene scene, LoadSceneMode mod)
        {
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
