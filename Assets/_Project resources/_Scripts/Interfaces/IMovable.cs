using UnityEngine;

namespace AirHockey
{
    public interface IMovable
    {
        public void StartMovingRpc();
        public void MoveToRpc(Vector3 point);
        public void StopMovingRpc();
    }
}
