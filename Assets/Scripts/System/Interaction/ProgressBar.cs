using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	public Image progressbar;
	public Image progressbarBG;
	public float progress;
	public float total;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ShowBar();
	}

	public void SetProgress(float _progress, float _total){
		progress = _progress;
		total = _total;
	}

	public void ShowBar(){
		Vector2 _scale = new Vector2(progress/total, 1);
		progressbar.rectTransform.localScale = _scale;
	}
}
