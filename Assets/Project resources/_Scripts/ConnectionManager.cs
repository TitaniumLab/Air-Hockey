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
        private string _profileName;
        private ISession _session;
        [SerializeField] private TMP_InputField _inputField;


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
            if (_session.AvailableSlots == 0 && _networkManager.LocalClient.IsSessionOwner)
            {
                _networkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            }
        }


        public async void CreateOrJoinSessionAsync()
        {
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
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
