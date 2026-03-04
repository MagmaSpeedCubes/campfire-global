using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public struct Objective
{
    public string description;
    public string completionMessage;
    public TaskPointController taskPoint;
    public UnityEvent OnObjectiveCompleted;
}

