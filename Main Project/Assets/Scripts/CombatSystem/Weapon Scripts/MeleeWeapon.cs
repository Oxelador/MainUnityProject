using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public float Damage { get; set; }

    private GameObject _owner;
    private bool _isAttacking;
    [SerializeField] private float _attackRadius = 0.5f;

    public Transform attackPoint;

    private Collider[] _hitColliders;

    private void Awake()
    {
        _owner = transform.root.gameObject;
    }

    private void Update()
    {
        _hitColliders = Physics.OverlapSphere(attackPoint.position, _attackRadius);
    }

    public void PerformAttack(float damage, Transform target)
    {
        Damage = damage;
        _isAttacking = true;


        foreach (var collider in _hitColliders)
        {
            if (!_isAttacking) continue;
            if (collider.gameObject == _owner) continue;

            if (collider.gameObject.tag == "Enemy")
            {
                var health = collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(Damage);
                }
            }
        }
    }

    public void StartAttack()
    {

    }

    public void EndAttack()
    {
        _isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, _attackRadius);
    }
}
