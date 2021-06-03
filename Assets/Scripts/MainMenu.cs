using UnityEngine;
using System;
using Bolt;
using Bolt.Matchmaking;
using UdpKit;

public class MainMenu : GlobalEventListener
{
    private string hostSessionID = "default", joinSessionID = "default";
 
    // ###### Server hosting ######
    public void StartServer(string pSessionID) {
        hostSessionID = pSessionID;
        BoltLauncher.StartServer();
    }

    public override void BoltStartDone() {
        BoltMatchmaking.CreateSession(hostSessionID,sceneToLoad:"Game");
    }

    // ###### Server Joining ######
    public void StartClient(string pSessionID) {
        joinSessionID = pSessionID;
        BoltLauncher.StartClient();
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach(var session in sessionList){
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.Source == UdpSessionSource.Photon) {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
    }
}
