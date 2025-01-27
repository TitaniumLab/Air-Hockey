using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using Zenject;


namespace AirHockey
{
    public class DistributionOfPlayers : NetworkBehaviour
    {
        [SerializeField] private Camera[] _cameras;
        //[SerializeField] private List<Transform> _spawnPoss;
        //private MalletController[] _mallets = new MalletController[2];
        //public static DistributionOfPlayers Instance;


        [Inject]
        private void Construct([InjectOptional] List<ClientData> clientDatas)
        {
            if (clientDatas.Count == 0)
            {
            }
        }


        private void Awake()
        {
            // Debug.Log(NetworkManager.LocalClientId);
        }

        public void Start()
        {
        }


        //private void Awake()
        //{
        //    Instance = this;
        //}


        //public override void OnNetworkSpawn()
        //{
        //    base.OnNetworkSpawn();
        //}



        //public override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Instance = null;
        //}


        //public void SetCamera(ulong playerId)
        //{
        //    foreach (var cam in _cameras.Except(new[] { _cameras[playerId - 1] }))
        //    {
        //        cam.gameObject.SetActive(false);
        //    }
        //}

        //public Vector3 GetSpawnPosition(ulong playerId)
        //{
        //    return _spawnPoss[(int)playerId - 1].position;
        //}
    }
}
