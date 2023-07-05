using Game.Core.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UnitSystem.Actions 
{
    public class SpinAction : BaseAction
    {
        private float totalSpinDegrees;

        protected override void Awake()
        {
            base.Awake();
            actionName = "Spin";
        }

        private void Update()
        {
            if (!isActive) return;
                
            float spinDegrees = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinDegrees, 0);

            totalSpinDegrees += spinDegrees;

            if(totalSpinDegrees >= 360f)
            {
                ActionComplete();
            }
        }

        public override void TakeAction(GridPosition gridPosition, Action onSpinComplete)
        {
            ActionStart(onSpinComplete);
            totalSpinDegrees = 0;
        }

        public override string GetActionName()
        {
            return actionName;
        }

        public override List<GridPosition> GetValidActionGridPositions()
        {
            GridPosition unitGridPosition = unit.GridPosition;
            return new List<GridPosition> { unitGridPosition };
        }
    }
}