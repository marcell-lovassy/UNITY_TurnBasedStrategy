using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField]
    private Button endTurnButton;
    [SerializeField]
    private TextMeshProUGUI turnText;

    private void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
            UpdateTurnText();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
    }

    private void TurnSystem_OnTurnChanged()
    {
        UpdateTurnText();
    }

    private void UpdateTurnText()
    {
        turnText.text = $"TURN {TurnSystem.Instance.TurnNumber}";
    }
}
