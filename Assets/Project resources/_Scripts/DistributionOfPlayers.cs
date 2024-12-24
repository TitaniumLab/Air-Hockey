using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    public class DistributionOfPlayers : NetworkBehaviour
    {
        [SerializeField] private Camera[] _cameras;
        [SerializeField] private Transform[] _spawnPoss;
        [SerializeField] private NetworkObject _puckPrefab;
        public static DistributionOfPlayers Instance;

        private void Awake()
        {
            Instance = this;
            NetworkManager.SceneManager.OnSceneEvent += OnSceneLoad;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Instance = null;
        }


        private void OnSceneLoad(SceneEvent sceneEvent)
        {
            if (sceneEvent.SceneEventType == SceneEventType.SynchronizeComplete &&
                NetworkManager.LocalClient.IsSessionOwner)
            {
                Instantiate(_puckPrefab).Spawn();
            }
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
