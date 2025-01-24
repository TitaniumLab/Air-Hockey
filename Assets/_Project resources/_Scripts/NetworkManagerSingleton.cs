using Unity.Netcode;
using UnityEngine;
using UnityEngine.Scripting;

namespace AirHockey
{
    [RequireComponent(typeof(NetworkManager))]
    public class NetworkManagerSingleton : MonoBehaviour
    {
        private void Awake()
        {
            var manager = GetComponent<NetworkManager>();
            if (NetworkManager.Singleton != manager)
            {
                manager.Shutdown();
                Destroy(gameObject);
                return;
            }
        }
    }
}
