using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;


public class LevelManager : MonoBehaviour
{

    public List<Objective> objectives;
    int currentObjective = 0;

    public UnityEvent OnLevelBegin;
    public UnityEvent OnLevelClear;
    public UnityEvent OnLevelFail;

    void Start()
    {
        BeginLevel();//for now
    }

    public void BeginLevel()
    {

        OnLevelBegin.Invoke();
        BeginObjective();//the level assumes there is at least one objecive
    }

    public void BeginObjective()
    {
        Objective obj = objectives[currentObjective];
        obj.taskPoint.gameObject.SetActive(true);

        InGameHUDManager.instance.objectiveText.text = obj.description;
        WaypointManager.instance.SetTarget(obj.taskPoint.transform);

    }

    public void OnObjectiveCompleted()
    {
        currentObjective++;
        if(currentObjective >= objectives.Count)
        {
            OnLevelClear.Invoke();
        }
        else
        {
            BeginObjective();
        }
    }
}


public struct LevelRuntime
{
    public LevelState levelState;
}

public enum LevelState
{
    Paused,
    Loading, 
    InCutscene, 
    InDialogue,
    Playing

}
