using UnityEngine;

public class RangeWeapon : MonoBehaviour, IWeapon
{
    public float Damage { get; set; }

    public Transform shootPoint;
    public GameObject projectile;
    public float projectileSpeed = 32f;

    private Transform _target;

    public void PerformAttack(float damage, Transform target)
    {
        Damage = damage;
        _target = target;
        Debug.Log($"RangeWeapon.PerformAttack: Damage = {Damage}, Target = {target}");
    }

    void InstantiateProjectile(Vector3 destination)
    {
        Vector3 targetPoint = destination;

        if(_target != null)
        {
            Collider col = _target.GetComponent<Collider>();
            if (col != null)
            {
                targetPoint = col.bounds.center;
            }
        }

        Vector3 direction = (targetPoint - shootPoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject projInstance = Instantiate(projectile, shootPoint.position, rotation);
        projInstance.GetComponent<Projectile>().Damage = Damage;
        Rigidbody rb = projInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("Projectile prefab не содержит Rigidbody!");
        }
    }

    public void StartAttack()
    {
        Vector3 destination;
        if (_target != null)
        {
            destination = _target.position;
        }
        else
        {
            Ray ray = new Ray(shootPoint.position, shootPoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                destination = hit.point;
            else
                destination = ray.GetPoint(100f);

        }


        InstantiateProjectile(destination);
    }

    public void EndAttack()
    {
    }

    /*
    public void OnDrawGizmos()
    {
        if (shootPoint != null)
        {
            Ray ray = new Ray(shootPoint.position, shootPoint.forward);
            Vector3 dest;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                dest = hit.point;
            else
                dest = ray.GetPoint(100f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(shootPoint.position, dest);
            Gizmos.DrawSphere(dest, 0.1f);
        }
    }
    */
}
