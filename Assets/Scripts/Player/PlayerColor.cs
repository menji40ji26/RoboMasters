using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {



	public Color red;
	public Color blue;
	public MeshRenderer body;

	// Use this for initialization
	void Start () {
		//rends[i].material.color = color;
		
	}
	
	
	public void ClearColor(){
		body.material.color = Color.clear;
	}

	public void SetRed(){
		body.material.color = red;
	}

	public void SetBlue(){
		body.material.color = blue;
	}


}
