using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraRotate : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		Rotate();
	}

	void Rotate(){
		transform.Rotate(new Vector3(0f, 0.05f, 0f));
	}
}
