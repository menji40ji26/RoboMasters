using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NM : NetworkManager  {
	
	// public override void OnStartServer(){
    //     base.OnStartServer();
    //     GC.gc.cdn.gameObject.SetActive(true);
    //     //GC.state = "Playing";
    //     GC.gc.begin();
    // }

    // void Start() {

    //     //GC.gc.cdn.gameObject.SetActive(true);
    //     GC.state = "Playing";
    //     //GC.gc.begin();
    // }

    // public void StartupHost(){

    //     SetPort();
    //     NetworkManager.singleton.StartHost();
    // }

    // public void JoinGame(){
    //     SetIpAddress();
    //     SetPort();
    //     NetworkManager.singleton.StartClient();
    // }

    // void SetIpAddress(){
    //     string ip = GameObject.Find("InputFieldIPAddress").transform.Find("IPText").GetComponent<Text>().text;
    //     NetworkManager.singleton.networkAddress = ip;
    // }  
    // void Awake()
    // {
    //     Debug.Log("Awake");
    // }

    // void SetPort(){
    //     NetworkManager.singleton.networkPort = 189218;
    // }

    // void OnEnable()
    // {

    //     Debug.Log("OnEnable called");
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    //     // if(scene.name == "Main"){
    //     //     SetupMenuSceneButtons();
    //     // } else {
    //     //     SetupOtherSceneButtons();
    //     // }
        
    //     // Debug.Log("OnSceneLoaded: " + scene.name);
    //     // Debug.Log(mode);
    // }

    // // called when the game is terminated
    // void OnDisable()
    // {
    //     GC.state = "preparing";
    //     Debug.Log("OnDisable");
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // void SetupMenuSceneButtons(){

    //     NetworkServer.Reset();
    //     GameObject.Find("HostButton").GetComponent<Button>().onClick.RemoveAllListeners();
    //     GameObject.Find("HostButton").GetComponent<Button>().onClick.AddListener(StartupHost);

    //     GameObject.Find("JoinButton").GetComponent<Button>().onClick.RemoveAllListeners();
    //     GameObject.Find("JoinButton").GetComponent<Button>().onClick.AddListener(JoinGame);
    // }

    // void SetupOtherSceneButtons(){
    //     GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.RemoveAllListeners();
    //     GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    //     NetworkManager.singleton.StopClient();
    //     NetworkManager.singleton.StopHost();
    //     NetworkManager.singleton.StopMatchMaker();
    //     Network.Disconnect();

    // }

}
