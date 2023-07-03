using Game.Core.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UnitSystem.Actions
{
    public class ShootAction : BaseAction
    {
        [SerializeField]
        private int maxShootDistance;

        private float totalSpinDegrees;



        protected override void Awake()
        {
            base.Awake();
            actionName = "Shoot";
        }

        private void Update()
        {
            if (!isActive) return;

            float spinDegrees = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinDegrees, 0);

            totalSpinDegrees += spinDegrees;

            if (totalSpinDegrees >= 360f)
            {
                isActive = false;
                actionCompletedCallback?.Invoke();
            }
        }

        public override void TakeAction(GridPosition gridPosition, Action onSpinComplete)
        {
            actionCompletedCallback = onSpinComplete;
            totalSpinDegrees = 0;
            isActive = true;
        }

        public override string GetActionName()
        {
            return actionName;
        }

        public override List<GridPosition> GetValidActionGridPositions()
        {
            List<GridPosition> validGridPositions = new List<GridPosition>();

            GridPosition unitGridPosition = unit.GridPosition;

            for (int x = -maxShootDistance; x <= maxShootDistance; x++)
            {
                for (int z = -maxShootDistance; z <= maxShootDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (IsActionValidOnPosition(unitGridPosition, testGridPosition))
                    {
                        validGridPositions.Add(testGridPosition);
                    }
                }
            }

            return validGridPositions;
        }

        private bool IsActionValidOnPosition(GridPosition currentPosition, GridPosition possiblePosition)
        {
            if (!LevelGrid.Instance.IsValidGridPosition(possiblePosition)) return false;
            var unitAtPosition = LevelGrid.Instance.GetUnitAtGridPosition(possiblePosition);
            if (unitAtPosition == null) return false;

            //check if the "team is different" so the enemy can shoot the player and the player can shoot the enemy
            return unitAtPosition.IsEnemy != unit.IsEnemy;
        }
    }
}