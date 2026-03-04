using UnityEngine;
using MagmaLabs.UI;

public class WaypointManager : MonoBehaviour
{
    [HideInInspector]public static  WaypointManager instance;
    [SerializeField] private Pointer pointer;
    [SerializeField] private TextInfographic distanceText;
    public Transform target;
    public Transform position;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("Multiple instances of WaypointManager detected. Destroying duplicate.");
            Destroy(this);
        }
    }
    
    void Update()
    {
        if (target != null && position != null)
        {
            float distance = GetDistanceToTarget();
            float angle = GetAngleToTargetDegrees();

            pointer.SetValue(angle);
            distanceText.SetValue((int)distance);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetTrackedPosition(Transform position)
    {
        this.position = position;
    }

    public float GetDistanceToTarget()
    {
        return Vector3.Distance(position.position, target.position);
    }

    public float GetAngleToTargetDegrees()
    {
        Vector3 directionToTarget = target.position - position.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        return (angle + 360) % 360; // Normalize to [0, 360)
    }
}
