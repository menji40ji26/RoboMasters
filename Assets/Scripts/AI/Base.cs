using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Base : NetworkBehaviour {

	public float maxHp;
	public string faction;

	[SyncVar]
	public bool sentryAlive = true;
	
	[SyncVar]
	public float hp;
	public Image baseHealthBar;

	// Use this for initialization
	void Start () {
		hp = maxHp;
		if(faction == "red"){
			GC.gc.baseRed = this;
		} else if (faction == "blue"){
			GC.gc.baseBlue = this;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckHp();
	}


	// void OnCollisionEnter(Collision other) {
	// 	if (other.gameObject.CompareTag("Projectile")){
	// 		hp -= 30;
	// 		CmdCheckHp();
	// 	}
    // }

	public void Hit(float _damage, float _bonus){

		if(sentryAlive)
			hp -= _damage * 0.5f * ( 1 + _bonus );
		else 
			hp -= _damage * ( 1 + _bonus );

	}

	void CheckHp(){

		//transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
		if(hp <= 0){
			if(faction == "red"){
				GC.gc.GetComponent<GameState>().blueWin();
			} else if(faction == "blue"){
				GC.gc.GetComponent<GameState>().redWin();
			} 
			hp = 0;
		}

		float _x = hp/maxHp;
		baseHealthBar.rectTransform.localScale = new Vector3(_x,1,1);
	}
}
