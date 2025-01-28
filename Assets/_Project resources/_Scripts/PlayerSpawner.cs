using UnityEngine;
using Zenject;

namespace AirHockey
{
    public class PlayerSpawner : IInitializable
    {
        private PlayerFactory _factory;


        public PlayerSpawner(PlayerFactory factory)
        {
            _factory = factory;
        }


        public void Initialize()
        {
            _factory.Create(Vector3.zero);
        }
    }
}
