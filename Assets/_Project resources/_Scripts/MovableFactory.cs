using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace AirHockey
{
    public class MovableFactory
    {
        private NetworkObject _movableObj;

        public MovableFactory(NetworkObject networkObject)
        {
            _movableObj = networkObject;
        }

        public MalletController Create()
        {
            var obj = GameObject.Instantiate(_movableObj);

            if (!obj.TryGetComponent(out MalletController component))
            {
                component = obj.AddComponent<MalletController>();
            }


            if (NetworkManager.Singleton.IsApproved)
            {
                obj.Spawn();
            }

            var owner = NetworkManager.Singleton.CurrentSessionOwner;
            // In distributed authority SpawnWithOwnership(sessionOwner) throw exception
            if (NetworkManager.Singleton.LocalClientId != owner)
            {
                obj.ChangeOwnership(owner);
            }

            return component;
        }
    }
}
