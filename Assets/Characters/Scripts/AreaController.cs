using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class AreaController : MonoBehaviour
{
    public string areaName;
    public List<Collider2D> collidersInArea;
    public UnityEvent<Collider2D, string> OnEnterArea; 
    public UnityEvent<Collider2D, string> OnExitArea; 
    public UnityEvent<Collider2D, string> OnRemainInArea; 
    
    void FixedUpdate()
    {
        foreach(Collider2D collider in collidersInArea){
            OnRemainInArea.Invoke(collider, areaName);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        OnEnterArea.Invoke(other, areaName);
        collidersInArea.Add(other);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        OnExitArea.Invoke(other, areaName);
        collidersInArea.Remove(other);
    }

}
