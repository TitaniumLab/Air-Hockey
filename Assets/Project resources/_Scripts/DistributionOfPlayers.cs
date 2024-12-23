using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    public class DistributionOfPlayers : NetworkBehaviour
    {
        [SerializeField] private Camera[] _cameras;
        [SerializeField] private Transform[] _spawnPoss;
        public static DistributionOfPlayers Instance;

        private void Awake()
        {
            Instance = this;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Instance = null;
        }



        public void SetCamera(int playerIndex)
        {
            int index = (int)NetworkManager.Singleton.LocalClientId - 1;
            foreach (var cam in _cameras.Except(new[] { _cameras[index] }))
            {
                cam.gameObject.SetActive(false);
            }

        }

        public Vector3 GetSpawnPosition(int playerIndex)
        {
            return _spawnPoss[playerIndex].position;
        }
    }
}
