using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Heat : NetworkBehaviour {

	public float heat;
	public float maxHeat;
	public float coolingSpeed;
	public float heat42;
	public float maxHeat42;
	public float coolingSpeed42;
	public bool overHeat;
	public bool overHeat42;
	public string type;
	//SentryShoot s;
	public Player p;

	public void Setup(string _type){
		type = _type;
		if(isLocalPlayer){
			GC.gc.customData2Text.text = "Heat";
			//if(type == "sentry") {
				// s = GetComponent<SentryShoot>();
				// SetSentryHeat();
			//} else {
				p = GetComponent<Player>();
				SetPlayerHeat();
			//} 
		}
	}

	void SetDataUI(){
		if(p.type == "hero"){
			GC.gc.customData2NumberText.text = Mathf.RoundToInt(heat) + " / " + maxHeat + " " + Mathf.RoundToInt(heat42) + " / " + maxHeat42;
			if(heat42 >= maxHeat42 * 1.5){
				GC.gc.customData2NumberText.color = Color.red;
			} else if(heat42 >= maxHeat42){
				GC.gc.customData2NumberText.color = Color.yellow;
			} else {
				GC.gc.customData2NumberText.color = Color.white;
			}
		} else {
			GC.gc.customData2NumberText.text = Mathf.RoundToInt(heat) + " / " + maxHeat;
			if(heat >= maxHeat * 1.5){
				GC.gc.customData2NumberText.color = Color.red;
			} else if(heat >= maxHeat){
				GC.gc.customData2NumberText.color = Color.yellow;
			} else {
				GC.gc.customData2NumberText.color = Color.white;
			}
		}
	
		
	}
	
	float cd;

	void FixedUpdate () {
		cd -= Time.deltaTime;
		if(cd <= 0){
			cd = 0.1f;
			if(isLocalPlayer){
				Cooling();
				if(p.type == "hero"){
					Cooling42();
				}
				SetDataUI();
				OverheatImageBlink();
			}
		}
		
		if(isLocalPlayer){
			OverheatImageBlink();
		}
	}

	public void Shoot17(){
		heat += 20;
	}

	public void Shoot42(){
		heat42 += 40;
	}

	public void SetPlayerHeat(){
		if(isLocalPlayer){
			if(p.type == "standard" || p.type == "hero"){
				if(GetComponent<PlayerLevel>().level == 1){
					maxHeat = 90;
					coolingSpeed = 18;
				} else if(GetComponent<PlayerLevel>().level == 2){
					maxHeat = 180;
					coolingSpeed = 36;
				} else if(GetComponent<PlayerLevel>().level == 3){
					maxHeat = 360;
					coolingSpeed = 72;
				}
			} else if(p.type == "gunner"){
				maxHeat = 180;
				coolingSpeed = 36;
			}

			if(p.type == "hero"){
				if(GetComponent<PlayerLevel>().level == 1){
					maxHeat42 = 80;
					coolingSpeed42 = 20;
				} else if(GetComponent<PlayerLevel>().level == 2){
					maxHeat42 = 160;
					coolingSpeed42 = 40;
				} else if(GetComponent<PlayerLevel>().level == 3){
					maxHeat42 = 320;
					coolingSpeed42 = 80;
				}
			}
		}

	}

	//Change later
	void CmdOverHeat(){
		float _damage = (heat - maxHeat * 2) / 250 * p.maxHp;
		CmdDeductHp(_damage);
	}

	void CmdOverHeat42(){
		float _damage = (heat42 - maxHeat42 * 2) / 250 * p.maxHp;
		CmdDeductHp(_damage);
	}

	[Command]
	void CmdDeductHp(float _damage){
		p.hp -= _damage;
	}

	// void SetSentryHeat(){
	// 	maxHeat = 4500;
	// 	coolingSpeed = 1500;
	// }

	void Cooling42(){
		heat42 -= coolingSpeed42 / 10f * ( 1 + p.coolingBonus);
		if(heat42 > maxHeat42 * 2){
			CmdOverHeat42();
			heat42 = maxHeat42 * 2;
			overHeat42 = true;
			GC.gc.overHeatWarring.enabled = true;
		} else if(heat42 < maxHeat42){
			overHeat42 = false;
		} 
		if(heat42 < 0) {
			heat42 = 0;
		} 

		if(!overHeat42 && !overHeat) {
			GC.gc.overHeatWarring.enabled = false;
		}
	}

	void Cooling(){
		heat -= coolingSpeed / 10f * ( 1 + p.coolingBonus);
		if(heat > maxHeat * 2){
			CmdOverHeat();
			heat = maxHeat * 2;
			overHeat = true;
			GC.gc.overHeatWarring.enabled = true;
		} else if(heat < maxHeat){
			overHeat = false;
		} 
		if(heat < 0) {
			heat = 0;
		} 

		if( p.type != "hero"){
			if(!overHeat) {
				GC.gc.overHeatWarring.enabled = false;
			}
		}
	}

	float a;
	void OverheatImageBlink(){
		if(overHeat || overHeat42){
			a -= Time.deltaTime / 3f;
			if(a <= 0.4f){
				a = 0.8f;
			}
			GC.gc.overHeatWarring.color = new Color(GC.gc.overHeatWarring.color.r,GC.gc.overHeatWarring.color.g, GC.gc.overHeatWarring.color.b,a );
		}
	}
}
