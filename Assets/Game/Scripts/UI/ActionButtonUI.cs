using Game.UnitSystem;
using Game.UnitSystem.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI 
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI actionNameText;
        [SerializeField]
        private TextMeshProUGUI actionCostText;
        [SerializeField]
        private Button button;
        [SerializeField]
        private GameObject selectionVisual;

        private BaseAction action;

        public void SetAction(BaseAction action)
        {
            this.action = action;
            actionNameText.text = action.GetActionName().ToUpper();
            actionCostText.text = action.ActionPointCost.ToString();
            button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(action);
            });
        }

        public void UpdateSelectedVisual()
        {
            BaseAction selectedAction = UnitActionSystem.Instance.SelectedAction;
            selectionVisual.SetActive(action == selectedAction);
        }
    }
}