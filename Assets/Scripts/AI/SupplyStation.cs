using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SupplyStation : NetworkBehaviour {

	[SyncVar]
	public GameObject receiver;
	[SyncVar]
	public int ammo17;
	[SyncVar]
	public float nextAutoFillTime;
	public string faction;
	public float reloadFreq;
	float reloadCD;



	// Use this for initialization
	void Start () {
		ammo17 = 2000;
		nextAutoFillTime = 60;
	}

	void FixedUpdate(){
		CountTime();
		if(receiver){
			Reload();
		}
	}

	void OnTriggerEnter(Collider _other){
		if(_other.tag == "Player" && !receiver){
			if(_other.GetComponent<Player>().faction == faction){
				receiver = _other.gameObject;
				print(receiver.name + " entered the supply station");
			}
		}
	}
	
	void OnTriggerExit(Collider _other){
		if(_other.tag == "Player" && _other.gameObject == receiver){
			receiver = null;
		}
	}

	void Reload(){
		if(receiver.GetComponent<Player>().ammo < 100){
			reloadCD -= Time.deltaTime;
			if(reloadCD <= 0 && ammo17 > 0 ){
				reloadCD = reloadFreq;
				receiver.GetComponent<Player>().ammo ++;
				ammo17 --;
			}
		}
	}

	void CountTime(){
		if(GC.gc.state == "playing"){
			nextAutoFillTime -= Time.deltaTime;
			if(nextAutoFillTime <= 0){
				ammo17 += 100;
				nextAutoFillTime = 60;
			}
		}
	}
}
