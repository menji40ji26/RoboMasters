using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager  {

	public Text ipAdressText;
	public string ipAdress;

	public void StartHosting() {
		base.StartHost();
	}


	public void Join(){
		SetIP();
		if(ipAdress != null)
		base.StartClient();

	}

	public void SetIP(){
		ipAdress = ipAdressText.text;
		NetworkManager.singleton.networkAddress = ipAdress;
	}

	// void OnLevelWasLoaded(int level) {
	// 	if(level == 1){
	// 		GameObject.Find("LeaveButton").GetComponent<Button>().onClick.RemoveAllListeners();
	// 		GameObject.Find("LeaveButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
	// 	}
	// }
	// void OnEnable()
	// {
	// 	SceneManager.sceneLoaded += Loadedscene;
	// }

	// void OnDisable()
	// {
	// 	SceneManager.sceneLoaded -= Loadedscene;
	// }

	// public GameObject leaveButton;

	// void Loadedscene(Scene scene,LoadSceneMode mode)
	// {
	// 	scene = SceneManager.GetActiveScene();
	// 	//print(scene.buildIndex);
	// 	leaveButton = GameObject.Find("LeaveButton");
	// 	//if(leaveButton){
			
	// 		leaveButton.GetComponent<Button>().onClick.RemoveAllListeners();
	// 		leaveButton.GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);

	// 	//}
	// }


}
