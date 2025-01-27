using UnityEngine;
using Zenject;

namespace AirHockey
{
    public class PlayerSpawner : IInitializable
    {
        private PlayerControllerFactory _factory;


        public PlayerSpawner(PlayerControllerFactory factory)
        {
            _factory = factory;
            Debug.Log("Spawner");
        }

        public void Initialize()
        {
            _factory.Create();
            Debug.Log("init");
        }
    }
}
