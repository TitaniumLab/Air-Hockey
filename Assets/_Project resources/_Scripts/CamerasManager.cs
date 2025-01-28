using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace AirHockey
{
    public class CamerasManager : MonoBehaviour
    {
        [SerializeField] private List<Camera> _cameras;
        private int _teamIndex;

        [Inject]
        private void Construct([InjectOptional] PlayerData data)
        {
            if (data == null)
            {
                _teamIndex = 0;
            }
            else
            {
                _teamIndex = data.TeamIndex;
            }
        }


        private void Awake()
        {
            foreach (var cam in _cameras.Except(new[] { _cameras[_teamIndex] }))
            {
                cam.gameObject.SetActive(false);
            }
        }
    }
}
