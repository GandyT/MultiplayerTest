using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputAddressMenu : MonoBehaviour
{
    [SerializeField] private MultiplayerLobby networkManager;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Canvas MainUI;
    [SerializeField] private GameObject LobbyUI;
    [SerializeField] private Text ErrorMessage;

    private bool connected = false;

    private void Awake()
    {
        joinButton.onClick.AddListener(JoinLobby);
        ErrorMessage.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        /* SUBSCRIBE TO THESE EVENTS */
        networkManager.ClientConnected += HandleClientConnect;
        networkManager.ClientDisconnected += HandleClientDisconnect;
    }

    private void OnDisable()
    {
        networkManager.ClientConnected -= HandleClientConnect;
        networkManager.ClientDisconnected -= HandleClientDisconnect;
    }

    public void JoinLobby ()
    {
        networkManager.networkAddress = input.text;
        networkManager.StartClient();

        joinButton.interactable = false;
        Debug.Log($"JOINING LOBBY: {input.text}");
    }

    private void HandleClientConnect ()
    {
        joinButton.interactable = true;
        gameObject.SetActive(false);
        connected = true;
        Debug.Log("CLIENT CONNECTED!");
    }
    private void HandleClientDisconnect ()
    {
        joinButton.interactable = true;
        MainUI.GetComponent<MenuSceneLoader>().LoadUniqueComponent(gameObject);

        if (!connected)
        {
            ErrorMessage.gameObject.SetActive(true);
            Invoke("DisableError", 2f);
        } else
        {
            connected = false;
        }

        Debug.Log("CLIENT DISCONNECTED!");
    }

    private void DisableError ()
    {
        ErrorMessage.gameObject.SetActive(false);
    }
}
