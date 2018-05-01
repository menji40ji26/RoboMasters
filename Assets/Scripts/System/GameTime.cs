using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {

	public float time;
	public float currentTime;
	public Text timeText;
	// Use this for initialization
	void Start () {
		currentTime = time;
	}

	// Update is called once per frame
	void FixedUpdate () {
		CountTime();
	}
	
	
	void CountTime(){
		if(GC.gc.state == "playing"){
			int _minute = Mathf.CeilToInt((currentTime-60)/60);
			int _second = Mathf.CeilToInt(currentTime - (_minute*60));
			currentTime -= Time.fixedDeltaTime;
			if(currentTime>=420){
				timeText.text = " 07 : 00";
			} else if(_second == 60){
				timeText.text = "0" + (_minute + 1) + " : 00";
			} else if(_second <= 10){
				timeText.text = "0" + _minute + " : 0" + _second;
			} else if(time <= 0){
				timeText.text = "00 : 00";
				GetComponent<GameState>().TimeEnd();
			} else {
				timeText.text = "0" + _minute + " : " + _second;
			}
		} else if(GC.gc.state == "preparing"){
			currentTime = time;
		}

	}
}
