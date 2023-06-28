using Game.Core;
using Game.Core.Grid;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UnitSystem
{
    public class UnitActionSystem : MonoBehaviour
    {
        public event UnityAction OnSelectedUnitChanged;

        public static UnitActionSystem Instance { get; private set; }
        public Unit SelectedUnit => selectedUnit;

        [SerializeField]
        private Unit selectedUnit;
        [SerializeField]
        private LayerMask unitLayer;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one UnitActionSystem! " + transform + " - " + Instance);
                Destroy(gameObject);
            }
            Instance = this;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TryUnitSelection()) return;
                SetUnitMovement();
            }
        }

        private bool TryUnitSelection()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayer))
            {
                if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
            return false;
        }

        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke();
        }

        private void SetUnitMovement()
        {
            if (selectedUnit != null)
            {
                Vector3 target = MouseWorldHelper.GetPosition();
                GridPosition mouseGridPositzion = LevelGrid.Instance.GetGridPosition(target);

                if (selectedUnit.MoveAction.IsVaidActionGridPosition(mouseGridPositzion))
                {
                    Debug.Log("Move");
                    selectedUnit.MoveAction.Move(mouseGridPositzion);
                }
            }
        }
    }
}
