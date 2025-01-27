using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

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
            // !!!!!!!!!!!!!! SceneContext.ExtraBindingsInstallMethod = (container) => { container.Bind<ITickable>().To<CheckTick>().AsSingle().NonLazy(); };
            //NetworkManager.Singleton.SceneManager.OnSceneEvent += OnArenaLoad;
            if (NetworkManager.Singleton.LocalClient.IsSessionOwner)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(_arenaSceneName, LoadSceneMode.Single);
            }

        }
    }
}
