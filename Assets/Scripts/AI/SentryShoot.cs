using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SentryShoot : NetworkBehaviour {

	public string faction;
	public bool isHero;
	public Player p;
	public List<Player> enemies;

	public Heat h;
	public bool aimed;
	public float shootPower;
	public float shootFreq;
	public float rotateSpeed;
	public float range;
	public int ammo;
	//For Testing
	public float cd;
	public float distance;
	public float currestClosetDis;

	public GameObject bulletPrefab;
	public Transform shootPoint;

	public Transform target;
	public Transform turret;
	public Transform aimObj;

	public bool blackOut;
	public float blackOutCD = 60;

	void GetEnemyList(){
		if(cd <= 0){
			enemies = new List<Player>();
			if(faction == "red"){
				//for (int i = 0; i < GC.gc.bluePlayers.Count; i++){
					//enemies.Add(GC.gc.bluePlayers[i].gameObject);
				//}
				enemies = GC.gc.bluePlayers;
			}
			else if (faction == "blue") {
				//for (int i = 0; i < GC.gc.redPlayers.Count; i++){
					//enemies.Add(GC.gc.redPlayers[i].gameObject);
				//}
				enemies = GC.gc.redPlayers;
			}
			if(GC.gc.state == "end"){
				GetComponent<SentryMove>().enabled = false;
				GetComponent<SentryHealth>().enabled = false;
				this.enabled = false;
			}
		}
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(GC.gc.state != "playing"){
			return;
		}
		if(!isHero && GC.gc.firstBlood && blackOutCD > 0){
			blackOut = true;
			blackOutCD -= Time.time;
			if(blackOutCD <= 0){
				blackOutCD = 0;
				blackOut = false;
			}
		}

		if(!blackOut){
			GetEnemyList();
			FindTarget();
			if(target && isHero){
				aimObj.LookAt(target);
				CheckAngle();
			}
			Aim(target);

			if(p){
				ammo = p.ammo;
			}
			if(target && ammo > 0){

				cd -= Time.fixedDeltaTime;
				if(cd <= 0) {

					LocalShoot();
					CmdShoot();
					cd = shootFreq;
					if(p){
						p.ammo --;;
					} else {
						ammo --;
					}
				}
			} else if(!isHero){
				Spin();
			}
		}
	
	}

	void SynAmmo(){
		if(p){
			ammo = p.ammo;
		}
	}

	void Spin(){		
	   	Vector3 _bodyRotation = new Vector3(0f, rotateSpeed / 2f, 0f);

		turret.Rotate(_bodyRotation);
	}

	[Command]
	public void CmdShoot() {
		GameObject _bulletCopy = Instantiate(GC.gc.bullet_small, transform);
		_bulletCopy.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootPower * 70f);
		
		NetworkServer.Spawn(_bulletCopy);
		_bulletCopy.transform.parent = null;
		_bulletCopy.transform.position = shootPoint.transform.position;
		_bulletCopy.GetComponent<Bullet>().shooter = this.gameObject;

	}

	void LocalShoot() {
		GameObject _bulletLocalCopy = Instantiate(GC.gc.bullet_small_local, shootPoint);
		_bulletLocalCopy.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootPower * 70f);
		_bulletLocalCopy.transform.position = shootPoint.position;
	}


	void FindTarget(){
		currestClosetDis = range;
		for (int i = 0; i < enemies.Count; i++){
			if( enemies[i].GetComponent<Player>().state == "normal" && enemies[i].GetComponent<Player>().type != "pilot" && enemies[i].GetComponent<Player>().type != "gunner"){
				float _dis = CountDis(enemies[i].transform);
				if(_dis < currestClosetDis){
					target = enemies[i].transform;
					currestClosetDis = _dis;
				} else {
					target = null;
				}
			} else {
				target = null;
			}
		
		}

		if(target){
			FindArmorPlate();
			if(CountDis(target) > range){
				target = null;
			} else if(target.CompareTag("Player")){
				if(target.GetComponent<Player>().state != "normal"){
					target = null;
				}
			} else if(target.CompareTag("ArmorPlate")){
				if(target.parent.GetComponentInParent<Player>().state != "normal"){
					target = null;
				}
			}
		}
	}

	void CheckAngle(){
		print(transform.eulerAngles.y - aimObj.eulerAngles.y);
		if(transform.eulerAngles.y - aimObj.eulerAngles.y  < 135 || transform.eulerAngles.y - aimObj.eulerAngles.y > 225){
			target = null;
		}
	}

	void FindArmorPlate(){
		if(target.Find("ArmorPlates")){
			//List<Transform> _plates = new List<Transform>();
			int _index = 0;
			for (int i = 0; i < target.Find("ArmorPlates").childCount; i++){
				float _dis = CountDis(target.Find("ArmorPlates").GetChild(i));
				if(_dis < currestClosetDis){
					_index = i;
					currestClosetDis = _dis;
				} 
			}

			target = target.Find("ArmorPlates").GetChild(_index);
		}

	}

	float CountDis(Transform _target){
		distance = Vector3.Distance(transform.position, _target.position );
		return distance;
	}

	void Aim(Transform _target){
		if(_target){
			Vector3 targetDir = target.position - turret.position;

			// The step size is equal to speed times frame time.
			float step = rotateSpeed * Time.deltaTime;

			Vector3 newDir = Vector3.RotateTowards(turret.forward, targetDir, step, 0.0f);

			// Move our position a step closer to the target.
			turret.rotation = Quaternion.LookRotation(newDir);
		}
	}
}
