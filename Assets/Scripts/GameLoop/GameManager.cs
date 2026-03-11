using System;

using UnityEngine;

using MagmaLabs;
using MagmaLabs.Economy;


public class GameManager : MonoBehaviour
{
  
    public static GameManager instance;
    public SaveManager saveManager;

    [SerializeField]private SerializableDictionary<Canvas> canvases;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            Debug.LogWarning("Multiple instances of GameManager detected. Destroying duplicate.");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    
    }
    void Start()
    {
        saveManager = SaveManager.instance;
        bool newPlayer = false;
        if (saveManager.LoadString("joinDate").Equals(""))
        {
            SerializableDateTime sdt = new SerializableDateTime(DateTime.Now);
            saveManager.SaveString("joinDate", sdt.ToString());
            newPlayer = true;
        }

        if (newPlayer)
        {
            canvases.Get("titleScreen").enabled = true;
        }
        else
        {
            canvases.Get("headquarters").enabled = true;
        }



        //SerializableDateTime joinDate = new SerializableDateTime()


        AudioManager.instance.PlayBGM("casualTheme");
    }

}
