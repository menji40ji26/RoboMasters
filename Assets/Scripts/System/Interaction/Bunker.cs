using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bunker : NetworkBehaviour {


	string sensorType;
	public GameObject receiver;

	void Start () {
		sensorType = "bunker";
	}

	void OnTriggerEnter(Collider _other){
		if(_other.CompareTag("Player")){
			receiver = _other.gameObject;
			EnterSensor();
		}
	}
	void OnTriggerExit(Collider _other){
		if(_other.gameObject == receiver){
			LeaveSensor();
		}
	}

	void EnterSensor(){
		print(receiver.name + " Enter Bunker");
		receiver.GetComponent<PlayerInteraction>().onSensor = sensorType;
		CmdBonus();
	}

	[Command]
	void CmdBonus(){
		receiver.GetComponent<Player>().defence += 0.5f;
		receiver.GetComponent<Player>().asBonus = 5f;
		receiver.GetComponent<Player>().coolingBonus += 4f;
	}

	[Command]
	void CmdCancelBonus(){
		receiver.GetComponent<Player>().defence -= 0.5f;
		receiver.GetComponent<Player>().asBonus = 1f;
		receiver.GetComponent<Player>().coolingBonus -= 4f;
	}

	void LeaveSensor(){
		if(receiver.GetComponent<PlayerInteraction>().onSensor == sensorType){
			CmdCancelBonus();
			receiver.GetComponent<PlayerInteraction>().onSensor = null;
		}
	}
}
