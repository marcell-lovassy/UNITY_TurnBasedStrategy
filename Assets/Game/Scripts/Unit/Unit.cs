using Game.Core.Grid;
using Game.UnitSystem.Actions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UnitSystem 
{
    public class Unit : MonoBehaviour
    {
        public static event UnityAction OnAnyActionPointsChanged;

        public MoveAction MoveAction => moveAction;
        public SpinAction SpinAction => spinAction;
        public BaseAction[] ActionArray => actionArray;
        public GridPosition GridPosition => gridPosition;
        public Vector3 WorldPosition => transform.position;
        public int AvailableActionPoints => actionPoints;
        public bool IsEnemy => isEnemy;

        [SerializeField]
        private bool isEnemy;

        private const int ACTION_POINTS_MAX = 2;

        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] actionArray;
        private HealthSystem healthSystem;
        private RagdollSpawner ragdollSpawner;
        private int actionPoints = ACTION_POINTS_MAX;

        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
            spinAction = GetComponent<SpinAction>();
            actionArray = GetComponents<BaseAction>();
            healthSystem = GetComponent<HealthSystem>();
            ragdollSpawner = GetComponent<RagdollSpawner>();
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

            healthSystem.OnDead += HealthSystem_OnDead;
        }

        private void HealthSystem_OnDead()
        {
            LevelGrid.Instance.ClearUnitAtGridPosition(this, gridPosition);
            Destroy(gameObject);
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

        private void TurnSystem_OnTurnChanged()
        {
            if((isEnemy && !TurnSystem.Instance.IsPlayerTurn) || !isEnemy && TurnSystem.Instance.IsPlayerTurn) 
            {
                actionPoints = ACTION_POINTS_MAX;
                OnAnyActionPointsChanged?.Invoke();
            }
        }


        private void SpendActionPoints(int amount) 
        {
            actionPoints -= amount;
            OnAnyActionPointsChanged?.Invoke();
        }

        internal void TakeDamage(int dmg)
        {
            healthSystem.TakeDamage(dmg);
        }
    }
}