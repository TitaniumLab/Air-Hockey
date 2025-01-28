using System;
using UnityEngine;

namespace AirHockey
{
    public class PlayerData
    {
        public int TeamIndex { get; }

        PlayerData(int teamIndex)
        {
            TeamIndex = teamIndex;
        }
        // [field: SerializeField] public int SkinId;
    }
}
