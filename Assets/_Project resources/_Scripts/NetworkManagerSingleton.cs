using Unity.Netcode;

namespace AirHockey
{
    public class NetworkManagerSingleton : NetworkManager
    {
        private void Awake()
        {
            // For debuging porpuses
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
                return;
            }
            NetworkConfig.UseCMBService = true;
        }
    }
}
