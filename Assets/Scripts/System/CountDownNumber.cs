using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownNumber : MonoBehaviour {

	Text cdText;
	public float cdTime;
	float currentCdTime;
	// Use this for initialization
	void Start () {
		cdText = GetComponent<Text>();
	}

	void OnEnable() {
		currentCdTime = cdTime + 2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		startCountDown();
	}

	public void startCountDown(){

		currentCdTime -= Time.fixedDeltaTime;
		if(currentCdTime <= 0) {

			gameObject.SetActive(false);
		} else if(currentCdTime <= 2 ){
			cdText.text = "LAUNCH";
		} else if(currentCdTime > 2){
			cdText.text =  " " + Mathf.CeilToInt(currentCdTime - 2) + " ";
			
		}
	}
}
