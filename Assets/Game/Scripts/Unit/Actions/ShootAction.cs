using Game.Core.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UnitSystem.Actions
{
    public class ShootAction : BaseAction
    {
        public event EventHandler<ShootEventArgs> OnShoot;

        public class ShootEventArgs : EventArgs 
        {
            public Unit targetUnit;
            public Unit shootingUnit;
        }

        private enum ShootingState
        {
            Aiming,
            Shooting,
            Cooloff,
        }

        [SerializeField]
        private int maxShootDistance;

        private ShootingState state;
        private float stateTimer;
        private float aimingStateTime = 1f;
        private float shootingStateTime = 0.1f;
        private float coolOffStateTime = 0.5f;
        private float aimRotationSpeed = 10f;

        private bool canShootBullet;
        private Unit targetUnit;


        protected override void Awake()
        {
            base.Awake();
            actionName = "Shoot";
        }

        private void Update()
        {
            if (!isActive) return;

            stateTimer -= Time.deltaTime;

            switch (state)
            {
                case ShootingState.Aiming:
                    Vector3 aimDirection = (targetUnit.WorldPosition - unit.WorldPosition).normalized;
                    transform.forward = Vector3.Lerp(transform.forward, aimDirection, aimRotationSpeed * Time.deltaTime);
                    break;
                case ShootingState.Shooting:
                    if (canShootBullet) 
                    {
                        Shoot();
                        canShootBullet = false;
                    }
                    break;
                case ShootingState.Cooloff:
                    break;
            }

            if (stateTimer <= 0f) 
            {
                NextState();
            }
        }

        private void Shoot()
        {
            OnShoot?.Invoke(this, new ShootEventArgs() { shootingUnit = unit, targetUnit = targetUnit });
            targetUnit.TakeDamage(40);
        }

        private void NextState()
        {
            switch (state)
            {
                case ShootingState.Aiming:
                    state = ShootingState.Shooting;
                    stateTimer = shootingStateTime;
                    break;
                case ShootingState.Shooting:
                    state = ShootingState.Cooloff;
                    stateTimer = coolOffStateTime;
                    break;
                case ShootingState.Cooloff:
                    ActionComplete();
                    break;
            }
        }

        public override void TakeAction(GridPosition gridPosition, Action onShootComplete)
        {
            ActionStart(onShootComplete);
            state = ShootingState.Aiming;
            stateTimer = aimingStateTime;

            targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
            canShootBullet = true;
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

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                    if(testDistance > maxShootDistance + 1) 
                    {
                        continue;
                    }

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
            var unitAtPosition = LevelGrid.Instance.GetUnitAtGridPosition(possiblePosition);
            if (unitAtPosition == null) return false;

            //check if the "team is different" so the enemy can shoot the player and the player can shoot the enemy
            return unitAtPosition.IsEnemy != unit.IsEnemy;
        }
    }
}