using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;


namespace AirHockey
{
    public class DistributionOfPlayers : NetworkBehaviour
    {
        [SerializeField] private Camera[] _cameras;
        [SerializeField] private List<Transform> _spawnPoss;


        //public Vector3 GetSpawnPosition(ulong playerId)
        //{
        //    return _spawnPoss[(int)playerId - 1].position;
        //}
    }
}
