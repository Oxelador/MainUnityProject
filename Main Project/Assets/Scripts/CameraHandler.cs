using UnityEngine;

namespace oxi
{
    public class CameraHandler : MonoBehaviour
    {
        public Vector3 offset;
        public float smoothTime = 0.25f;

        Transform target;
        Vector3 currentVelocity = Vector3.zero;

        void Start()
        {
            if (target == null)
                target = GameObject.FindGameObjectWithTag("Player").transform;

            transform.position = target.position + offset;
        }

        void FixedUpdate()
        {
            FollowTarget();
        }

        public void FollowTarget()
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        }

    }
}