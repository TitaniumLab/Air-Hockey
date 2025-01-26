using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using static TestInstaller;

namespace AirHockey
{
    [RequireComponent(typeof(IMatchmake))]
    //[RequireComponent(typeof(NetworkManager))]
    public class MatchManager : MonoBehaviour
    {
        private IMatchmake _matchmake;
        //private NetworkManager _networkManager;
        private ZenjectSceneLoader _loader;

        [SerializeField] private string _arenaSceneName;
        [SerializeField] private NetworkObject _playerControllerPrefab;

        [Inject]
        private void Construct(ZenjectSceneLoader loader)
        {
            _loader = loader;

        }


        private void Awake()
        {
            _matchmake = GetComponent<IMatchmake>();
            //_networkManager = GetComponent<NetworkManager>();
            _matchmake.OnMatchFound += LoadGameArena;
        }

        private async void Start()
        {
            await Awaitable.NextFrameAsync();
            //SceneManager.LoadScene(_arenaSceneName, LoadSceneMode.Single);
        }


        private void OnDestroy()
        {
            _matchmake.OnMatchFound -= LoadGameArena;
        }


        private void LoadGameArena()
        {
            SceneContext.ExtraBindingsInstallMethod = (container) => { container.Bind<ITickable>().To<CheckTick>().AsSingle().NonLazy(); };
            //NetworkManager.Singleton.SceneManager.OnSceneEvent += OnArenaLoad;
            if (NetworkManager.Singleton.LocalClient.IsSessionOwner)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(_arenaSceneName, LoadSceneMode.Single);
            }

        }


        //private void OnArenaLoad(SceneEvent sceneEvent)
        //{
        //    if (sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted)
        //    {
        //        var id = NetworkManager.Singleton.LocalClientId;
        //        // DistributionOfPlayers.Instance.SetCamera(id);
        //        Instantiate(_playerControllerPrefab).SpawnAsPlayerObject(id);
        //        Debug.Log("All members loaded to scene.");
        //        NetworkManager.Singleton.SceneManager.OnSceneEvent -= OnArenaLoad;
        //    }
        //}
    }
}
