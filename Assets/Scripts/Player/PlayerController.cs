using System;
using System.Collections.Generic;
using Player;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    protected static readonly int SpeedHash = Animator.StringToHash("Speed");


    [Header("Movement Data")]

    [SerializeField] private float maximumSpeed = 5f;

    [SerializeField] private float jumpHeight = 1.0f;

    [SerializeField] private float acceleration = 5f;

    [SerializeField] private float rotationSpeed = 8f;
    private float gravityValue = -9.81f;

    [Header("References")]
    [SerializeField] private CharacterController characterController;

    [SerializeField] private InputReader input;

    [SerializeField] private InputBlocker blocker;

    [SerializeField] private PlayerTransform playerTransform;//Global player transform

    [SerializeField] private Transform Visuals;

    [SerializeField] private Animator animator;
    
    [SerializeField] private LockOnTarget lockOnTarget;

    [SerializeField] private CinemachineCamera CinemachineCamera;

    [Header("Attacking data")]

    [SerializeField] private PlayerAttackManager attackManager;

    private bool _lockOn = false;
    private bool isGrounded = false;
    private Vector2 moveDirection;

    private float verticalVelocity;

    private float currentVelocity = 0;
    private Camera mainCamera;

    private StateMachine stateMachine;

    private PlayerState defaultState;

    private AttackState attackState;

    #region Trigger Transitions

    private TriggerTransition attackTrigger;

    private TriggerTransition endAttackTrigger;

    private TriggerTransition dialogueTrigger;
    private TriggerTransition endDialogueTrigger;

    #endregion


    private void Awake()
    {
        SetupStateMachine();
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform.Transform = transform;
    }
    void Start()
    {
        mainCamera = Camera.main;
    }
    void OnEnable()
    {
        input.Jump += OnJump;
        input.Attack += OnAttack;
        input.Spell += OnSpell;
        input.Lock += OnLock;
    }
    void OnDisable()
    {
        input.Jump -= OnJump;
        input.Attack -= OnAttack;
        input.Spell -= OnSpell;
        input.Lock -= OnLock;
        

    }
    private void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        LocomotionState locomotionState = new LocomotionState(this, animator, PlayerStates.Locomotion);

        defaultState = locomotionState;//set default state for editor

        JumpState jumpState = new JumpState(this, animator, PlayerStates.Jumping);
        FallingState fallingState = new FallingState(this, animator, PlayerStates.Falling);

        DialogueState dialogueState = new DialogueState(this, animator, PlayerStates.Speaking);
        attackState = new AttackState(this, animator, PlayerStates.Attacking);
        attackManager.AssignAttackState(attackState);

        attackTrigger = new TriggerTransition(attackState);
        endAttackTrigger = new TriggerTransition(locomotionState);

        dialogueTrigger = new TriggerTransition(dialogueState);
        endDialogueTrigger = new TriggerTransition(locomotionState);

        attackManager.OnComboEnd += () => endAttackTrigger.Trigger();

        At(fallingState, locomotionState, new Func<bool>(() => isGrounded));
        At(locomotionState, fallingState, new Func<bool>(() => verticalVelocity < -0.3 && !isGrounded));
        At(locomotionState, jumpState, new Func<bool>(() => verticalVelocity > 0 && !isGrounded));
        At(jumpState, fallingState, new Func<bool>(() => verticalVelocity < -0.3 && !isGrounded));

        At(locomotionState, attackState, attackTrigger.Condition);
        At(attackState, locomotionState, endAttackTrigger.Condition);

        Any(dialogueState, dialogueTrigger.Condition);
        At(dialogueState, locomotionState, endDialogueTrigger.Condition);




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
        Vector3 target = lockOnTarget.target ? lockOnTarget.target.position : transform.position;
        
        Vector3 direction = target - CinemachineCamera.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        CinemachineCamera.transform.rotation = lookRotation;
        
    }

    public void HandleMovement()
    {
        if (blocker.isBlocked) return;

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
            var lookDirection = new Vector3(movement.x, 0, movement.z); // Flatten the movement vector   

            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                Visuals.rotation = Quaternion.Slerp(Visuals.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed); // Smooth rotation
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

    public void HandleRunSpeed()
    {
        animator.SetFloat(SpeedHash, currentVelocity / maximumSpeed, 0.05f, Time.fixedDeltaTime);
    }


    private void OnJump()
    {
        if (blocker.isBlocked) return;

        if (characterController.isGrounded)
        {
            // Calculate the jump velocity using the correct formula
            verticalVelocity = Mathf.Sqrt(2f * jumpHeight * -gravityValue);
        }
    }

    private void OnAttack()
    {
        if (blocker.isBlocked) return;
        if (attackState != null && stateMachine.Current == attackState) { attackState.TryQueueAttack(); }
        else
        {
            attackTrigger.Trigger();
            attackState.ChangeStartAction(AttackType.Attack);
        }

    }

    private void OnSpell()
    {
        if (blocker.isBlocked) return;

        if (attackState != null && stateMachine.Current == attackState) { attackState.TryQueueSpell(); }
        else
        {
            attackState.ChangeStartAction(AttackType.Spell);
            attackTrigger.Trigger();
        }


    }

    public void SetDefaultState()
    {
        if (stateMachine == null || defaultState == null) return;
        stateMachine.SetState(defaultState);
    }

    public void UpdateDialogueState(bool inDialogue)
    {
        if (inDialogue) dialogueTrigger.Trigger();
        else endDialogueTrigger.Trigger();
    }

    private void OnLock()
    {
        _lockOn = true;
    }

    
}