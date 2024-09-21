using System;
using Enemy.Boss.MiniBoss;

namespace Enemy.EnemyAnimControllers
{
    public class MiniBossAnimController: EnemyAnimController
    {
        protected override void Start()
        {
            base.Start();
            _enemy.GetComponent<MiniBossEnemy>().OnAttack2 += OnAttack2;
            _enemy.GetComponent<MiniBossEnemy>().OnDashAttack += OnDashAttack;
            _enemy.GetComponent<MiniBossEnemy>().OnWaveAttack += OnWaveAttack;
            _enemy.GetComponent<MiniBossEnemy>().OnEndAttack2 += OnEndAttack2;
            _enemy.GetComponent<MiniBossEnemy>().OnEndDashAttack += OnEndDashAttack;
            _enemy.GetComponent<MiniBossEnemy>().OnEndWaveAttack += OnEndWaveAttack;
            _enemy.GetComponent<MiniBossEnemy>().OnChangePhase += OnChangePhase;
            _enemy.GetComponent<MiniBossEnemy>().OnEndChangePhase += OnEndChangePhase;
            _enemy.GetComponent<MiniBossEnemy>().OnHealthRecovery += OnHealthRecovery;
            _enemy.GetComponent<MiniBossEnemy>().OnEndHealthRecovery += OnEndHealthRecovery;
            _enemy.GetComponent<MiniBossEnemy>().OnLandAttack += OnLandAttack;
            _enemy.GetComponent<MiniBossEnemy>().OnEndLandAttack += OnEndLandAttack;
        }


        private void OnAttack2()
        {
            _animator.SetBool("IsAttack2", true);
        }

        private void OnDashAttack()
        {
            _animator.SetBool("IsDashAttack", true);
        }

        private void OnWaveAttack()
        {
            _animator.SetBool("IsLandAttack", true);
        }

        private void OnEndAttack2()
        {
            _animator.SetBool("IsAttack2", false);
        }

        private void OnEndDashAttack()
        {
            _animator.SetBool("IsDashAttack", false);
        }

        private void OnEndWaveAttack()
        {
            _animator.SetBool("IsLandAttack", false);
        }

        private void OnChangePhase()
        {
            _animator.SetBool("IsChangePhase", true);
        }
     
        private void OnEndChangePhase()
        {
            _animator.SetBool("IsChangePhase", false);
        }

        private void OnHealthRecovery()
        {
            _animator.SetBool("IsHealthRecovery", true);
        }
        private void OnEndHealthRecovery()
        {
            _animator.SetBool("IsHealthRecovery", false);
        }

        private void OnLandAttack()
        {
            _animator.SetBool("IsLandAttack", true);
        }

        private void OnEndLandAttack()
        {
            _animator.SetBool("IsLandAttack", false);
        }
    }
}