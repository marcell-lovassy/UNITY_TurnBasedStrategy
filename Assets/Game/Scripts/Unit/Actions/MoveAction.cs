using Game.Core.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UnitSystem.Actions
{
    public class MoveAction : BaseAction
    {
        [Header("Movement Properties")]
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private int maxMoveDistance;
        [SerializeField]
        private float stoppingDistance;
        [SerializeField]
        private float rotationSpeed;

        [Space]
        [SerializeField]
        private Animator animator;

        private Vector3 targetPosition;

        protected override void Awake()
        {
            base.Awake();
            targetPosition = transform.position;
        }

        void Update()
        {
            if (!isActive) return;


            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                transform.position += moveDirection * Time.deltaTime * moveSpeed;
                animator.SetBool("IsMoving", true);

            }
            else
            {
                animator.SetBool("IsMoving", false);
                actionCompletedCallback?.Invoke();
                isActive = false;
            }

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }


        public void Move(GridPosition targetPosition, Action moveCompleted)
        {
            actionCompletedCallback = moveCompleted;
            this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
            isActive = true;
        }

        public bool IsVaidActionGridPosition(GridPosition gridPosition) 
        {
            var validGridPositionList = GetValidActionGridPositions();
            return validGridPositionList.Contains(gridPosition);
        }

        public List<GridPosition> GetValidActionGridPositions()
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
    }
}