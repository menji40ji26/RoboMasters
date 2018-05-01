using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SentryHealth : NetworkBehaviour {

	public float hp;

	void Start(){
		hp = GC.gc.maxHp_Sentry;
	}

	public void GetHit(float _damage, float _bonus){
		hp -= _damage * ( 1 + _bonus);

		if(hp <= 0) {
			Die();
		}
	}

	void Die(){
		hp = 0;
		// if(GetComponent<SentryShoot>().faction == "red"){
		// 	GameObject.Find("Base_Red").GetComponent<Base>().sentryAlive = false;
		// 	GameObject.Find("RedBaseLockIcon").SetActive(false);
		// } else if(GetComponent<SentryShoot>().faction == "blue"){
		// 	GameObject.Find("Base_Blue").GetComponent<Base>().sentryAlive = false;
		// 	GameObject.Find("BlueBaseLockIcon").SetActive(false);
		// }
		RpcUnlockBase();
		GetComponent<SentryMove>().enabled = false;
		GetComponent<SentryShoot>().enabled = false;
		this.enabled = false;
	}

	[ClientRpc]
	void RpcUnlockBase(){
		if(GetComponent<SentryShoot>().faction == "red"){
			GameObject.Find("Base_Red").GetComponent<Base>().sentryAlive = false;
			GameObject.Find("RedBaseLockIcon").SetActive(false);
			GC.gc.baseRed.GetComponent<Animator>().SetTrigger("Open");
			GC.gc.baseRed.GetComponent<Animator>().SetBool("Opened", true);
		} else if(GetComponent<SentryShoot>().faction == "blue"){
			GameObject.Find("Base_Blue").GetComponent<Base>().sentryAlive = false;
			GameObject.Find("BlueBaseLockIcon").SetActive(false);
			GC.gc.baseBlue.GetComponent<Animator>().SetTrigger("Open");
			GC.gc.baseRed.GetComponent<Animator>().SetBool("Opened", true);
		}
	}

}
