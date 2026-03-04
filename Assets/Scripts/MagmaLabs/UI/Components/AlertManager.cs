using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MagmaLabs.Animation;

namespace MagmaLabs.UI
{
    public class AlertManager : MonoBehaviour
    {
        [SerializeField]private GameObject simpleAlertPrefab, infoAlertPrefab;

        public void BroadcastAlert(string title, float duration)
        {
            GameObject alert = Instantiate(simpleAlertPrefab);
            Alert alertBody = alert.GetComponent<Alert>();

            alertBody.title.text = title;
            alertBody.duration = duration;
        }

        public void BroadcastAlert(string title, Sprite icon, float duration)
        {
            GameObject alert = Instantiate(simpleAlertPrefab);
            Alert alertBody = alert.GetComponent<Alert>();

            alertBody.title.text = title;
            alertBody.icon.sprite = icon;
            alertBody.duration = duration;
        
        }

        
    }


}

