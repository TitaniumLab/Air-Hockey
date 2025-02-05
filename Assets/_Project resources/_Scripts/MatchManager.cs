using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace AirHockey
{
    public class MatchManager : MonoBehaviour
    {
        private PlayerSpawner _spawner;
        private PlayerData _playerData;


        [Inject]
        private void Construct(PlayerSpawner spawner, [InjectOptional] PlayerData playerData)
        {
            _spawner = spawner;
            if (playerData == null)
            {
                _playerData = playerData;
            }
        }


        private void Awake()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.SceneManager.OnSceneEvent += OnSceneLoad;
            }

        }


        private void OnSceneLoad(SceneEvent sceneEvent)
        {
            if (sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted)
            {
                Debug.Log("All clients loaded to scene.");
            }
        }
    }
}
