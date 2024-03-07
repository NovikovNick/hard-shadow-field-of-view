using UnityEngine;
using UnityEngine.Assertions;


namespace m8t
{
    public class PlayerController : MonoBehaviour, ICameraObservable
    {
        [Header("--------- Mandatory injections --------- ")]
        [SerializeField] private PlatformerMovement movement;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private CharacterEvents characterEvents;

        [Header("--------- State --------- ")]
        [SerializeField] private Vector2 mouseDir2d;

        // infrastructure
        private PlayerInputHandler inputHandler;

        void Start()
        {
            inputHandler = PlayerInputHandler.Instance;

            Assert.IsNotNull(movement);
            Assert.IsNotNull(healthComponent);
            Assert.IsNotNull(weaponComponent);
            Assert.IsNotNull(characterEvents);
            Assert.IsNotNull(inputHandler);
        }

        void FixedUpdate()
        {
            if (healthComponent.IsDead) return;

            movement.AddInput(inputHandler.MoveInput.x, inputHandler.JumpInput);
        }


        void Update()
        {
            if (healthComponent.IsDead)
            {
                weaponComponent.StopFire();
                characterEvents.OnDeath.Invoke();
                return;
            }

            if (inputHandler.LookAtInput != Vector2.zero)
            {
                Vector3 dir3d = Camera.main.ScreenToWorldPoint(inputHandler.LookAtInput) - transform.position;
                mouseDir2d = new Vector2(dir3d.x, dir3d.y).normalized;
                characterEvents.OnLookAt.Invoke(mouseDir2d);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                weaponComponent.StartFire();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                weaponComponent.StopFire();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                characterEvents.OnDeath.Invoke();
            }
        }

        public Vector3 Target()
        {
            return transform.position;
        }

        public float TargetVelocityByX()
        {
            return movement.Velocity.x;
        }
    }
}

