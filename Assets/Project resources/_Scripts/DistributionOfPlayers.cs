using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    public class DistributionOfPlayers : NetworkBehaviour
    {
        [SerializeField] private Camera[] _cameras;
        [SerializeField] private NetworkObject _networkObject;
        [SerializeField] private MalletController[] _mallets;
        public static DistributionOfPlayers Instance;

        private void Awake()
        {
            Instance = this;
            //var netobj = Instantiate(_networkObject);
            //_mallets[0] = netobj.GetComponent<MalletController>();
            ////netobj.Spawn();
            //var netobj2 = Instantiate(_networkObject);
            //_mallets[1] = netobj2.GetComponent<MalletController>();
            //netobj2.Spawn();

            //NetworkManager.SceneManager.OnSceneEvent += OnSceneLoad;
        }


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


        public IMovable GetMovable(ulong playerId)
        {
            return _mallets[playerId - 1];
        }
    }
}
