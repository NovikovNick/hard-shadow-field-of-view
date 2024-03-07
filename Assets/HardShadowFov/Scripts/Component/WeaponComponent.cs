using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace m8t
{
    public class WeaponComponent : MonoBehaviour
    {
        [Header("--------- General settings ---------")]
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float offset = 1;

        [Header("--------- Events ---------")]
        [SerializeField] private UnityEvent<AmmoData> OnAmmoDataChange;

        [Header("State")]
        [SerializeField] private bool wantsToShoot;
        [SerializeField] private Weapon equipedWeapon;
        [SerializeField] private Vector2 aimDirection;
        private AmmoData ammo;


        private void Start()
        {
            ammo = new AmmoData();
            ammo.bullet = equipedWeapon.roundsInClip;
            ammo.clips = 3;
        }

        public void OnLookAt(Vector2 dir)
        {
            aimDirection = dir;
        }

        public void StartFire()
        {
            if (equipedWeapon != null)
            {
                wantsToShoot = true;
                StartCoroutine(StartShooting());
            }
            
        }

        public void StopFire()
        {
            wantsToShoot = false;
            StopCoroutine(StartShooting());
        }
        
        private IEnumerator StartShooting()
        {
            while (wantsToShoot)
            {
                if(ammo.bullet == 0)
                {
                    yield return new WaitForSeconds(1.0f);
                    ammo.bullet = equipedWeapon.roundsInClip;
                    --ammo.clips;
                }
                Shoot();
                yield return new WaitForSeconds(1.0f / equipedWeapon.fireRatePerSecond);
            }
        }

        protected void Shoot()
        {
            var obj = Instantiate(equipedWeapon.projectile);
            obj.transform.position = transform.position + new Vector3(aimDirection.x, aimDirection.y, 0) * offset;

            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            obj.GetComponent<ProjectileComponent>().direction = aimDirection;


            var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), aimDirection, 100f, layerMask);
            if (hit.collider != null)
            {
                var hitbox = hit.collider.gameObject.GetComponent<HitBoxComponent>();
                if (hitbox)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red, 0.1f);
                    // hitbox.ApplyDamage(damage);
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green, 0.1f);
                }
            }
            --ammo.bullet;
            OnAmmoDataChange.Invoke(ammo);
        }
    }
}
