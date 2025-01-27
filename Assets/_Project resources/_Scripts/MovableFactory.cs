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
            Debug.Log("MovableFactory");
        }

        public IMovable Create()
        {
            var obj = GameObject.Instantiate(_movableObj);
            if (!obj.TryGetComponent(out IMovable component))
            {
                component = obj.AddComponent<MalletController>();
            }
            var owner = NetworkManager.Singleton.CurrentSessionOwner;
            if (NetworkManager.Singleton.didStart)
            {
                obj.Spawn();
            }
            // In distributed authority SpawnWithOwnership(sessionOwner) throw exception
            if (NetworkManager.Singleton.LocalClientId != owner)
            {
                obj.ChangeOwnership(owner);
            }

            return component;
        }
    }
}
