using Game.Core.Grid;
using Game.UnitSystem.Actions;
using UnityEngine;

namespace Game.UnitSystem 
{
    [RequireComponent(typeof(MoveAction))]
    public class Unit : MonoBehaviour
    {
        public MoveAction MoveAction => moveAction;
        public SpinAction SpinAction => spinAction;
        public BaseAction[] ActionArray => actionArray;
        public GridPosition GridPosition => gridPosition;

        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] actionArray;

        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
            spinAction = GetComponent<SpinAction>();

            actionArray = GetComponents<BaseAction>();
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