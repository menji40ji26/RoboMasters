using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour {

	static public Hint h;
	public Text hintText;
	Color c;
	public string message;


	// Use this for initialization
	void Start () {
		h = this;
		hintText.text = null;
		c = hintText.color;
	}
	
	// Update is called once per frame
	void Update () {
		HintFade();
	}

	public void SetHint(string _hint){
		hintText.text = _hint;
		c.a = 1f;
	}

	void HintFade(){
		if(hintText.text != null){
			c.a -= Time.deltaTime/5f;
			hintText.color = c;
			if(c.a <= 0){
				hintText.text = null;
			}
		}
	}

	//Hint List
	public void ClimbHint(){
		message = "Hold [C] To Climb";
		SetHint(message);
	}

	public void GetChestHint(){
		message = "Press [C] To Reload Ammo";
		SetHint(message);
	}

	public void HeroHint(){
		message = "Hold [Z][X] To Lift Or Lower";
		SetHint(message);
	}

	public void PilotHint(){
		message = "Press [Ctrl][Space] To Move Vertically";
		SetHint(message);
	}
}
