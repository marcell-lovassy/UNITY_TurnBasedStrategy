using UnityEngine;
using UnityEngine.Events;

public class TurnSystem : MonoBehaviour
{
    public event UnityAction OnTurnChanged;

    public static TurnSystem Instance;

    public int TurnNumber => turnNumber;
    public bool IsPlayerTurn => isPlayerTurn;

    private int turnNumber = 1;
    private bool isPlayerTurn = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one TurnSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;
        OnTurnChanged?.Invoke();
    }
}
