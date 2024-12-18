using System;
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
            await UnityServices.InitializeAsync();
        }


        //private void OnGUI()
        //{
        //    if (_connectionState == ConnectionState.Connected)
        //        return;

        //    GUI.enabled = _connectionState != ConnectionState.Connecting;

        //    using (new GUILayout.HorizontalScope(GUILayout.Width(250)))
        //    {
        //        GUILayout.Label("Profile Name", GUILayout.Width(100));
        //        _profileName = GUILayout.TextField(_profileName);
        //    }

        //    GUI.enabled = GUI.enabled && !string.IsNullOrEmpty(_profileName);/*&& !string.IsNullOrEmpty(_sessionName);*/

        //    if (GUILayout.Button("Create or Join Session"))
        //    {
        //        CreateOrJoinSessionAsync();
        //    }
        //}

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
                    Timeout = TimeSpan.FromSeconds(5)
                };

                var options = new SessionOptions()
                {
                    MaxPlayers = 2,
                    Type = "Session",
                }.WithDistributedAuthorityNetwork();

                var session = await MultiplayerService.Instance.MatchmakeSessionAsync(quickJoinOprions, options);
                SceneManager.LoadScene(1);
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
