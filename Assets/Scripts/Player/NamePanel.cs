using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePanel : MonoBehaviour {

	TextMesh nameText;

	// Use this for initialization
	void Start () {
		nameText = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		SetNamePanel();
	}

	void FixedUpdate(){
		if(Camera.current)
		transform.LookAt(Camera.current.transform);
	}

	public void SetNamePanel(){
		nameText.text = transform.parent.name;
	}
}
