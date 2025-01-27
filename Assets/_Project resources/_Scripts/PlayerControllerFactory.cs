using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace AirHockey
{
    public class PlayerControllerFactory
    {
        private NetworkObject _playerController;
        private MovableFactory _movableFactory;

        public PlayerControllerFactory(NetworkObject playerController, MovableFactory malletFactory)
        {
            _playerController = playerController;
            _movableFactory = malletFactory;
            Debug.Log("PlayerControllerFactory");
        }


        public PlayerController Create()
        {
            var id = NetworkManager.Singleton.LocalClientId;
            var obj = GameObject.Instantiate(_playerController);
            if (!obj.TryGetComponent(out PlayerController playerController))
            {
                playerController = obj.AddComponent<PlayerController>();
            }
            IMovable movable = _movableFactory.Create();
            playerController.Init(movable);
            if (!obj.TryGetComponent(out PlayerController controller))
            {
                obj.AddComponent<PlayerController>();
            }
            if (NetworkManager.Singleton.didStart)
            {
                obj.SpawnAsPlayerObject(id);
            }

            return controller;
        }
    }
}
