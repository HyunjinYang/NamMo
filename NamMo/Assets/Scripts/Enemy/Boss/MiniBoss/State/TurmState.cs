using System.Collections;
using Enemy.MelEnemy;
using System;
using System.Collections;
using UnityEngine;
namespace Enemy.Boss.MiniBoss.State
{
    public class TurmState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public TurmState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        public void Enter()
        {
            _MiniBossEnemy.StartTurm();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}