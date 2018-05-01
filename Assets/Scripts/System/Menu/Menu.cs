using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject menuUI;
	public GameObject networkUI;
	public GameObject networkManager;

	// Use this for initialization
	void Start () {
		
	}

	public void EnterLobby() {
		SceneManager.LoadScene(2);
	}

	public void Quit() {
		Application.Quit();
	}

	public void OpenNetworkUI(){
		networkManager.SetActive(true);
		networkUI.SetActive(true);
	}

	public void CloseNetworkUI(){
		networkManager.SetActive(false);
		networkUI.SetActive(false);

	}

	public void OpenMenuUI(){
		menuUI.SetActive(true);
		
	}

	public void CloseMenuUI(){
		menuUI.SetActive(false);
		
	}

}
