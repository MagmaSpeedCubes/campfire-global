using UnityEngine;
using UnityEngine.Events;

using MagmaLabs.Controllers;
public class TaskPointController : MonoBehaviour
{
    [SerializeField]private float holdTime;
    
    
    private bool playerInRange;
    private float inRangeDuration = 0;
    public UnityEvent OnEnterTaskPoint;
    public UnityEvent<float, float> OnHold;

    public UnityEvent OnExitTaskPoint;
    public UnityEvent OnCompleteHold;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<TopDown2DPlayerController>() != null)
        {
            playerInRange = true;
            OnEnterTaskPoint.Invoke();
        }
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (playerInRange)
        {
            inRangeDuration += Time.deltaTime;
        }
        if(inRangeDuration > holdTime)
        {
            OnCompleteHold.Invoke();
            gameObject.SetActive(false);
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.GetComponent<TopDown2DPlayerController>() != null)
        {
            playerInRange = false;
            inRangeDuration = 0f;
            OnExitTaskPoint.Invoke();
        }
    }
}
