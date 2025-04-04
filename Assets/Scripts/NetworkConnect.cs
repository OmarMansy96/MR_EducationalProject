using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class NetworkConnect : MonoBehaviour
{
    Lobby currentLobby;
    public string joinCode;
    public int maxConnection = 20;
    public UnityTransport transport;
    float heartBeatTimer;
    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        CreateOrJoin();
    }
    async void CreateOrJoin()
    {
        try
        {
            currentLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
            string relayJoinCode = currentLobby.Data["JOIN_CODE"].Value;

            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);
            transport.SetClientRelayData(joinAllocation.RelayServer.IpV4, (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes, joinAllocation.Key, joinAllocation.ConnectionData, joinAllocation.HostConnectionData);

            NetworkManager.Singleton.StartClient();
        }
        catch
        {
            Create();
        }
    }
    public async void Create()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
       
        Debug.LogError(newJoinCode);
        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
        lobbyOptions.IsPrivate = false;
        lobbyOptions.Data = new Dictionary<string, DataObject>();
        DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public, newJoinCode);
        lobbyOptions.Data.Add("JOIN_CODE", dataObject);
        currentLobby = await LobbyService.Instance.CreateLobbyAsync("Lobby Name", maxConnection, lobbyOptions);
        NetworkManager.Singleton.StartHost();
    }
    public async void Joint()
    {

    }
    private void Update()
    {
        if (heartBeatTimer > 15)
        {
            heartBeatTimer -= 15;
            
            if(currentLobby!= null && currentLobby.HostId==AuthenticationService.Instance.PlayerId)
            {
                LobbyService.Instance.SendHeartbeatPingAsync(currentLobby. Id);
            }
        
        }
        heartBeatTimer += Time.deltaTime;
    }
}
