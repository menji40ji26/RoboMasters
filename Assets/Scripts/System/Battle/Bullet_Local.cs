using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Local : MonoBehaviour {

	bool stop = false;
	
	void Start () {
		transform.parent = null;
	}

	void Update () {
		if(stop){
			Destroy(this.gameObject, 2f);
		}

	}

	void OnCollisionEnter(Collision other){
		stop = true;
    }
}
