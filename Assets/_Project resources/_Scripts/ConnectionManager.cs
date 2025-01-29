using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirHockey
{
    public class ConnectionManager : MonoBehaviour
    {
        [SerializeField] private int _maxPlayers = 2;
        private ISession _session;





        private async void Awake()
        {
            //_networkManager = GetComponent<NetworkManager>();
            await UnityServices.InitializeAsync();
        }


        private void Start()
        {
            NetworkManager.Singleton.NetworkConfig.UseCMBService = true; // Enabled by default on distributed authority, but also throws an warning
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnSessionOwnerPromoted += OnSessionOwnerPromoted;
        }


        private void OnDestroy()
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
            NetworkManager.Singleton.OnSessionOwnerPromoted -= OnSessionOwnerPromoted;
        }


        private void OnSessionOwnerPromoted(ulong sessionOwnerPromoted)
        {
            if (NetworkManager.Singleton.LocalClient.IsSessionOwner)
            {
                Debug.Log($"Client-{NetworkManager.Singleton.LocalClientId} is the session owner.");
            }
        }

        private void OnClientConnectedCallback(ulong clientId)
        {
            Debug.Log($"Client-{clientId} connected.");
            if (/*_session != null &&*/ _session.MaxPlayers == (int)clientId)
            {
                if (NetworkManager.Singleton.LocalClient.IsSessionOwner)
                {
                    NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                }
                //OnMatchFound?.Invoke();
            }
        }


        public async void CreateOrJoinSessionAsync()
        {
            try
            {
                if (!AuthenticationService.Instance.IsSignedIn)
                {
                    Debug.Log("SignIn started.");
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    Debug.Log("SignedIn successfully.");
                }
                var quickJoinOprions = new QuickJoinOptions()
                {
                    CreateSession = true,
                    Timeout = TimeSpan.FromSeconds(1)
                };

                var options = new SessionOptions()
                {
                    MaxPlayers = _maxPlayers,
                    Type = "Session",
                }.WithDistributedAuthorityNetwork();

                _session = await MultiplayerService.Instance.MatchmakeSessionAsync(quickJoinOprions, options);
                Debug.Log(_session);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }


        //private void LoadGameArena()
        //{
        //    // !!!!!!!!!!!!!! SceneContext.ExtraBindingsInstallMethod = (container) => { container.Bind<ITickable>().To<CheckTick>().AsSingle().NonLazy(); };
        //    //NetworkManager.Singleton.SceneManager.OnSceneEvent += OnArenaLoad;
        //    if (NetworkManager.Singleton.LocalClient.IsSessionOwner)
        //    {
        //        NetworkManager.Singleton.SceneManager.LoadScene(_arenaSceneName, LoadSceneMode.Single);
        //    }

        //}
    }
}
