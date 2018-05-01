using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Sentry : NetworkBehaviour {

	public SentryShoot ss;
	public SentryMove sm;
	public SentryHealth sh;

	// Use this for initialization
	void Start () {
		if(!isServer){
			ss.enabled = false;
			sm.enabled = false;
			sh.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
