using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarUI : MonoBehaviour {
 
	public string barType;
	public List<GameObject> bars;
	public float value;
	public float maxValue;

	void FixedUpdate () {
		Match();
	}

	void Match(){
		if(GC.gc.localPlayer){
			if(barType == "speed" && GC.gc.state == "playing"){
				maxValue = 26;
				value =  GC.gc.localPlayer._attackPower / maxValue;
				ShowBars();
			} else if(barType == "power" && GC.gc.state == "playing"){
				maxValue = 35;
				value =  GC.gc.localPlayer._power / maxValue;
				ShowBars();
			}
		}
		
	}

	void ShowBars(){
		for (int i = 0; i < bars.Count; i++){
			bars[i].SetActive(true);
		}
		if(value < 0.95f){
			bars[16].SetActive(false);
		}
		if(value < 0.9f){
			bars[15].SetActive(false);
		}
		if(value < 0.85f){
			bars[14].SetActive(false);
		}
		if(value < 0.8f){
			bars[13].SetActive(false);
		}
		if(value < 0.75f){
			bars[12].SetActive(false);
		}
		if(value < 0.70f){
			bars[11].SetActive(false);
		}
		if(value < 0.65f){
			bars[10].SetActive(false);
		}
		if(value < 0.60f){
			bars[9].SetActive(false);
		}
		if(value < 0.55f){
			bars[8].SetActive(false);
		}
		if(value < 0.50f){
			bars[7].SetActive(false);
		}
		if(value < 0.45f){
			bars[6].SetActive(false);
		}
		if(value < 0.4f){
			bars[5].SetActive(false);
		}
		if(value < 0.35f){
			bars[4].SetActive(false);
		}
		if(value < 0.3f){
			bars[3].SetActive(false);
		}
		if(value < 0.25f){
			bars[2].SetActive(false);
		}
		if(value < 0.2f){
			bars[1].SetActive(false);
		}
		if(value < 0.1f){
			bars[0].SetActive(false);
		}
	}

}
