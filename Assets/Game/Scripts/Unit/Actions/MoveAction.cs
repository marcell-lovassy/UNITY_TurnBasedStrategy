using Game.Core.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UnitSystem.Actions
{
    public class MoveAction : BaseAction
    {
        public event UnityAction OnStartMoving;
        public event UnityAction OnStopMoving;

        [Header("Movement Properties")]
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private int maxMoveDistance;
        [SerializeField]
        private float stoppingDistance;
        [SerializeField]
        private float rotationSpeed;

        private Vector3 targetPosition;

        protected override void Awake()
        {
            base.Awake();
            actionName = "Move";
            targetPosition = transform.position;
        }

        void Update()
        {
            if (!isActive) return;


            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                transform.position += moveDirection * Time.deltaTime * moveSpeed;

            }
            else
            {

                OnStopMoving?.Invoke();
                ActionComplete();
            }

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }


        public override void TakeAction(GridPosition targetPosition, Action onMoveComplete)
        {
            ActionStart(onMoveComplete);
            this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);

            OnStartMoving?.Invoke();
        }

        public override List<GridPosition> GetValidActionGridPositions()
        {
            List<GridPosition> validGridPositions = new List<GridPosition>();

            GridPosition unitGridPosition = unit.GridPosition;

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
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
            return LevelGrid.Instance.IsValidGridPosition(possiblePosition)
                        && !LevelGrid.Instance.HasUnitOnGridPosition(possiblePosition)
                        && possiblePosition != currentPosition;
        }

        public override string GetActionName()
        {
            return actionName;
        }
    }
}