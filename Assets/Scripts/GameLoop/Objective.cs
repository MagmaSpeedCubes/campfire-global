using UnityEngine;
using UnityEngine.Events;
public class Objective : MonoBehaviour
{
    [SerializeField]private float holdTimeToComplete = 2f;
    private float timePlayerHasBeenInZone = 0f;
    private bool playerInZone = false;

    public UnityEvent OnObjectiveCompleted;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            playerInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            playerInZone = false;
            timePlayerHasBeenInZone = 0f;
        }
    }

    void Update()
    {
        if(playerInZone)
        {
            timePlayerHasBeenInZone += Time.deltaTime;
            if(timePlayerHasBeenInZone >= holdTimeToComplete)
            {
                OnObjectiveCompleted.Invoke();
                playerInZone = false; // Prevent multiple completions
            }
        }
    }
}
