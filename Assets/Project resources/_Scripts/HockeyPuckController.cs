using Unity.Netcode;
using UnityEngine;

namespace AirHockey
{
    [RequireComponent(typeof(Rigidbody))]
    public class HockeyPuckController : NetworkBehaviour
    {
        private Rigidbody _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Debug.Log(collision.relativeVelocity);
            if (collision.gameObject.TryGetComponent(out NetworkObject netObj) &&
                //collision.gameObject.TryGetComponent(out Rigidbody rb) &&
                OwnerClientId != netObj.OwnerClientId &&
                IsSessionOwner)

            {
                //_rb.linearVelocity = collision.relativeVelocity;
                NetworkObject.ChangeOwnership(netObj.OwnerClientId);
            }
        }
    }
}
