using UnityEngine;

namespace oxi
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactbleText;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            Debug.Log($"You interacted with {gameObject.name}.");
        }
    }
}
