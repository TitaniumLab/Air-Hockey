using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;

namespace AirHockey
{
    //[RequireComponent(typeof(NetworkManager))]
    public class ConnectionManager : MonoBehaviour, IMatchmake
    {
        [SerializeField] private int _maxPlayers = 2;
        //private NetworkManager _networkManager;
        private ISession _session;

        public event Action OnMatchFound;




        private async void Awake()
        {
            //_networkManager = GetComponent<NetworkManager>();
            //NetworkManager.Singleton.NetworkConfig.UseCMBService = true;

            await UnityServices.InitializeAsync();
        }


        private void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnSessionOwnerPromoted += OnSessionOwnerPromoted;
        }


        private void OnDestroy()
        {
            //if (AuthenticationService.Instance.IsSignedIn)
            //{
            //    AuthenticationService.Instance.SignOut();
            //    Debug.Log("Signed out successfully.");
            //}
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
            if (_session.MaxPlayers == (int)clientId)
            {
                OnMatchFound?.Invoke();
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
    }
}
