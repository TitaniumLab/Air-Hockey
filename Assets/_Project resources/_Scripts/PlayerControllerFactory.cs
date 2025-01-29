using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace AirHockey
{
    public class PlayerControllerFactory
    {
        private NetworkObject _playerController;

        public PlayerControllerFactory(NetworkObject playerController)
        {
            _playerController = playerController;
        }


        public PlayerController Create()
        {
            var obj = GameObject.Instantiate(_playerController);

            if (!obj.TryGetComponent(out PlayerController controller))
            {
                controller = obj.AddComponent<PlayerController>();
            }

            // Allows to debug without starting network
            if (NetworkManager.Singleton != null)
            {
                var id = NetworkManager.Singleton.LocalClientId;
                obj.SpawnAsPlayerObject(id);
                Debug.Log($"{nameof(PlayerController)} created as {nameof(NetworkObject)} of client-{id}");
            }
            else
            {
                Debug.Log($"{nameof(PlayerController)} created as local");
            }

            return controller;
        }
    }
}
