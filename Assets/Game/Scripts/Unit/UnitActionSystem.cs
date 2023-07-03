using Game.Core;
using Game.Core.Grid;
using Game.UnitSystem.Actions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.UnitSystem
{
    public class UnitActionSystem : MonoBehaviour
    {
        public event UnityAction OnSelectedUnitChanged;
        public event UnityAction OnSelectedActionChanged;
        public event UnityAction<bool> OnBusyChanged;
        public event UnityAction OnActionStarted;

        public static UnitActionSystem Instance { get; private set; }
        public Unit SelectedUnit => selectedUnit;
        public BaseAction SelectedAction => selectedAction;

        [SerializeField]
        private Unit selectedUnit;
        [SerializeField]
        private LayerMask unitLayer;

        private BaseAction selectedAction;
        private bool isBusy;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one UnitActionSystem! " + transform + " - " + Instance);
                Destroy(gameObject);
            }
            Instance = this;
        }

        private void Start()
        {
            SetSelectedUnit(selectedUnit);
        }

        private void Update()
        {
            if (isBusy) return;
            if (!TurnSystem.Instance.IsPlayerTurn) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (TryHandleUnitSelection()) return;

            HandleSelectedAction();
        }

        private void HandleSelectedAction()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 target = MouseWorldHelper.GetPosition();
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(target);

                if (!selectedAction.IsVaidActionGridPosition(mouseGridPosition)) 
                {
                    return;
                }
                if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction)) 
                {
                    return;
                }

                SetBusy();
                selectedAction.TakeAction(mouseGridPosition, ClearBusy);

                OnActionStarted?.Invoke();
            }
        }

        private void SetBusy()
        {
            isBusy = true;
            OnBusyChanged?.Invoke(isBusy);
        }

        private void ClearBusy()
        {
            isBusy = false;
            OnBusyChanged?.Invoke(isBusy);
        }

        private bool TryHandleUnitSelection()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayer))
                {
                    if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
                    {
                        //already selected
                        if (unit == selectedUnit) return false;

                        if (unit.IsEnemy) return false;

                        SetSelectedUnit(unit);
                        return true;
                    }
                }
            }
            return false;
        }

        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            SetSelectedAction(selectedUnit.MoveAction);
            OnSelectedUnitChanged?.Invoke();
        }

        public void SetSelectedAction(BaseAction action)
        {
            selectedAction = action;
            OnSelectedActionChanged?.Invoke();
        }
    }
}
