using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLevel : NetworkBehaviour {
	
	[SyncVar]
	public int level;
	[SyncVar]
	public float exp;
	public float expToNext;
	public float surviveTime;
	public Player p;


	public void Setup(){
		level = 1;
		CountLevel();
		SetDataUI();
	}
	
	void FixedUpdate () {
		CountTime();
	}

	void CountTime(){
		if(isLocalPlayer && GC.gc.state == "playing" && p){
			if(p.state == "normal" ){
				surviveTime += Time.deltaTime;
				AutoGainExp();
			}
		}
	}

	void CountLevel(){
		if(p.type == "standard"){
			if(level == 1) {
				expToNext = 4;
			} else if(level == 2) {
				expToNext = 8;
			}
		} else if(p.type == "hero"){
			if(level == 1) {
				expToNext = 8;
			} else if(level == 2) {
				expToNext = 12;
			}
		}

		if(p.type == "standard"){
			if(level == 1 && exp >= expToNext){
				level ++;
				exp -= expToNext;
				p.maxHp = 1000;
				p.hp += 250;
				p.attackSpeed = 0.33f;
			} else if(level == 2 && exp >= expToNext){
				level ++;
				exp -= expToNext;
				p.maxHp = 1500;
				p.hp += 500;
				p.attackSpeed = 0.125f;
			}
		} else if(p.type == "hero"){
			if(level == 1 && exp >= expToNext){
				level ++;
				exp -= expToNext;
				p.maxHp = 2500;
				p.hp += 1000;
				p.attackSpeed = 0.5f;
			} else if(level == 2 && exp >= expToNext){
				level ++;
				exp -= expToNext;
				p.maxHp = 3500;
				p.hp += 1000;
				p.attackSpeed = 0.5f;
			}
		}

		if(p.hp>p.maxHp){
			p.hp = p.maxHp;
		}

		if(GetComponent<PlayerSetup>().isLocalPlayer){
			GC.gc.maxHPText.text = "/ " + p.maxHp.ToString();
		}
		GetComponent<Heat>().SetPlayerHeat();

		SetDataUI();
	}

	void SetDataUI(){
		if(isLocalPlayer){
			GC.gc.customData3Text.text = "Level [Exp]";
			GC.gc.customData3NumberText.text = level + " [" + exp.ToString() + " / " + expToNext.ToString() + "] " ;
		}
	}

	void AutoGainExp(){
		if(surviveTime >= 60){
			if(p.type == "standard"){
				exp ++;
			} else if(p.type == "hero"){
				exp += 2;
			}
			surviveTime = 0;
			CountLevel();
		}
	}

	public void GetFirstBlood(){
		exp += 5;
		CountLevel();
	}

	public void GetKillExp(string _type, int _level){
		if(_type == "standard"){
			if(_level == 1)
				exp += 2.5f;
			else if(_level == 2)
				exp += 5f;
			else if(_level == 3)
				exp += 7.5f;
		} else if(_type == "hero"){
			if(_level == 1)
				exp += 7.5f;
			else if(_level == 2)
				exp += 10f;
			else if(_level == 3)
				exp += 15f;
		} else if(_type == "sentry"){
				exp += 7.5f;
		} else if(_type == "engineer"){
				exp += 5f;
		} else if(_type == "bunker"){
				exp += 5f;
		}
		CountLevel();
	}
}
