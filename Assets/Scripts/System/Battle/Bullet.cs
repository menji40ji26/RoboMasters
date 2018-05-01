using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	public bool stop = false;
	public float damage;
	public float damageBonus;
	public GameObject shooter;

	// // Use this for initialization
	// void Start () {
	// 	transform.parent = null;
	// }
	void start(){
		Destroy(this.gameObject, 3f);
	}
	
	// Update is called once per frame
	//[ServerCallback]
	void Update () {
		if(stop){
			Destroy(this.gameObject, 2f);
		}

	}

	void OnCollisionEnter(Collision other)
    {

		if(!isServer)
			return;
		if(stop)
			return;

		if(other.gameObject.tag == "ArmorPlate"){
			GetTarget(other.transform.parent.name);
		}


		stop = true;

		// if(other.gameObject.tag == "Player"){
		// 	HitPlayer(other.gameObject.GetComponent<Player>());
		// } else if(other.gameObject.tag == "Base"){
		// 	HitBase(other.gameObject);
		// }  else if(other.gameObject.tag == "Sentry"){
		// 	HitSentry(other.gameObject.GetComponent<SentryHealth>());
		// } 
    }

	void GetTarget(string _name){
		if(_name == "Blue_Standard1_DamageSensor"){
			HitPlayer(GC.gc.pl.standard1_Blue);
		} else if(_name == "Blue_Standard2_DamageSensor"){
			HitPlayer(GC.gc.pl.standard2_Blue);
		} else if(_name == "Blue_Standard3_DamageSensor"){
			HitPlayer(GC.gc.pl.standard3_Blue);
		} else if(_name == "Red_Standard1_DamageSensor"){
			HitPlayer(GC.gc.pl.standard1_Red);
		} else if(_name == "Red_Standard2_DamageSensor"){
			HitPlayer(GC.gc.pl.standard2_Red);
		} else if(_name == "Red_Standard3_DamageSensor"){
			HitPlayer(GC.gc.pl.standard3_Red);
		} else if(_name == "Red_Hero_DamageSensor"){
			HitPlayer(GC.gc.pl.hero_Red);
		} else if(_name == "Blue_Hero_DamageSensor"){
			HitPlayer(GC.gc.pl.hero_Blue);
		} else if(_name == "Blue_Sentry_DamageSensor"){
			HitSentry(GC.gc.pl.sentry_Blue.sh);
		} else if(_name == "Red_Sentry_DamageSensor"){
			HitSentry(GC.gc.pl.sentry_Red.sh);
		} else if(_name == "Red_Base_DamageSensor"){
			HitBase(GC.gc.baseRed);
		} else if(_name == "Blue_Base_DamageSensor"){
			HitBase(GC.gc.baseBlue);
		}
	}

	//void OnTriggerEnter(Collider other){
		// if(!isServer)
		// 	return;
		// stop = true;

		// print(other.gameObject.tag);
		// if(other.gameObject.tag == "ArmorPlate"){
		// 	print("Hit Armor");
		// 	if(other.transform.parent.parent.tag == "Player")
		// 	HitPlayer(other.gameObject);
		// 	if(other.transform.parent.parent.tag == "Sentry")
		// 	HitSentry(other.gameObject);
		// }
	//}

	void HitPlayer(Player _player){
		if(_player.hp > 0) {

			_player.hp -= damage * ( 1 - _player.defence ) * ( 1 + damageBonus );
			if(_player.hp <= 0){
				_player.hp = 0;
				print(shooter + " destroyed " + _player);

				CheckFirstBlood();
				if(shooter.tag == "Player")
				shooter.GetComponent<PlayerLevel>().GetKillExp(_player.type, _player.GetComponent<PlayerLevel>().level);
			}
		}
	}

	void HitSentry(SentryHealth _sentry){
		if(_sentry.hp > 0) {
			_sentry.GetHit(damage, damageBonus);
			if(_sentry.hp <= 0){
				print(shooter + " destroyed " + _sentry);
				CheckFirstBlood();
				if(shooter.tag == "Player")
				shooter.GetComponent<PlayerLevel>().GetKillExp("sentry", 1);
			}
		}
		
	}

	void HitBase(Base _base){
		_base.Hit(damage, damageBonus);
	}

	void CheckFirstBlood(){
		if(!GC.gc.firstBlood){
			if(shooter.tag == "Player")
				shooter.GetComponent<PlayerLevel>().GetFirstBlood();
			GC.gc.firstBlood = true;
		}
	}


	// public void fly(Transform shootPoint, float power){
	// 	damage = power;
	// }
}
