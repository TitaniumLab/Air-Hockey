using UnityEngine;

namespace AirHockey
{
    public interface IMovable
    {
        public void MoveToRpc(Vector3 point);
        public void StopMoving();
    }
}
