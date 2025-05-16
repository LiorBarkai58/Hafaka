using System;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    protected static readonly int SpeedHash = Animator.StringToHash("Speed");


    [Header("Movement Data")]

    [SerializeField] private float maximumSpeed = 5f;

    [SerializeField] private float jumpHeight = 1.0f;

    [SerializeField] private float acceleration = 5f;
    private float gravityValue = -9.81f;

    [Header("References")]
    [SerializeField] private CharacterController characterController;

    [SerializeField] private InputReader input;

    [SerializeField] private Transform Visuals;

    [SerializeField] private Animator animator;

    private bool isGrounded = false;
    private Vector2 moveDirection;

    private float verticalVelocity;

    private float currentVelocity = 0;
    private Camera mainCamera;

    private StateMachine stateMachine;

    #region Trigger Transitions

    private TriggerTransition attackTrigger;

    private TriggerTransition endAttackTrigger;
    #endregion


    private void Awake()
    {
        SetupStateMachine();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        mainCamera = Camera.main;
    }
    void OnEnable(){
        input.Jump += OnJump;
        input.Attack += OnAttack;
    }
    void OnDisable(){
        input.Jump -= OnJump;
        input.Attack -= OnAttack;
    }
    private void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        LocomotionState locomotionState = new LocomotionState(this, animator);
        JumpState jumpState = new JumpState(this, animator);
        FallingState fallingState = new FallingState(this, animator);
        AttackState attackState = new AttackState(this, animator);

        attackTrigger = new TriggerTransition(attackState);
        endAttackTrigger = new TriggerTransition(locomotionState);

        At(fallingState, locomotionState, new Func<bool>(() => isGrounded));
        At(locomotionState, fallingState, new Func<bool>(() => verticalVelocity < -0.3 && !isGrounded));
        At(locomotionState, jumpState, new Func<bool>(() => verticalVelocity > 0 && !isGrounded));
        At(jumpState, fallingState, new Func<bool>(() => verticalVelocity < -0.3 && !isGrounded));
        At(locomotionState, attackState, attackTrigger.Condition);
        At(attackState, locomotionState, endAttackTrigger.Condition);


        
        
        // At(locomotionState, attackState,)


        stateMachine.SetState(locomotionState);
    }

    void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);

    void Update()
    {
        moveDirection = input.Direction;
        stateMachine.Update();
        isGrounded = characterController.isGrounded;
        
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void HandleMovement()
    {
        // Get the camera's forward and right directions
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Flatten the camera's forward and right vectors to the horizontal plane
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to ensure consistent movement speed
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on input and camera orientation
        currentVelocity = Mathf.Lerp(currentVelocity, maximumSpeed * Mathf.Clamp01(moveDirection.magnitude), Time.fixedDeltaTime * acceleration); // Calculate the current velocity based on input magnitude
        Vector3 movement = (input.Direction.x * cameraRight + input.Direction.y * cameraForward) * currentVelocity * Time.fixedDeltaTime;
        movement.y = verticalVelocity * Time.deltaTime;
        // If there is movement input, rotate the character to face the movement direction
        if (input.Direction.sqrMagnitude > 0.01f)
        {
            Vector3 lookDirection = new Vector3(movement.x, 0, movement.z); // Flatten the movement vector
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                Visuals.rotation = Quaternion.Slerp(Visuals.rotation, targetRotation, Time.deltaTime * 6f); // Smooth rotation
            }
        }
        // Move the character using the CharacterController
        characterController.Move(movement);
    }

    public void HandleGravity(float gravityMultiplier = 1)
    {
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -0.1f; // Small value to keep the character grounded
        }
        else
        {
            verticalVelocity += gravityValue * gravityMultiplier * Time.deltaTime; // Apply gravity
        }
    }

    public void HandleRunSpeed(){
        animator.SetFloat(SpeedHash, currentVelocity/maximumSpeed, 0.05f, Time.fixedDeltaTime);
    }
    

    private void OnJump()
    {
        if (characterController.isGrounded)
        {
            // Calculate the jump velocity using the correct formula
            verticalVelocity = Mathf.Sqrt(2f * jumpHeight * -gravityValue);
        }
    }

    private void OnAttack(){
        attackTrigger.Trigger();
    }

    public void AttackStart(){
        attackTrigger.Reset();
    }

    public void OnAttackEnd(){
        endAttackTrigger.Trigger();
    }

    
}