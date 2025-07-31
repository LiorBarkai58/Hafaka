namespace Player.PlayerStateMachine
{
    using UnityEngine;

    public class DashState : PlayerState
    {
        public DashState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier) : base(playerController, animator, stateIdentifier)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Dash State Entered");
        }
        public override void FixedUpdate()
        {
            playerController.HandleDash(Time.fixedDeltaTime);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}