using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealingPoint : NetworkBehaviour {

	[SyncVar]
	public GameObject receiver;
	public string faction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(receiver) heal();
	}

	void OnTriggerEnter(Collider _other){
		if(_other.tag == "Player" && !receiver){
			if(_other.GetComponent<Player>().faction == faction){
				receiver = _other.gameObject;
				print(receiver.name + " entered the healing station");
			}
		}
	}
	
	void OnTriggerExit(Collider _other){
		if(_other.tag == "Player" && _other.gameObject == receiver){
			receiver = null;
		}
	}

	void heal(){
		Player _p = receiver.GetComponent<Player>();
		if(_p.hp < _p.maxHp){
			_p.hp += _p.maxHp * 0.05f * Time.deltaTime;
		} else {
			_p.hp = _p.maxHp;
		}
	}

}
