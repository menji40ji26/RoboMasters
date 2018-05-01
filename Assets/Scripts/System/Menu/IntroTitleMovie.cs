using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTitleMovie : MonoBehaviour {

	public int cd;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		cd --;
		if(cd <= 0){
			SceneManager.LoadScene(1);
		}
	}
}
