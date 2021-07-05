using UnityEngine;
using UnityEngine.UI;

public class RefManager : MonoBehaviour
{
    public static RefManager INSTANCE;
    public GameObject LobbyUI;
    public Text[] LobbyTexts = new Text[2];

    private void Awake()
    {
        INSTANCE = this;
    }
}
