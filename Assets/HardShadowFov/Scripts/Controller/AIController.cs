using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;


namespace m8t
{
    public class AIController : MonoBehaviour
    {
        [Header("--------- Mandatory injections --------- ")]
        [SerializeField] private PlatformerMovement movement;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private CharacterEvents characterEvents;

        [Header("--------- State --------- ")]
        [SerializeField] private Vector2 aimDirection = new Vector2(-1, 0);


        void Start()
        {
            Assert.IsNotNull(movement);
            Assert.IsNotNull(healthComponent);
            Assert.IsNotNull(weaponComponent);
            Assert.IsNotNull(characterEvents);

            StartCoroutine(StartAI());
        }

        private void Update()
        {
            if (healthComponent.IsDead)
            {
                characterEvents.OnDeath.Invoke();
                return;
            }
            characterEvents.OnLookAt.Invoke(aimDirection);
        }

        private IEnumerator StartAI()
        {
            int dir = 1;
            while (!healthComponent.IsDead)
            {
                yield return new WaitForSeconds(2f);

                for (int i = 0; i < 100; i++)
                {
                    movement.AddInput(dir, false);
                    yield return null;
                }

                movement.AddInput(0, true);
                yield return null;

                weaponComponent.StartFire();
                yield return new WaitForSeconds(0.5f);
                weaponComponent.StopFire();

                movement.AddInput(0, true);
                yield return null;

                dir = -dir;
                aimDirection = new Vector2(dir, 0);
            }
        }
    }
}
