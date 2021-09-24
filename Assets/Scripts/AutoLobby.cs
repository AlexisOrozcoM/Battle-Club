using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoLobby : MonoBehaviourPunCallbacks
{


    public Button ConnectButton;
    public Button JoinRandomButton;
    public Text Log;
    public Text PlayerCount;
    public int playersCount;

    public byte maxPlayersAllow = 4;

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.ConnectUsingSettings())
            {
                Log.text += "\n Connected to Server";
            }
            else
            {
                Log.text += "\n Failing Connecting to Server";
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        ConnectButton.interactable = false;
        JoinRandomButton.interactable = true;
        
    }

    public void JoinRandom()
    {
        if (!PhotonNetwork.JoinRandomRoom())
        {
            Log.text += "\n Joinned room";
        }          
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Log.text += "\n No Rooms available, creating one...";

        if(PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() {MaxPlayers = maxPlayersAllow }))
        {
            Log.text += "\n Room Created";
        }

        else
        {
            Log.text += "\n Fail Creating Room";
        }
    }


    public override void OnJoinedRoom()
    {
        JoinRandomButton.interactable = false;
        Log.text += "\n Joinned!!";
    }

    private void FixedUpdate()
    {
        if(PhotonNetwork.CurrentRoom != null)
        {
            playersCount = PhotonNetwork.CurrentRoom.PlayerCount;

            PlayerCount.text  = playersCount + "/" + maxPlayersAllow + " Players";

        }
        
    }

}
