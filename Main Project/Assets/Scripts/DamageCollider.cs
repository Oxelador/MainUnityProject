using System.Collections.Generic;
using UnityEngine;

namespace oxi
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;

        public int currentWeaponDamage = 10;

        private HashSet<Collider> damagedEnemies = new HashSet<Collider>();

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Collider playerCollider = player.GetComponent<Collider>();
                if (playerCollider != null)
                {
                    Physics.IgnoreCollision(damageCollider, playerCollider, true);
                }
            }
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
            damagedEnemies.Clear();
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Enemy")
            {
                if (damagedEnemies.Contains(collision))
                    return;

                Health health = collision.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(currentWeaponDamage);
                    damagedEnemies.Add(collision);
                }
            }
        }
    }
}