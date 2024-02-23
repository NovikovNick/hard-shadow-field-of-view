using UnityEngine;
using UnityEngine.InputSystem;

namespace m8t
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Input action asset")]
        [SerializeField] private InputActionAsset inputActionsAsset;

        [Header("Action Map Name References")]
        [SerializeField] private string actionMapName = "Player";

        [Header("Action Name References")]
        [SerializeField] private string move = "Move";
        [SerializeField] private string lookTo = "LookTo";
        [SerializeField] private string lookAt = "LookAt";
        [SerializeField] private string jump = "Jump";

        private InputAction moveAction;
        private InputAction lookToAction;
        private InputAction lookAtAction;
        private InputAction jumpAction;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookToInput { get; private set; }
        public Vector2 LookAtInput { get; private set; }

        private bool jumpInput_;
        public bool JumpInput
        {
            get
            {
                bool oldValue = jumpInput_;
                jumpInput_ = false;
                return oldValue;
            }
            private set => jumpInput_ = value;
        }

        public static PlayerInputHandler Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            moveAction = inputActionsAsset.FindActionMap(actionMapName).FindAction(move);
            lookToAction = inputActionsAsset.FindActionMap(actionMapName).FindAction(lookTo);
            lookAtAction = inputActionsAsset.FindActionMap(actionMapName).FindAction(lookAt);
            jumpAction = inputActionsAsset.FindActionMap(actionMapName).FindAction(jump);

            moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            moveAction.canceled += ctx => MoveInput = Vector2.zero;

            lookToAction.performed += ctx => LookToInput = ctx.ReadValue<Vector2>();
            lookToAction.canceled += ctx => LookToInput = Vector2.zero;

            lookAtAction.performed += ctx => LookAtInput = ctx.ReadValue<Vector2>();
            lookAtAction.canceled += ctx => LookAtInput = Vector2.zero;

            jumpAction.performed += ctx => JumpInput = ctx.performed;
            jumpAction.canceled += ctx => JumpInput = false;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            lookToAction.Enable();
            lookAtAction.Enable();
            jumpAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookToAction.Disable();
            lookAtAction.Disable();
            jumpAction.Disable();
        }
    }

}