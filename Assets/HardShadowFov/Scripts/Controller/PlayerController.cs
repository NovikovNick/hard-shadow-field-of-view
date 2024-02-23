using UnityEngine;
using UnityEngine.Assertions;


namespace m8t
{
    [System.Serializable]
    public class PlayerController : MonoBehaviour, ICameraObservable
    {
        [Header("Mandatory injections")]
        [SerializeField] private PlatformerMovement movement;

        // singletons
        private PlayerInputHandler inputHandler;

        // state
        private Vector2 mouse2d = Vector2.right;

        void Start()
        {
            inputHandler = PlayerInputHandler.Instance;

            Assert.IsNotNull(inputHandler);
            Assert.IsNotNull(movement);
        }

        void FixedUpdate()
        {
            movement.AddInput(inputHandler.MoveInput.x, inputHandler.JumpInput);
        }

        void Update()
        {
            var playerPawnPosition2d = new Vector2(transform.position.x, transform.position.y);

            if (inputHandler.LookAtInput != Vector2.zero)
            {
                var mouse3d = Camera.main.ScreenToWorldPoint(inputHandler.LookAtInput);
                mouse2d = (new Vector2(mouse3d.x, mouse3d.y) - playerPawnPosition2d).normalized;
            }

            if (inputHandler.LookToInput != Vector2.zero)
            {
                mouse2d = inputHandler.LookToInput.normalized;
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

