using System;

namespace AirHockey
{
    [Serializable]
    public class ClientData
    {
        public ClientType CType;

        // !!! Add prefab skin !!!

        public enum ClientType
        {
            Player = 0,
            Spectator = 1
        }
    }


}
