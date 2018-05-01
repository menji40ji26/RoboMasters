using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

	GC gc;
	public Text gameStateText;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GC>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GC.gc.redPlayers.Count + GC.gc.bluePlayers.Count == GC.players.Count && GC.gc.state != "end"){
			GC.gc.state = "playing";
		} else if (GC.gc.state != "end") {
			GC.gc.state = "preparing";
		}
	}

	public void CheckSurvivor(){
		int _red = gc.redPlayers.Count;
		int _blue = gc.bluePlayers.Count;
		print("CheckSurvivor" + " Blue " + GC.gc.bluePlayers.Count + " Red " + GC.gc.redPlayers.Count);
		for (int i = 0; i < gc.redPlayers.Count; i++){
			if(gc.redPlayers[i].state == "dead"){
				_red --;
			}
		}

		for (int i = 0; i < gc.bluePlayers.Count; i++){
			if(gc.bluePlayers[i].state == "dead"){
				_blue --;
			}
		}

		if(_red == 0) blueWin();
		else if(_blue == 0) redWin();
	}

	public void blueWin(){
		if(gc.localPlayer.faction == "blue"){
			Win();
		} else if(gc.localPlayer.faction == "red"){
			Lose();
		}

		GC.gc.state = "end";
		GC.gc.gameStateUI.SetActive(true);
	}

	public void redWin(){
		if(gc.localPlayer.faction == "red"){
			Win();
		} else if(gc.localPlayer.faction == "blue"){
			Lose();
		}

		GC.gc.state = "end";

			GC.gc.gameStateUI.SetActive(true);
	}

	public void TimeEnd(){
		if(GC.gc.baseBlue.hp > GC.gc.baseRed.hp){
			blueWin();
		} else if(GC.gc.baseBlue.hp < GC.gc.baseRed.hp){
			redWin();
		} else {
			gameStateText.text = "DRAW";
		}
		GC.gc.state = "end";
	}

	void Win(){
		gameStateText.text = "VICTORY";
		gameStateText.color = Color.yellow;
	}
	void Lose(){
		gameStateText.text = "DEFEATED";
		gameStateText.color = Color.black;
	}
}
