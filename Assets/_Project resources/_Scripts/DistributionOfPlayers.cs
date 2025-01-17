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
        private MalletController[] _mallets = new MalletController[2];
        public static DistributionOfPlayers Instance;

        private void Awake()
        {
            Instance = this;
        }


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }

        //public IMovable SpawnMovable(ulong playerId)
        //{
        //    if (IsSessionOwner)
        //    {
        //        var netobj = Instantiate(_networkObject);
        //        _mallets[playerId - 1] = netobj.GetComponent<MalletController>();
        //        netobj.Spawn();
        //        return netobj.GetComponent<MalletController>();
        //    }
        //    return _mallets[playerId - 1];
        //}


        public override void OnDestroy()
        {
            base.OnDestroy();
            Instance = null;
        }


        public void SetCamera(ulong playerId)
        {
            foreach (var cam in _cameras.Except(new[] { _cameras[playerId - 1] }))
            {
                cam.gameObject.SetActive(false);
            }
        }

        public Vector3 GetSpawnPosition(ulong playerId)
        {
            return _spawnPoss[(int)playerId - 1].position;
        }
    }
}
