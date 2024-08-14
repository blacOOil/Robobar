using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class JoinandCreateRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField EnterCode;
    // Start is called before the first frame update
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(EnterCode.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(EnterCode.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("FinalProjectOnlineDemo");
    }
}
