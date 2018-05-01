using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	string remoteLayerName = "RemotePlayer";
	public Player _player;
 	public string _netID;

	void Start() {
		_netID = GetComponent<NetworkIdentity>().netId.ToString();
		//string _netID = GameObject.Find("LobbyManager").GetComponent<SaveLobbyNames>().playerName;
		if(isLocalPlayer){
			//_player.playerName = GameObject.Find("LobbyManager").GetComponent<SaveLobbyNames>().playerName;
			GC.gc.SetupLocalPlayer(_netID, _player);
		}
		GC.gc.RegisterPlayer(_netID, _player);
	}

	[Command]
	public void CmdSetup(string _type, string _role, string _faction){
		//_player.type = _type;
		//GetComponent<Player>().state = "prepare";
		_player.RpcBegin(_type, _role, _faction);
	}

	public void Setup(string _type){
		GetComponent<Player>().state = "prepare";
	}

	public override void OnStartLocalPlayer() {
		
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);


	}

	public override void PreStartClient(){

		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
	}

	void AssignRemoteLayer(){
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	void OnDisable (){
		GC.gc.UnRegisterPlayer("Player " + _netID);
		if(!isLocalPlayer) return;
		if(GC.gc.mainCam != null){
			GC.gc.mainCam.gameObject.SetActive(true);
			GC.gc.paused = false;
		}

	}




}
