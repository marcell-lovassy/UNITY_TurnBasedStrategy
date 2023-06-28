using Game.Core.Grid;
using Game.UnitSystem.Actions;
using UnityEngine;

namespace Game.UnitSystem 
{
    [RequireComponent(typeof(MoveAction))]
    public class Unit : MonoBehaviour
    {
        public MoveAction MoveAction => moveAction;

        private GridPosition gridPosition;

        private MoveAction moveAction;

        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);   
        }

        private void Update()
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if(newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }
    }
}