using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    [RequireComponent(typeof(NetworkManager))]
    public class ConnectionManager : MonoBehaviour
    {
        private NetworkManager _networkManager;


        private async void Awake()
        {
            _networkManager = GetComponent<NetworkManager>();
        }
    }
}
