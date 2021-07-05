using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public static GameManager INSTANCE;
    public Dictionary<String, String> PlayerSettings
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        } else
        {
            Destroy(gameObject);
        }

        PlayerSettings = new Dictionary<string, string>();
        DontDestroyOnLoad(gameObject);
    }

    /* PREFS */
    public string GetRandomName ()
    {
        return $"Player{UnityEngine.Random.Range(1, 1000)}";
    }
    public void SetDisplayName (TMP_InputField input)
    {
        if (GameManager.INSTANCE.PlayerSettings.ContainsKey("DISPLAY_NAME"))
        {
            GameManager.INSTANCE.PlayerSettings["DISPLAY_NAME"] = input.text;
        }
        else
        {
            GameManager.INSTANCE.PlayerSettings.Add("DISPLAY_NAME", input.text);
        }

        Debug.Log($"Player Name: {GameManager.INSTANCE.PlayerSettings["DISPLAY_NAME"]}");
    }
}
