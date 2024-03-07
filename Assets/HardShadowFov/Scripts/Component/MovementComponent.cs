using UnityEngine;
using UnityEngine.Assertions;

namespace m8t
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformerMovement : MonoBehaviour
    {
        [Header("--------- Mandatory injections ---------")]
        [SerializeField] private Animator anim;

        [Header("--------- General settings ---------")]
        [SerializeField] private float horizontalInput;
        [SerializeField] private float speed = 15f;
        [SerializeField] private float jumpForce = 150f;
        [SerializeField] private int maxJumps = 2;
        [SerializeField] private LayerMask groundLayerMask;

        [Header("--------- Ground Check Settings ---------")]
        [SerializeField] private Vector2 onGroundBoxCastSize;
        [SerializeField] private Vector2 onGroundBoxPositionOffset;
        [SerializeField] private float onGroundBoxCastDistance;

        [Header("State")]
        [SerializeField] private int jumps = 0;

        // Adjacement components
        private Rigidbody2D rb;

        public Vector2 Velocity
        {
            get { return rb.velocity; }
            private set { rb.velocity = value; }
        }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            Assert.IsNotNull(rb);
            Assert.IsNotNull(anim);
        }

        public void AddInput(float horInput, bool jumpPressed)
        {
            if (!enabled)
            {
                return;
            }
            if (OnGround())
            {
                jumps = 0;
            }

            rb.velocity = new Vector2(horInput * speed, rb.velocity.y);

            if (jumpPressed && jumps < maxJumps)
            {
                ++jumps;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        private void LateUpdate()
        {
            anim.SetInteger("xVelocity", (int) Velocity.x);
        }

        private bool OnGround()
        {
            var pos2d = new Vector2(transform.position.x, transform.position.y);
            var hit = Physics2D.BoxCast(pos2d + onGroundBoxPositionOffset,
                                        onGroundBoxCastSize,
                                        0f,
                                        Vector2.down,
                                        onGroundBoxCastDistance,
                                        groundLayerMask);
            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            var position3d = new Vector3(
                transform.position.x + onGroundBoxPositionOffset.x,
                transform.position.y + onGroundBoxPositionOffset.y - onGroundBoxCastDistance,
                transform.position.z
            );

            var size3d = new Vector3(
                onGroundBoxCastSize.x,
                onGroundBoxCastSize.y,
                0
            );

            Gizmos.DrawWireCube(position3d, size3d);
        }
    }
}