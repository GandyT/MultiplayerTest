using UnityEngine;
using UnityEngine.UI;

public class HostButton : MonoBehaviour
{
    [SerializeField] private MultiplayerLobby networkManager;
    [SerializeField] private GameObject MainMenuUI;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HostLobby);
    }

    public void HostLobby ()
    {
        Debug.Log($"Starting Host: {GameManager.INSTANCE.PlayerSettings["DISPLAY_NAME"]}");
        networkManager.StartHost();
        MainMenuUI.SetActive(false);
    }
}
