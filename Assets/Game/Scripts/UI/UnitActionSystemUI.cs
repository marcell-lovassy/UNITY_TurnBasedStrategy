using Game.UnitSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField]
        private Transform actionButtonPrefab;
        [SerializeField]
        private Transform actionButtonContainer;

        private List<ActionButtonUI> actionButtons = new List<ActionButtonUI>();

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += OnActionChanged;
            CreateUnitActionButtons();
            UpdateSelectedVisual();
        }


        private void OnUnitChanged()
        {
            CreateUnitActionButtons();
            UpdateSelectedVisual();
        }
        private void OnActionChanged()
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
    }
}