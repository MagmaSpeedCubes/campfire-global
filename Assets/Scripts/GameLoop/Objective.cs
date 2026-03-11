using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public struct Objective
{
    public string description;
    public string completionMessage;
    public TaskPointController taskPoint;
    public UnityEvent OnObjectiveStarted;
    public UnityEvent OnObjectiveCompleted;

    [Header("Alerts")]
    public ObjectiveAlert startAlert;
    public ObjectiveAlert completionAlert;

    [Header("Cutscenes")]
    public ObjectiveCutscene startCutscene;
    public ObjectiveCutscene completionCutscene;

    [System.Serializable]
    public struct ObjectiveAlert
    {
        public bool enabled;
        public string message;
        public float duration;
    }

    [System.Serializable]
    public struct ObjectiveCutscene
    {
        public bool enabled;
        public UnityEvent OnBegin;
        public UnityEvent OnEnd;
    }
}
