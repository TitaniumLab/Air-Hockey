using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirHockey
{
    [RequireComponent(typeof(NetworkManager))]
    public class ConnectionManager : MonoBehaviour
    {
        private NetworkManager _networkManager;
        private ConnectionState _connectionState;
        private string _profileName;
        private ISession _session;
        [SerializeField] private TMP_InputField _inputField;

        private enum ConnectionState
        {
            Disconnected,
            Connecting,
            Connected,
        }

        private async void Awake()
        {
            _networkManager = GetComponent<NetworkManager>();
            _networkManager.OnClientConnectedCallback += OnClientConnectedCallback;
            _networkManager.OnSessionOwnerPromoted += OnSessionOwnerPromoted;
            await UnityServices.InitializeAsync();
        }

        private void OnSessionOwnerPromoted(ulong sessionOwnerPromoted)
        {
            if (_networkManager.LocalClient.IsSessionOwner)
            {
                Debug.Log($"Client-{_networkManager.LocalClientId} is the session owner!");
            }
        }

        private void OnClientConnectedCallback(ulong clientId)
        {
            //if (_networkManager.LocalClientId == clientId)
            //{
            //    Debug.Log($"Client-{clientId} is connected and can spawn {nameof(NetworkObject)}s.");
            //}

            if (_session.AvailableSlots == 0 && _networkManager.LocalClient.IsSessionOwner)
            {
                //_networkManager.SceneManager.OnLoadComplete += ;
                //_networkManager.SceneManager.OnLoadComplete += () =>
                //{
                //    Debug.Log(handler.ToString());
                //};
                _networkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                //_networkManager.SceneManager.scene
            }

        }


        public async void CreateOrJoinSessionAsync()
        {
            _connectionState = ConnectionState.Connecting;
            try
            {
                AuthenticationService.Instance.SwitchProfile(_inputField.text);
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                var quickJoinOprions = new QuickJoinOptions()
                {
                    CreateSession = true,
                    Timeout = TimeSpan.FromSeconds(1)
                };

                var options = new SessionOptions()
                {
                    MaxPlayers = 2,
                    Type = "Session",
                }.WithDistributedAuthorityNetwork();

                _session = await MultiplayerService.Instance.MatchmakeSessionAsync(quickJoinOprions, options);
                _connectionState = ConnectionState.Connected;
            }
            catch (Exception e)
            {
                _connectionState = ConnectionState.Disconnected;
                Debug.LogException(e);
            }
        }
    }
}
