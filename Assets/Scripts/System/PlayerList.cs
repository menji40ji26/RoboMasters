using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerList : NetworkBehaviour {

	public Player hero_Red;
	public Player standard1_Red;
	public Player standard2_Red;
	public Player standard3_Red;
	public Player engineer_Red;
	public Player pilot_Red;
	public Player gunner_Red;

	public Player hero_Blue;
	public Player standard1_Blue;
	public Player standard2_Blue;
	public Player standard3_Blue;
	public Player engineer_Blue;
	public Player pilot_Blue;
	public Player gunner_Blue;

	public Sentry sentry_Blue;
	public Sentry sentry_Red;

	public GameObject red_standard1_damageSensor;
	public GameObject red_standard2_damageSensor;
	public GameObject red_standard3_damageSensor;
	public GameObject blue_standard1_damageSensor;
	public GameObject blue_standard2_damageSensor;
	public GameObject blue_standard3_damageSensor;
	public GameObject red_hero_damageSensor;
	public GameObject blue_hero_damageSensor;
	public GameObject red_sentry_damageSensor;
	public GameObject blue_sentry_damageSensor;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!isServer){
			return;
		}
		FindSentries();
		AttachSensors();
	}

	void FindSentries(){
		if(!sentry_Blue){
			sentry_Blue = GameObject.Find("SentryBlue").GetComponent<Sentry>();
		}
		if(!sentry_Red){
			sentry_Red = GameObject.Find("SentryRed").GetComponent<Sentry>();
		}
		
	}

	void AttachSensors(){
		if(standard1_Red){
			red_standard1_damageSensor.transform.position = standard1_Red.transform.GetChild(0).position;
			red_standard1_damageSensor.transform.rotation = standard1_Red.transform.rotation;
		}
		if(standard2_Red){
			red_standard2_damageSensor.transform.position = standard2_Red.transform.GetChild(0).position;
			red_standard2_damageSensor.transform.rotation = standard2_Red.transform.rotation;
		}
		if(standard3_Red){
			red_standard3_damageSensor.transform.position = standard3_Red.transform.GetChild(0).position;
			red_standard3_damageSensor.transform.rotation = standard3_Red.transform.rotation;
		}
		if(standard1_Blue){
			blue_standard1_damageSensor.transform.position = standard1_Blue.transform.GetChild(0).position;
			blue_standard1_damageSensor.transform.rotation = standard1_Blue.transform.rotation;
		}
		if(standard2_Blue){
			blue_standard2_damageSensor.transform.position = standard2_Blue.transform.GetChild(0).position;
			blue_standard2_damageSensor.transform.rotation = standard2_Blue.transform.rotation;
		}
		if(standard3_Blue){
			blue_standard3_damageSensor.transform.position = standard3_Blue.transform.GetChild(0).position;
			blue_standard3_damageSensor.transform.rotation = standard3_Blue.transform.rotation;
		}
		if(hero_Blue){
			blue_hero_damageSensor.transform.position = hero_Blue.transform.GetChild(0).position;
			blue_hero_damageSensor.transform.rotation = hero_Blue.transform.rotation;
		}
		if(hero_Red){
			red_hero_damageSensor.transform.position = hero_Red.transform.GetChild(0).position;
			red_hero_damageSensor.transform.rotation = hero_Red.transform.rotation;
		}
		//Sentry
		if(sentry_Blue){
			blue_sentry_damageSensor.transform.position = sentry_Blue.transform.GetChild(0).position;
			blue_sentry_damageSensor.transform.rotation = sentry_Blue.transform.rotation;
		}
		if(sentry_Red){
			red_sentry_damageSensor.transform.position = sentry_Red.transform.GetChild(0).position;
			red_sentry_damageSensor.transform.rotation = sentry_Red.transform.rotation;
		}
	}

	[Command]
	public void CmdAddPlayer(GameObject _player, string _role, string _faction){

		print(_player + "  " + _role + "  " +  _faction);
		Player _p = _player.GetComponent<Player>();
		if(_faction == "red"){
			if(_role == "standard1"){
				standard1_Red = _p;
			} else if(_role == "standard2"){
				standard2_Red = _p;
			} else if(_role == "standard3"){
				standard3_Red = _p;
			} else if(_role == "hero"){
				hero_Red = _p;
			} else if(_role == "engineer"){
				engineer_Red = _p;
			} else if(_role == "pilot"){
				pilot_Red = _p;
			} else if(_role == "gunner"){
				gunner_Red = _p;
			}
		} else if(_faction == "blue"){
			if(_role == "standard1"){
				standard1_Blue = _p;
			} else if(_role == "standard2"){
				standard2_Blue = _p;
			} else if(_role == "standard3"){
				standard3_Blue = _p;
			} else if(_role == "hero"){
				hero_Blue = _p;
			} else if(_role == "engineer"){
				engineer_Blue = _p;
			} else if(_role == "pilot"){
				pilot_Blue = _p;
			} else if(_role == "gunner"){
				gunner_Blue = _p;
			}
		}
	}
}
