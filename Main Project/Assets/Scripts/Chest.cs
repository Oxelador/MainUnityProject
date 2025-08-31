using UnityEngine;

namespace oxi
{
    public class Chest : MonoBehaviour, IInteractable
    {
        private Animator _animator;
        private LootBag _lootBag;

        public bool IsInteracted { get; set; }

        private void Start()
        {
            IsInteracted = false;
            _animator = GetComponent<Animator>();
            _lootBag = GetComponent<LootBag>();
        }

        public string GetDescription()
        {
            return $"Open the {gameObject.name}";
        }

        public void Interact()
        {
            _lootBag.InstantiateLoot(transform.position);
            _animator.SetBool("isChestInteracted", true);
        }
    }
}
