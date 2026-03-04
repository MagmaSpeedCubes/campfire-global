using UnityEngine;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private CanvasGroup endScreen;

    public void ShowWinScreen()
    {
        endScreen.alpha = 1f;
    }

    public void ShowLossScreen()
    {
        
    }
}
