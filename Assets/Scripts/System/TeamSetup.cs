using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TeamSetup : NetworkBehaviour {

	// public List<Player> teamRed = new List<Player>();

	// public List<Player> teamBlue = new List<Player>();

	public Image[] imageRed;
	public Image[] imageBlue;
	public Player localPlayer;
	public string currentType;

	void FixedUpdate(){
		CheckAlive();
	}

	string _playerType(string _type){
		string _theType = _type;
		if(_theType == "standard1" || _theType == "standard2" || _theType == "standard3" ){
			_theType = "standard";
		}
		return _theType;
	}

	void AddToList(){

	}

	// void SetType(){
	// 	localPlayer.GetComponent<PlayerRole>().CmdSetType(currentType);
	// 	localPlayer.GetComponent<PlayerRole>().SetType();
	// }

	Vector3 SetSpawnPoint(string _type, string _faction){
		Vector3 _thePoint = Vector3.zero;
		if(_faction == "red"){
			if(_type == "hero"){
				_thePoint = GC.gc.redSpawnPoints[0].position;
			} else if(_type == "standard1"){
				_thePoint = GC.gc.redSpawnPoints[1].position;
			} else if(_type == "standard2"){
				_thePoint = GC.gc.redSpawnPoints[2].position;
			} else if(_type == "standard3"){
				_thePoint = GC.gc.redSpawnPoints[3].position;
			} else if(_type == "engineer"){
				_thePoint = GC.gc.redSpawnPoints[4].position;
			} else if(_type == "pilot" || _type == "gunner"){
				_thePoint = GC.gc.redSpawnPoints[5].position;
			} 
		} else if(_faction == "blue"){
			if(_type == "hero"){
				_thePoint = GC.gc.blueSpawnPoints[0].position;
			} else if(_type == "standard1"){
				_thePoint = GC.gc.blueSpawnPoints[1].position;
			} else if(_type == "standard2"){
				_thePoint = GC.gc.blueSpawnPoints[2].position;
			} else if(_type == "standard3"){
				_thePoint = GC.gc.blueSpawnPoints[3].position;
			} else if(_type == "engineer"){
				_thePoint = GC.gc.blueSpawnPoints[4].position;
			} else if(_type == "pilot" || _type == "gunner"){
				_thePoint = GC.gc.blueSpawnPoints[5].position;
			} 
		}
		return _thePoint;
	}

	public void JoinRed(string _type){
		currentType = _playerType(_type);
		if(GC.gc.redPlayers.Count > 0){
			for (int i = 0; i < GC.gc.redPlayers.Count; i++){
				if(GC.gc.redPlayers[i]!=localPlayer){
					GC.gc.redPlayers.Add(localPlayer);
					//localPlayer.type = _playerType(_type);
				}
			}
		} else {
			GC.gc.redPlayers.Add(localPlayer);
			//localPlayer.type = _playerType(_type);
		}

		SpawnRed(SetSpawnPoint(_type, "red"));
		Setup(currentType, _type, "red");
		//SetType();
	}

	public void JoinBlue(string _type){
		currentType = _playerType(_type);
		if(GC.gc.bluePlayers.Count > 0){
			for (int i = 0; i < GC.gc.bluePlayers.Count; i++){
				if(GC.gc.bluePlayers[i]!=localPlayer){
					GC.gc.bluePlayers.Add(localPlayer);
					//localPlayer.type = 
				}
			}
		} else {
			GC.gc.bluePlayers.Add(localPlayer);
			//localPlayer.type = _playerType(_type);
		}
		SpawnBlue(SetSpawnPoint(_type, "blue"));
		Setup(currentType, _type, "blue");
		//SetType();
	}


	void CheckAlive(){
		for (int i = 0; i < GC.gc.redPlayers.Count; i++)
		{
			if(GC.gc.redPlayers[i]!=null){
				imageRed[i].gameObject.SetActive(true);
			} else {

				imageRed[i].gameObject.SetActive(false);
			}
		}

		for (int i = 0; i < GC.gc.bluePlayers.Count; i++)
		{
			if(GC.gc.bluePlayers[i]!=null){
				imageBlue[i].gameObject.SetActive(true);
			} else {

				imageBlue[i].gameObject.SetActive(false);
			}
		}
	}

	void Setup(string _type, string _role, string _faction){
		GC.gc.setupUI.SetActive(false);
		localPlayer.GetComponent<PlayerSetup>().CmdSetup(_type, _role, _faction);
		GC.gc.paused = false;
		GC.gc.battleUI.SetActive(true);
	}

	void SpawnRed(Vector3 _spawnPoint){
		for (int i = 0; i < GC.gc.redPlayers.Count; i++){
			if(GC.gc.redPlayers[i] == localPlayer){
				localPlayer.transform.position = _spawnPoint;
				localPlayer.CmdSetRed();
			}
		}
	}

	void SpawnBlue(Vector3 _spawnPoint){
		for (int i = 0; i < GC.gc.bluePlayers.Count; i++){
			if(GC.gc.bluePlayers[i] == localPlayer){
				localPlayer.transform.position = _spawnPoint;
				localPlayer.CmdSetBlue();
			}
		}
	}

	public void RemoveDeadPlayer(Player _Player){

		if(_Player.faction == "red"){
			for (int i = 0; i < GC.gc.redPlayers.Count; i++){
				if(GC.gc.redPlayers[i] == _Player){
					GC.gc.redPlayers.Remove(_Player);
				}
			}
		} else if(_Player.faction == "blue"){
			for (int i = 0; i < GC.gc.bluePlayers.Count; i++){
				if(GC.gc.bluePlayers[i] == _Player){
					GC.gc.bluePlayers.Remove(_Player);
				}
			}
		}
		GC.gc.GetComponent<GameState>().CheckSurvivor();
	}


}
