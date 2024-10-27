namespace Enemy.EnemyAnimControllers
{
    public class RushEnemyAnimController: EnemyAnimController
    {
        protected override void Start()
        {
            base.Start();
            _enemy.GetComponent<RushEnemy.RushEnemy>().OnJump += OnJump;
            _enemy.GetComponent<RushEnemy.RushEnemy>().OnEndJump += OnEndJump;
        }

        private void OnJump()
        {
            _animator.SetBool("IsJump", true);
        }

        private void OnEndJump()
        {
            _animator.SetBool("IsJump", false);
        }
    }
}