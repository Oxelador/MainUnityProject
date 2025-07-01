using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject owner;
    public float Damage { get; set; }

    private bool collided;

    private void Awake()
    {
        owner = transform.root.gameObject;
    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject != owner && co.gameObject.tag == "Player" || co.gameObject.tag == "Enemy")
        {
            var health = co.gameObject.GetComponent<Health>();
            if (health != null)
            {
                Debug.Log("Projectile hit: " + co.gameObject.name);
                health.TakeDamage(Damage);
            }
        }
        if (co.gameObject != owner && co.gameObject.tag != "Projectile" && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }
    }
}
