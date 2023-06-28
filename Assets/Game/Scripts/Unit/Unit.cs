using Game.Core.Grid;
using UnityEngine;

namespace Game.UnitSystem 
{
    public class Unit : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float rotationSpeed;
        [SerializeField]
        private float stoppingDistance;
        [SerializeField]
        private Animator animator;

        private Vector3 targetPosition;
        private GridPosition gridPosition;

        private void Awake()
        {
            targetPosition = transform.position;
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);   
        }

        private void Update()
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

            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if(newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }

        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }

}
