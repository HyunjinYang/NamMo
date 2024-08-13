namespace Enemy.EnemyAnimControllers
{
    public class MelEnemyAnimController: EnemyAnimController
    {
        protected override void Start()
        {
            base.Start();
            _enemy.GetComponent<MelEnemy.MelEnemy>().OnDownAttack += DownAttack;
            _enemy.GetComponent<MelEnemy.MelEnemy>().OnEndDownAttack += EndDownAttack;
        }

        private void DownAttack()
        {
            _animator.SetBool("IsDownAttack", true);
        }

        private void EndDownAttack()
        {
            _animator.SetBool("IsDownAttack", false);
        }
    }
}