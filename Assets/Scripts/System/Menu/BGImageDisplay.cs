using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGImageDisplay : MonoBehaviour {

	public Sprite[] bgImages;
	public Image bgImage;
	public int countDownTime;
	public int currentTime;
	public Text introText;
	public int introCurrentTime;
	public Image mask;

	// Use this for initialization
	void Start () {
		currentTime = countDownTime;
		SetImage();
	}
	

	void SetImage(){
		bgImage.sprite = bgImages[Random.Range(0, bgImages.Length)];
	}

	float x = -50;
	float y = -20;


	// Update is called once per frame
	void FixedUpdate () {
		CountIntro();
		CountBGImage();
	}


	void CountIntro(){
		introCurrentTime --;
		if(introCurrentTime <= 0){
			introText.transform.parent.gameObject.SetActive(false);
		}
	}

	void CountBGImage(){

		if(introCurrentTime <= 0){
			currentTime --;
			x += 0.1f;
			y += 0.03f;
			GetComponent<RectTransform>().localPosition = new Vector3(x,y,0);
			if(currentTime <= 0) {

				currentTime = countDownTime;
				x = -50;
				y = -20;
				SetImage();

			} if(currentTime == 130){

				GetComponent<Animator>().SetTrigger("Fade");
			}
		}
		
	}
}
