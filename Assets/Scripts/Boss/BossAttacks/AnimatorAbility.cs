using UnityEngine;

namespace Boss.BossAttacks
{
    public class AnimatorAbility: BossAbility
    {
        [SerializeField] private Animator animator;
        protected static readonly int ActivateHash = Animator.StringToHash("Activate");

        public override void Activate()
        {
            base.Activate();
            animator.SetTrigger(ActivateHash);
        }
    }
}