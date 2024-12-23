using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirHockey
{
    [RequireComponent(typeof(IMatchmake))]
    [RequireComponent(typeof(NetworkManager))]
    public class MatchManager : MonoBehaviour
    {
        private IMatchmake _matchmake;
        private NetworkManager _networkManager;
        [SerializeField] private string _arenaSceneName;
        [SerializeField] private GameObject _playerPrefab;

        private void Awake()
        {
            _matchmake = GetComponent<IMatchmake>();
            _networkManager = GetComponent<NetworkManager>();
            _matchmake.OnMatchFound += LoadGameArena;
        }

        private void OnDestroy()
        {
            _matchmake.OnMatchFound -= LoadGameArena;
        }


        private void LoadGameArena()
        {
            _networkManager.SceneManager.OnSceneEvent += OnArenaLoad;
            if (_networkManager.LocalClient.IsSessionOwner)
            {
                _networkManager.SceneManager.LoadScene(_arenaSceneName, LoadSceneMode.Single);
            }
        }

        private void OnArenaLoad(SceneEvent sceneEvent)
        {
            if (sceneEvent.SceneEventType == SceneEventType.SynchronizeComplete)
            {

                int index = (int)NetworkManager.Singleton.LocalClientId - 1;
                DistributionOfPlayers.Instance.SetCamera(index);
                var pos = DistributionOfPlayers.Instance.GetSpawnPosition(index);
                SpawnPlayer(pos);
                Debug.Log("All members loaded to scene.");
                _networkManager.SceneManager.OnSceneEvent -= OnArenaLoad;
            }
        }


        private void SpawnPlayer(Vector3 position)
        {
            var obj = Instantiate(_playerPrefab, position, Quaternion.identity);
            var netObj = obj.GetComponent<NetworkObject>();
            netObj.Spawn(true);
        }
    }
}
