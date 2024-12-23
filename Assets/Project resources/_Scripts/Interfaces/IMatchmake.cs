using System;

namespace AirHockey
{
    public interface IMatchmake
    {
        public event Action OnMatchFound;
    }
}
