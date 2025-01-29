using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    public class MatchManager : MonoBehaviour
    {
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
