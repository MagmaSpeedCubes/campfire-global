using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MagmaLabs.UI;
public class InGameHUDManager : MonoBehaviour
{
    [HideInInspector]public static InGameHUDManager instance;

    public TextMeshProUGUI objectiveText;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("Multiple instances of InGameHUDManager detected. Destroying duplicate.");
            Destroy(this);
        }
    }

    





}

