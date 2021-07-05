using Mirror;
using UnityEngine.UI;
using UnityEngine;

public class NetworkRoomPlayerLobby : NetworkBehaviour
{
    public bool isLeader = false;
    [SerializeField] private GameObject LobbyUIPrefab;
    private GameObject LobbyUI;
    private Text[] PlayerTexts = new Text[2];

    private void Awake()
    {
        LobbyUI = RefManager.INSTANCE.LobbyUI;
        PlayerTexts = RefManager.INSTANCE.LobbyTexts;
    }

    private MultiplayerLobby networkManagerRef = null;
    private MultiplayerLobby networkManager
    {
        get
        {
            if (networkManagerRef == null) networkManagerRef = NetworkManager.singleton as MultiplayerLobby;
            return networkManagerRef;
        }
    }

    [SyncVar(hook = nameof(HandleDisplayNameChange))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStateChange))]
    public bool isReady = false;

    public override void OnStartAuthority()
    {
        CmdUpdateDisplayName(GameManager.INSTANCE.PlayerSettings["DISPLAY_NAME"]);
        LobbyUI.SetActive(true);
    }

    public override void OnStartClient ()
    {
        networkManager.RoomPlayers.Add(this);
        UpdateDisplay();
    }

    public override void OnStopClient ()
    {
        networkManager.RoomPlayers.Remove(this);
        UpdateDisplay();
    }

    public void HandleReadyStateChange(bool oldValue, bool newValue) => UpdateDisplay();
    public void HandleDisplayNameChange(string oldValue, string newValue) => UpdateDisplay();
 
    private void UpdateDisplay ()
    {
        if (!hasAuthority)
        {
            foreach (NetworkRoomPlayerLobby player in networkManager.RoomPlayers)
            {
                if (player.hasAuthority) player.UpdateDisplay();
            }
        }

        for (int i = 0; i < PlayerTexts.Length; ++i)
        {
            if (i >= networkManager.RoomPlayers.Count)
            {
                PlayerTexts[i].text = "Waiting...";
            }
            else
            {
                PlayerTexts[i].text = networkManager.RoomPlayers[i].DisplayName + " - " + (networkManager.RoomPlayers[i].isReady ? "READY" : "NOT READY");
            }
        }
        
    }

    [Command]
    public void CmdUpdateDisplayName (string name)
    {
        DisplayName = name;
    }

    [Command]
    public void CmdUpdateReadyState (bool readyState)
    {
        isReady = readyState;
    }


}
