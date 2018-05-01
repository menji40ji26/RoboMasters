using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GC : MonoBehaviour {

	public string state;
	public bool paused = true;
	public bool firstBlood;
	public Player localPlayer;
	static public GC gc;
	private const string PLAYER_ID_PREFIX = "Player ";

	public static Dictionary<string, Player> players = new Dictionary<string, Player>();
	public List<Player> redPlayers;
	public List<Player> bluePlayers;

	//Prefabs
	public GameObject bullet_big;
	public GameObject bullet_small;
	public GameObject bullet_small_local;
	public GameObject bullet_big_local;
	public GameObject ammo_small;

	//GameObjects
	public PlayerList pl;
	public NM nm;
	public Base baseRed;
	public Base baseBlue;
	

	//Cam
	public Camera mainCam;
	public Camera pilotRedCam;
	public Camera pilotBlueCam;

	//UI
	public Text playerNameText;
	public Text playerRoleText;
	public Text speedText;
	public Text frequenceText;
	public Text powerText;
	public Image overHeatWarring;
	public GameObject setupUI;
	public GameObject battleUI;
	public GameObject gameStateUI;
	public TeamSetup teamSetup;
	public GameObject progressBar;
	public Hint hint;

	//Var
	public float cameraSpeed;
	public float acceleration_standard;
	public float acceleration_hero;
	public float acceleration_engineer;
	public float acceleration_drone;
	public float maxSpeed_standard;
	public float maxSpeed_hero;
	public float maxSpeed_engineer;
	public float maxSpeed_drone;
	public float attackSpeed_standard;
	public float attackSpeed_hero;
	public float attackSpeed_drone;
	public float attackPower_standard;
	public float attackPower_hero;
	public float attackPower_drone;
	public float maxHp_Standard;
	public float maxHp_hero;
	public float maxHp_engineer;
	public float maxHp_drone;
	public float maxHp_Sentry;


	public Text customData1Text;
	public Text customData1NumberText;
	public Text customData2Text;
	public Text customData2NumberText;
	public Text customData3Text;
	public Text customData3NumberText;
	public Text currentHPText;
	public Text maxHPText;

	public Text timeText;
	public CountDownNumber cdn;
	public List<Transform> redSpawnPoints;
	public List<Transform> blueSpawnPoints;
	//public Transform[] redSpawnPoints;
	//public Transform[] blueSpawnPoints;

	public float delay;
	public float time;
	float currentTime;
	public Text testText;

	//Effect


	void Awake() {
		gc = this;
	}
	// Use this for initialization
	// void Start () {
		
    //     //Screen.SetResolution(1600, 900, true);
	// 	gc = this;
		
	// }
	
	// Update is called once per frame
	void FixedUpdate () {

		begin();
		if (Input.GetKeyUp(KeyCode.Escape)){
			paused = !paused;
		}
		if(paused || state == "end"){
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		} else {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	bool began = false;

	public void begin() {
		if(!began &&  GC.players.Count > 0){
			//GameObject.Find("SentryBlue").GetComponent<SentryShoot>().Setup();
			CheckPlayers();
			if(redPlayers.Count + bluePlayers.Count == GC.players.Count)
			began = true;
		}
	}

	public void RegisterPlayer(string _netID, Player _player){
		string _playerID = PLAYER_ID_PREFIX + _netID;
		//string _playerID = _player;
		players.Add(_playerID, _player);
		//_player.transform.name = _playerID;

	}

	public void SetupLocalPlayer(string _netID, Player _player){
		string _playerID = PLAYER_ID_PREFIX + _netID;
		//string _playerID = GameObject.Find("LobbyManager").GetComponent<SaveLobbyNames>().playerName;
		playerNameText.text = _playerID;
		playerRoleText.text = "Standard";
		teamSetup.localPlayer = _player;
		localPlayer = _player;
	}

	public void UnRegisterPlayer(string _playerID){
		players.Remove(_playerID);
	}

	public static Player GetPlayer(string _playerID){
		return players[_playerID];
	}

	void OnGUI(){




		if(Input.GetKey(KeyCode.Tab)){
			//Red
			GUILayout.BeginArea(new Rect(200, 200, 200, 500));
			GUILayout.BeginVertical();

			for (int i = 0; i < redPlayers.Count; i++){
				
				if(redPlayers[i].hp <= 0) redPlayers[i].hp = 0;
				GUILayout.Label("Red " + redPlayers[i].transform.name + " " + Mathf.RoundToInt(redPlayers[i].hp).ToString());
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();

			GUILayout.BeginArea(new Rect(600, 200, 200, 500));
			GUILayout.BeginVertical();
			foreach (string _playerID in players.Keys){
				if(players[_playerID].hp <= 0) players[_playerID].hp = 0;
				GUILayout.Label(_playerID + " " + Mathf.RoundToInt(players[_playerID].hp).ToString());
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();

			//Blue
			GUILayout.BeginArea(new Rect(400, 200, 200, 500));
			GUILayout.BeginVertical();
			for (int i = 0; i < bluePlayers.Count; i++){
				if(bluePlayers[i].hp <= 0) bluePlayers[i].hp = 0;
				GUILayout.Label("Blue " + bluePlayers[i].transform.name + " " + Mathf.RoundToInt(bluePlayers[i].hp).ToString());
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
			
		}
		
	}

	void CheckPlayers(){
		redPlayers = new List<Player>();
		bluePlayers = new List<Player>();
		foreach (string _playerID in players.Keys){
			//CheckPlayers(players[_playerID]);
			
			if(players[_playerID].faction == "red"){
				redPlayers.Add(players[_playerID]);
			} else if(players[_playerID].faction == "blue"){
				bluePlayers.Add(players[_playerID]);
			}
		}

	}

	void OnDisable (){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

	}

}
