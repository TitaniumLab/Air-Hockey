using System;
using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    public class DistributionOfPlayers : NetworkBehaviour
    {
        //[SerializeField] private Camera[] _cameras;
        [SerializeField] private Transform[] _spawnPoss;
        public static event Action<Vector3> OnMatchStart;

        private void Awake()
        {
        }

        private void Start()
        {
            if (IsOwner)
            {
                Debug.Log(_spawnPoss[NetworkManager.LocalClientId - 1]);
                OnMatchStart?.Invoke(_spawnPoss[NetworkManager.LocalClientId - 1].position);
            }

        }



        //public Camera GetPlayerCamera(int playerIndex)
        //{
        //    for (int i = 0; i < _cameras.Length; i++)
        //    {
        //        if (i != playerIndex)
        //        {
        //            _cameras[i].gameObject.SetActive(false);
        //        }
        //    }
        //    return _cameras[playerIndex];
        //}
    }
}
