namespace Enemies.EnemyStateMachine
{
    public class EnemyBaseState : IState
    {
        protected EnemyController EnemyController;

        protected EnemyBaseState(EnemyController enemyController) {
            EnemyController = enemyController;
        }
        
        public virtual void OnEnter(){
            
        }

        public virtual void Update(){
            
        }

        public virtual void FixedUpdate(){
            
        }

        public virtual void OnExit(){
            
        }
    }
}