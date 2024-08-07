namespace Enemy.EnemyAnimControllers
{
    public class RangeEnemyAnimController : EnemyAnimController
    {
        protected override void Start()
        {
            base.Start();
            _enemy.GetComponent<RangedEnemy>().OnRangeAttack += RangeAttack;
            _enemy.GetComponent<RangedEnemy>().OnEndRangeAttack += EndRangeAttack;
        }

        private void RangeAttack()
        {
            _animator.SetBool("IsRangeAttack", true);
        }

        private void EndRangeAttack()
        {
            _animator.SetBool("IsRangeAttack", false);
        }
    }
}