using System.Collections;
using System.Collections.Generic;
using MagmaLabs.UI;
using UnityEngine;
using UnityEngine.Events;


public class LevelManager : MonoBehaviour
{

    public string levelObjective;
    public List<Objective> objectives;

    //public List<LevelEvent> levelTimeline;
    int currentObjective = 0;

    public UnityEvent OnLevelBegin;
    public UnityEvent OnLevelClear;
    public UnityEvent OnLevelFail;

    void Start()
    {

        StartCoroutine(BeginLevelCoroutine());
    }

    IEnumerator BeginLevelCoroutine()
    {
        yield return null;
        AlertManager.instance.BroadcastAlert(levelObjective, 2);
        yield return new WaitForSeconds(2f);
        BeginLevel();
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

    public void OnObjectiveProgress(float completed, float total)
    {
        InGameHUDManager.instance.DisplayProgress(completed, total);
    }

    public void OnObjectiveCompleted()
    {
        Objective obj = objectives[currentObjective];
        AlertManager.instance.BroadcastAlert(obj.completionMessage, 2);


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
    public int funds;


}

public enum LevelState
{
    Paused,
    Loading, 
    InCutscene, 
    InDialogue,
    Playing

}


