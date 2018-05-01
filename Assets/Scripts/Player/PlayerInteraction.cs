using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInteraction : NetworkBehaviour {

	public string onSensor;
	public float climbSpeed = 1;
	public float climbTime;
	public float maxClimbTime;
	public float getChestTime;
	public float maxGetChestTime;
	public GameObject nextStair;
	public Transform chestPoint;
	public Chest targetChest;
	public string action;

	void FixedUpdate(){
		action = null;
		if(isLocalPlayer)
		if(GetComponent<Player>().type == "hero" || GetComponent<Player>().type == "engineer"){
			GetChest();
			if(GetComponent<Player>().type == "engineer"){
				Climb();
			}
		}
	}

	void Climb(){
		if(onSensor == "stair" && nextStair){
			if(Input.GetKey(KeyCode.C)){
				climbTime += Time.deltaTime;
				SetProgressBar(climbTime, maxClimbTime);
				action = "climbing";
			} else {
				climbTime = 0;
			}

			if(climbTime >=  maxClimbTime) {
				MoveToPosition();
				climbTime = 0;
				GC.gc.progressBar.SetActive(false);
			}
		}

		if(Input.GetKeyUp(KeyCode.C)){
			GC.gc.progressBar.SetActive(false);
		}
	}

	void GetChest(){
		RaycastHit hit;
		Ray findChestRay = new Ray(GetComponent<ComponentChange>().upperCam.transform.position, GetComponent<ComponentChange>().upperCam.transform.forward);
		if(Physics.Raycast(findChestRay, out hit, 1)){
			if(hit.collider.CompareTag("Chest")){
				GC.gc.hint.GetChestHint();

				if(!targetChest){
					if(Input.GetKeyDown(KeyCode.C)){
						targetChest = hit.collider.GetComponent<Chest>();
						GetComponent<Animator>().SetTrigger("Catch");
					}
				}
				if(targetChest && getChestTime < maxGetChestTime ){
					getChestTime += Time.deltaTime;
					SetProgressBar(getChestTime, maxClimbTime);
					action = "gettingChest";
					targetChest.transform.SetParent(chestPoint);
					targetChest.transform.localPosition = Vector3.zero;
					targetChest.transform.rotation = chestPoint.transform.rotation;
				} else {
					// if(targetChest){
					// 	targetChest.transform.SetParent(null);
					// }
					getChestTime = 0;
				}
				


				if(getChestTime >= maxGetChestTime && targetChest) {
					targetChest.transform.SetParent(null);
					targetChest.TransferAmmo(GetComponent<Player>());
					targetChest = null;
					getChestTime = 0;
					GC.gc.progressBar.SetActive(false);
				}
			}
		}

		if(Input.GetKeyUp(KeyCode.C)){
			GC.gc.progressBar.SetActive(false);
		}

	}

	void SetProgressBar(float _progress, float _total){
		GC.gc.progressBar.SetActive(true);
		GC.gc.progressBar.GetComponent<ProgressBar>().SetProgress(_progress,_total);
	}

	void MoveToPosition(){
		transform.position = nextStair.transform.position;
	}


}
