using Game.UnitSystem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField]
        private Transform actionButtonPrefab;
        [SerializeField]
        private Transform actionButtonContainer;
        [SerializeField]
        private TextMeshProUGUI actionPointsText;

        private List<ActionButtonUI> actionButtons = new List<ActionButtonUI>();

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
            //TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;

            CreateUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }

        private void Unit_OnAnyActionPointsChanged()
        {
            UpdateActionPoints();
        }

        //private void TurnSystem_OnTurnChanged()
        //{
        //    UpdateActionPoints();
        //}

        private void UnitActionSystem_OnActionStarted()
        {
            UpdateActionPoints();
        }

        private void UnitActionSystem_OnSelectedUnitChanged()
        {
            CreateUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }

        private void UnitActionSystem_OnSelectedActionChanged()
        {
            UpdateSelectedVisual();
        }

        private void CreateUnitActionButtons()
        {
            foreach (Transform button in actionButtonContainer)
            {
                Destroy(button.gameObject);
            }

            actionButtons.Clear();

            Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;

            foreach (var action in selectedUnit.ActionArray)
            {
                Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainer);
                ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetAction(action);
                actionButtons.Add(actionButtonUI);
            }
        }

        private void UpdateSelectedVisual()
        {
            foreach (var actionButton in actionButtons)
            {
                actionButton.UpdateSelectedVisual();
            }
        }

        private void UpdateActionPoints() 
        {
            Unit unit = UnitActionSystem.Instance.SelectedUnit;
            actionPointsText.text = $"Action Points: {unit.AvailableActionPoints}";
        }
    }
}