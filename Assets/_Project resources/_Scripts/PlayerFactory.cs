using UnityEngine;

namespace AirHockey
{
    public class PlayerFactory
    {
        private PlayerControllerFactory _controllerFactory;
        private MovableFactory _movableFactory;

        public PlayerFactory(PlayerControllerFactory controllerFactory, MovableFactory movableFactory)
        {
            _controllerFactory = controllerFactory;
            _movableFactory = movableFactory;
        }


        public (PlayerController, MalletController) Create(Vector3 position)
        {
            var controller = _controllerFactory.Create();
            var movable = _movableFactory.Create();
            movable.transform.position = position;
            movable.Init(controller);
            return (controller, movable);
        }
    }
}
