using System;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    [Header("Movement Data")]

    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private CharacterController characterController;

    [SerializeField] private InputReader input;

    [SerializeField] private Transform Visuals;

    [SerializeField] private Animator animator;



    private Vector2 moveDirection;

    bool isJumping = false;
    private Camera mainCamera;

    private StateMachine stateMachine;


    private void Awake()
    {
        SetupStateMachine();
    }
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        LocomotionState locomotionState = new LocomotionState(this, animator);
        JumpState jumpState = new JumpState(this, animator);

        Any(locomotionState, new Func<bool>(() => characterController.isGrounded));

        stateMachine.SetState(locomotionState);
    }

    void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);

    void Update()
    {
        moveDirection = input.Direction;
        stateMachine.Update();
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
        Vector3 movement = (input.Direction.x * cameraRight + input.Direction.y * cameraForward) * moveSpeed * Time.deltaTime;

        // If there is movement input, rotate the character to face the movement direction
        if (input.Direction.sqrMagnitude > 0.01f)
        {
            Vector3 lookDirection = new Vector3(movement.x, 0, movement.z); // Flatten the movement vector
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                Visuals.rotation = Quaternion.Slerp(Visuals.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation
            }
        }

        // Move the character using the CharacterController
        characterController.Move(movement);
    }

    public void HandleCharacterRotation(){
        
    }
    public void HandleJump(){

    }
}