using Mirror;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public class MultiplayerLobby : NetworkManager
{
    [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab;

    public event Action ClientConnected;
    public event Action ClientDisconnected;

    public List<NetworkRoomPlayerLobby> RoomPlayers = new List<NetworkRoomPlayerLobby>();

    public override void OnStartServer() {
        spawnPrefabs = Resources.LoadAll<GameObject>("NetworkPrefabs").ToList();
    }

    public override void OnStartClient ()
    {
        GameObject[] networkPrefabs = Resources.LoadAll<GameObject>("NetworkPrefabs");
        foreach (GameObject networkPrefab in networkPrefabs)
        {
            NetworkClient.RegisterPrefab(networkPrefab);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        // I'm 90% sure that OnClientConnect only invokes when the LocalPlayer connects.
        base.OnClientConnect(conn);
        ClientConnected?.Invoke();
    }

    public override void OnClientDisconnect (NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        ClientDisconnected?.Invoke();
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        bool isLeader = RoomPlayers.Count == 0;

        NetworkRoomPlayerLobby remotePlayer = Instantiate(roomPlayerPrefab);

        remotePlayer.isLeader = isLeader;

        NetworkServer.AddPlayerForConnection(conn, remotePlayer.gameObject);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (RoomPlayers.Count >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        RoomPlayers.Remove(conn.identity.GetComponent<NetworkRoomPlayerLobby>());
        base.OnServerDisconnect(conn);
    }

}
