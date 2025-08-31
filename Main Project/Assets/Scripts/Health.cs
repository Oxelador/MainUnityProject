using oxi;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace oxi
{
    public class Health : MonoBehaviour
    {
        [Header("References")]
        Collider objectCollider;
        CharacterStats characterStats;
        AnimatorHandler animatorHandler;

        //Events
        public event Action<float> UpdateHealth;
        public event Action<float> OnDamageTaken;

        public bool IsDead { get; private set; } = false;
        public float MaxHealth
        {
            get { return maxHealth; }
            set
            {
                if (value <= 0)
                {
                    maxHealth = 1;
                }
            }
        }
        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                if (currentHealth != value)
                {
                    currentHealth = value;
                    UpdateHealth?.Invoke(currentHealth);
                }
            }
        }

        float maxHealth;
        float currentHealth;
        float armor;

        void Start()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            objectCollider = GetComponent<Collider>();
            characterStats = GetComponent<CharacterStats>();

            maxHealth = characterStats.GetStatValueByName(StatName.Survivability);
            currentHealth = maxHealth;
            armor = characterStats.GetStatValueByName(StatName.Armor);
        }

        public void TakeDamage(float amount)
        {
            if (IsDead) return; // Prevent further damage if already dead

            float armorHealthLimitPercentage = (maxHealth / 100) * 35;
            float amountAfterArmor = amount - armor;

            if (amountAfterArmor >= armorHealthLimitPercentage)
            {
                amountAfterArmor = armorHealthLimitPercentage;
            }

            if (currentHealth <= 0 || amountAfterArmor > currentHealth)
            {
                Death();
            }
            else
            {
                CurrentHealth -= amountAfterArmor;

                animatorHandler.PlayTargetAnimation("Damage_01", true);
                Debug.Log(this.name + " take " + amountAfterArmor + " damage.");

                OnDamageTaken?.Invoke(amountAfterArmor);
            }

        }

        public void Heal(float amount)
        {
            if (amount >= maxHealth)
            {
                CurrentHealth = maxHealth;
            }

            CurrentHealth += amount;
        }

        void Death()
        {
            if (IsDead) return; // Prevent multiple death calls

            IsDead = true;
            UpdateHealth?.Invoke(0);
            Debug.Log(this.name + " is die!");

            if (tag == "Enemy")
            {
                LootBag lootBag = GetComponent<LootBag>();
                lootBag?.InstantiateLoot(transform.position);
                objectCollider.enabled = false;
                GetComponent<EnemyController>().enabled = false;
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponentInChildren<FloatingHealthBar>()?.gameObject.SetActive(false);
            }

            animatorHandler.PlayTargetAnimation("Death_01", true);
        }
    }
}
