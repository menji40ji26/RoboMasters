using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandStair : MonoBehaviour {

	public GameObject receiver;
	public GameObject next;
	string sensorType;

	// Use this for initialization
	void Start () {
		sensorType = "stair";
	}
	
	// Update is called once per frame
	void Update () {
		
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
		Player _p = receiver.GetComponent<Player>();
		print(receiver.name + " Enter Stair Area");
		if(_p.isLocalPlayer && next ){
			if(_p.type == "engineer"){
				GC.gc.hint.ClimbHint();
			}
		}
		receiver.GetComponent<PlayerInteraction>().onSensor = sensorType;
		receiver.GetComponent<PlayerInteraction>().nextStair = next;
	}

	void LeaveSensor(){
		if(receiver.GetComponent<PlayerInteraction>().onSensor == sensorType){
			receiver.GetComponent<PlayerInteraction>().onSensor = null;
		}
	}
}
