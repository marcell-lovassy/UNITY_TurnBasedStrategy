using Game.Core.Grid;
using Game.UnitSystem.Actions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UnitSystem 
{
    [RequireComponent(typeof(MoveAction))]
    public class Unit : MonoBehaviour
    {
        public static event UnityAction OnAnyActionPointsChanged;

        public MoveAction MoveAction => moveAction;
        public SpinAction SpinAction => spinAction;
        public BaseAction[] ActionArray => actionArray;
        public GridPosition GridPosition => gridPosition;
        public int AvailableActionPoints => actionPoints;
        public bool IsEnemy => isEnemy;

        [SerializeField]
        private bool isEnemy;

        private const int ACTION_POINTS_MAX = 2;

        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] actionArray;
        private int actionPoints = ACTION_POINTS_MAX;

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
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
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

        private void TurnSystem_OnTurnChanged()
        {
            if((isEnemy && !TurnSystem.Instance.IsPlayerTurn) || !isEnemy && TurnSystem.Instance.IsPlayerTurn) 
            {
                actionPoints = ACTION_POINTS_MAX;
                OnAnyActionPointsChanged?.Invoke();
            }
        }

        public bool TrySpendActionPointsToTakeAction(BaseAction action) 
        {
            if (CanSpendActionPointsToTakeAction(action)) 
            {
                SpendActionPoints(action.GetActionPointCost());
                return true;
            }

            return false;
        }

        public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
        {
            return actionPoints >= baseAction.GetActionPointCost();
        }

        private void SpendActionPoints(int amount) 
        {
            actionPoints -= amount;
            OnAnyActionPointsChanged?.Invoke();
        }
    }
}