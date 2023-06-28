using Game.Core.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UnitSystem.Actions
{
    public class MoveAction : MonoBehaviour
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

        private void Awake()
        {
            targetPosition = transform.position;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * Time.deltaTime * moveSpeed;

                transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);

                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }


        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        public List<GridPosition> GetValidActionGridPositions()
        {
            List<GridPosition> validGridPositions = new List<GridPosition>();

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                }
            }

            return validGridPositions;
        }
    }
}