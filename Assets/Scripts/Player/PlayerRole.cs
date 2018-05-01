using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRole : NetworkBehaviour {

	Player p;
	ComponentChange cc;
	float liftSpeed = 1;

	void Start(){
		p = GetComponent<Player>();
		cc = GetComponent<ComponentChange>();
	}

	// [Command]
	// public void CmdSetType(string _type){
	// 	p.type = _type;
	// }

	void FixedUpdate(){
		Action();
	}

	void Action(){
		if(isLocalPlayer){
			HeroLift();
		}
	}


	void HeroLift(){
		if(p.type == "hero" || p.type == "enginner"){
			print("is Hero");
			if(Input.GetKey(KeyCode.Z)){

			print(cc.upper.transform.position.y  + " " + cc.upperCam_HighPoint.transform.position.y);
			Vector3 _upperPos = new Vector3(0, liftSpeed ,0);
				cc.upper.transform.position += _upperPos * 0.01f; 
				if(cc.upper.transform.position.y > cc.upperCam_HighPoint.transform.position.y){
					cc.upper.transform.position = cc.upperCam_HighPoint.transform.position;
				}
			}
			if(Input.GetKey(KeyCode.X)){

			print("Press X");
			Vector3 _upperPos = new Vector3(0, liftSpeed ,0);
				cc.upper.transform.position -= _upperPos * 0.01f; 
				if(cc.upper.transform.position.y < cc.upperCam_LowPoint.transform.position.y){
					cc.upper.transform.position = cc.upperCam_LowPoint.transform.position;
				}
			}
		}
	}

	public void SetType(){
		if(p.type == "standard"){
			p.maxHp = GC.gc.maxHp_Standard;
			p.maxSpeed = GC.gc.maxSpeed_standard;
			p.acceleration = GC.gc.acceleration_standard;
			p.maxSpeed = GC.gc.maxSpeed_standard;
			p.attackSpeed = GC.gc.attackSpeed_standard;
			p.attackPower = GC.gc.attackPower_standard;
		} else if(p.type == "hero"){
			p.maxHp = GC.gc.maxHp_hero;
			p.maxSpeed = GC.gc.maxSpeed_hero;
			p.acceleration = GC.gc.acceleration_hero;
			p.maxSpeed = GC.gc.maxSpeed_hero;
			p.attackSpeed = GC.gc.attackSpeed_hero;
			p.attackPower = GC.gc.attackPower_hero;
			cc.upperCam.enabled = true;
			GC.gc.hint.HeroHint();
		} else if(p.type == "engineer"){
			p.maxHp = GC.gc.maxHp_engineer;
			p.maxSpeed = GC.gc.maxSpeed_engineer;
			p.acceleration = GC.gc.acceleration_engineer;
			p.maxSpeed = GC.gc.maxSpeed_engineer;
			cc.upperCam.enabled = true;
		} else if(p.type == "pilot"){
			p.maxHp = GC.gc.maxHp_drone;
			p.maxSpeed = GC.gc.maxSpeed_drone;
			p.acceleration = GC.gc.acceleration_drone;
			p.maxSpeed = GC.gc.maxSpeed_drone;
			GC.gc.hint.PilotHint();
		} else if(p.type == "gunner"){
			p.maxHp = GC.gc.maxHp_drone;
			p.attackSpeed = GC.gc.attackSpeed_drone;
			p.attackPower = GC.gc.attackPower_drone;
			p.ammo += 200;
		}
		p.hp = p.maxHp;
	}

}
