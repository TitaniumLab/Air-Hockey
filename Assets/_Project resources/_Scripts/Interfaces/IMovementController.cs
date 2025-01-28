using System;
using UnityEngine;

namespace AirHockey
{
    public interface IMovementController
    {
        public event Action OnStartMoving;
        public event Action<Vector3> OnMoveTo;
        public event Action OnStopMoving;
    }
}
