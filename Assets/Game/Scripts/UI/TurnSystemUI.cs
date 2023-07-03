using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField]
    private Button endTurnButton;
    [SerializeField]
    private TextMeshProUGUI turnText;
    [SerializeField]
    private GameObject enemyTurnVisual;

    private void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
            UpdateTurnText();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnVisibility();
    }

    private void TurnSystem_OnTurnChanged()
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnVisibility();
    }

    private void UpdateTurnText()
    {
        turnText.text = $"TURN {TurnSystem.Instance.TurnNumber}";
    }

    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisual.SetActive(!TurnSystem.Instance.IsPlayerTurn);
    }

    private void UpdateEndTurnVisibility()
    {
        endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn);
    }
}
