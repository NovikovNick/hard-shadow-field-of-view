using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m8t
{
    public class ProjectileComponent : MonoBehaviour
    {
        [Header("General settings")]
        [SerializeField] private int damage = 10;
        [SerializeField] private float velocity = 10f;
        [SerializeField] private LayerMask hitBoxlayerMask;
        [SerializeField] private LayerMask collisionLayerMask;
        [SerializeField] private float ttl = 2.0f;

        public Vector2 direction = Vector2.zero;


        void Awake()
        {
            Destroy(gameObject, ttl);
        }

        private void FixedUpdate()
        {

            var delta = direction * velocity * Time.fixedDeltaTime;
            transform.position += new Vector3(delta.x, delta.y, 0);
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & hitBoxlayerMask) != 0)
            {
                var hitbox = collision.gameObject.GetComponent<HitBoxComponent>();
                hitbox.ApplyDamage(damage);
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & collisionLayerMask) != 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
