using UnityEngine;

namespace Game.UnitSystem 
{
    public class BulletProjectile : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed = 3f;
        [SerializeField]
        private TrailRenderer trailRenderer;
        [SerializeField]
        private ParticleSystem hitEffect;

        private Vector3 target;

        public void Setup(Vector3 targetPosition) 
        {
            target = targetPosition;
        }

        void Update()
        {
            Vector3 moveDirection = (target - transform.position).normalized;
            float distanceBeforeMoving = Vector3.Distance(transform.position, target);
            transform.position += moveDirection * bulletSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(transform.position, target);

            if (distanceBeforeMoving < distanceAfterMoving) 
            {
                transform.position = target;
                trailRenderer.transform.parent = null;
                Instantiate(hitEffect, target, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

}