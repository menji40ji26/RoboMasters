using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pass : NetworkBehaviour {

	public string sensorType;
	public GameObject receiver;

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
		print(receiver.name + " Enter Stair Area");
		receiver.GetComponent<PlayerInteraction>().onSensor = sensorType;
		CmdBonus();
	}

	[Command]
	void CmdBonus(){
		receiver.GetComponent<Player>().coolingBonus += 4f;
		receiver.GetComponent<Player>().asBonus = 5f;
	}

	[Command]
	void CmdCancelBonus(){
		receiver.GetComponent<Player>().coolingBonus -= 4f;
		receiver.GetComponent<Player>().asBonus = 1f;
	}

	void LeaveSensor(){
		if(receiver.GetComponent<PlayerInteraction>().onSensor == sensorType){
			CmdCancelBonus();
			receiver.GetComponent<PlayerInteraction>().onSensor = null;
		}
	}
}
