using System.Collections;
using System.Collections.Generic;
using MagmaLabs.UI;
using UnityEngine;
using UnityEngine.Events;


public class LevelController : MonoBehaviour
{

    public string levelObjective;
    public List<Objective> objectives;

    //public List<LevelEvent> levelTimeline;
    int currentObjective = 0;

    public UnityEvent OnLevelBegin;
    public UnityEvent OnLevelClear;
    public UnityEvent OnLevelFail;

    public LevelRuntime levelRuntime;

    public static LevelController instance;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            levelRuntime = new LevelRuntime();
            Debug.LogWarning("Multiple instances of LevelController detected. Destroying duplicate.");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    
    }

    void Start()
    {
        
        //StartCoroutine(BeginLevelCoroutine());
    }

    IEnumerator BeginLevelCoroutine()
    {
        yield return null;
        AlertManager.instance.BroadcastAlert(levelObjective, 2);
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlayBGM("mainTheme");
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
        if (obj.taskPoint != null)
        {
            obj.taskPoint.gameObject.SetActive(true);
            WaypointManager.instance.SetTarget(obj.taskPoint.transform);
        }

        if (!string.IsNullOrWhiteSpace(obj.description))
        {
            InGameHUDManager.instance.objectiveText.text = obj.description;
        }

        obj.OnObjectiveStarted?.Invoke();
        TryBroadcastAlert(obj.startAlert);
        TryBeginCutscene(obj.startCutscene);

    }

    public void OnObjectiveProgress(float completed, float total)
    {
        InGameHUDManager.instance.DisplayProgress(completed, total);
    }

    public void OnObjectiveCompleted()
    {
        Objective obj = objectives[currentObjective];
        if (!TryBroadcastAlert(obj.completionAlert) && !string.IsNullOrWhiteSpace(obj.completionMessage))
        {
            AlertManager.instance.BroadcastAlert(obj.completionMessage, 2f);
        }
        TryBeginCutscene(obj.completionCutscene);
        obj.OnObjectiveCompleted?.Invoke();


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

    public void EndCutscene()
    {
        levelRuntime.levelState = LevelState.Playing;
    }

    private bool TryBroadcastAlert(Objective.ObjectiveAlert alert)
    {
        if (!alert.enabled)
        {
            return false;
        }

        string message = alert.message;
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        float duration = alert.duration <= 0f ? 2f : alert.duration;
        AlertManager.instance.BroadcastAlert(message, duration);
        return true;
    }

    private void TryBeginCutscene(Objective.ObjectiveCutscene cutscene)
    {
        if (!cutscene.enabled)
        {
            return;
        }

        levelRuntime.levelState = LevelState.InCutscene;
        cutscene.OnBegin?.Invoke();
    }




}

[System.Serializable]
public struct LevelRuntime
{
    public LevelState levelState;
    public int funds;
    public Character playerCharacter;


}
[System.Serializable]
public enum LevelState
{
    Paused,
    Loading, 
    InCutscene, 
    InDialogue,
    Playing

}
