using UnityEngine;

namespace AirHockey
{
    public class GoalArea : MonoBehaviour
    {
        [SerializeField] private int _teamIndex;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PuckController controller))
            {
                EventBus.Instance.Invoke(new GoalEvent(_teamIndex));

                Destroy(other.gameObject); // !!!!!!!!!!!!! CREATE OBJECT POOL
            }
        }
    }
}
