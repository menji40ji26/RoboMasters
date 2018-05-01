using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	[SyncVar]
	public string role;
	[SyncVar]
	public string type;
	[SyncVar]
	public string state;
	[SyncVar]
	public float hp;
	[SyncVar]
	public float maxHp;
	[SyncVar]
	public string faction;
	[SyncVar]
	public float defence;
	[SyncVar]
	public float coolingBonus;
	public float cameraSpeed;
	public float acceleration;
	public float speed;
	public float maxSpeed;
	public float _power;
	public float attackSpeed;
	public float asBonus = 1;
	public float attackPower;
	public float attackPower_offset;
	public float _attackPower;
	public float damageBonus;
	public int ammo;
	public int ammo42;
	Text ammoText;
	Text ammoNumberText;

	float lastShoot;
	public GameObject shootPoint;
	public Transform turrent;
	public Transform holder;
    public Rigidbody rb;


	private Vector3 velocity = Vector3.zero;
	private Vector3 bodyRotation = Vector3.zero;
	private Vector3 holderRotation = Vector3.zero;
	private Vector3 turretRotation = Vector3.zero;
	public PlayerShoot playerShoot;
	public Animator playerAnimator;
	public ComponentChange cc;


	[ClientRpc]
	public void RpcBegin(string _type, string _role, string _faction){
		faction = _faction;
		role = _role;
		type = _type;
		gameObject.name = _type + faction;
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
		playerAnimator.enabled = true;
		cc.SetupComponent(type);
		GetComponent<PlayerRole>().SetType();
		if(type == "gunner"){
			GetComponent<NetworkTransform>().interpolateMovement = 0.1f;
		} else if (type == "pilot"){
			GetComponent<NetworkTransform>().interpolateMovement = 0.5f;
		}
		if(isLocalPlayer){
			GetComponentInChildren<Camera>().enabled = true;
			//GetComponentInChildren<AudioListener>().enabled = true;
			GetComponent<PlayerLevel>().Setup();
			GetComponent<Heat>().Setup(type);
			GC.gc.mainCam.gameObject.SetActive(false);
			ammoText = GC.gc.customData1Text;
			ammoNumberText = GC.gc.customData1NumberText;
			ammoText.text = "Ammo";
			lastShoot = 0;
			RefreshText();
			GC.gc.maxHPText.text = "/ " + maxHp.ToString();
		}

		state = "normal";
		if(isServer)
		GC.gc.pl.CmdAddPlayer(gameObject, role, faction);
	}

	[Command]
	public void CmdSetRed(){
		faction = "red";
	}
	[Command]
	public void CmdSetBlue(){
		faction = "blue";
	}


	void FixedUpdate(){
		// if(state == "prepare"){
		// 	Begin();
		// }

		SetColor();
		checkState();
		if(isLocalPlayer && GC.gc.state == "playing"){
			if(GetComponent<PlayerInteraction>().action == null){
				command();
				action();
			}
			CheckCondition();
			RefreshText();
		} 

	}

	void SetColor(){

		if(faction == "red"){
			GetComponent<PlayerColor>().SetRed();
		} else if(faction == "blue"){
			GetComponent<PlayerColor>().SetBlue();
		}
	}

	float yTurn;
	float ySpeed;
	float zSpeed;
	float xSpeed;
	float drag = 0.3f;
	float yRot;
	float xRot;
	float holderSpeed;
	float turretSpeed;

	void movement() {


		float droneDrag = 1f;
		if(type == "pilot"){
			droneDrag = 0.05f;
		}

		//Vertical Move
		if(forward && zSpeed < maxSpeed){

			zSpeed += acceleration * 0.01f;
		} else if (!forward && zSpeed > 0f) {
			zSpeed -= drag * droneDrag;
			if(zSpeed < 0f) {
				zSpeed = 0f;
			}
		}

		if(backward && zSpeed > -maxSpeed){
			zSpeed -= acceleration * 0.01f;
		} else if (!backward && zSpeed < 0f) {
			zSpeed += drag * droneDrag;
			if(zSpeed > 0f) {
				zSpeed = 0f;
			}
		}

		//Horizontal Move
		if(leftward && xSpeed < maxSpeed / 2){
			xSpeed += acceleration * 0.01f;
		} else if (!leftward && xSpeed > 0f) {
			xSpeed -= drag * droneDrag;
			if(xSpeed < 0f) {
				xSpeed = 0f;
			}
		}

		if(rightward && xSpeed > -maxSpeed / 2){
			xSpeed -= acceleration * 0.01f;
		} else if (!rightward && xSpeed < 0f) {
			xSpeed += drag * droneDrag;
			if(xSpeed > 0f) {
				xSpeed = 0f;
			}
		}


		//Height Move
		if(up && ySpeed < maxSpeed / 4){
			ySpeed += acceleration * 0.01f;
		} else if (!up && xSpeed > 0f) {
			ySpeed -= drag * droneDrag;
			if(ySpeed < 0f) {
				ySpeed = 0f;
			}
		}

		if(down && ySpeed > -maxSpeed / 4){
			ySpeed -= acceleration * 0.01f;
		} else if (!down && ySpeed < 0f) {
			ySpeed += drag * droneDrag;
			if(ySpeed > 0f) {
				ySpeed = 0f;
			}
		}


		//Check Slope
		float _angle = 0f;
		if(transform.rotation.eulerAngles.x > 270f){
			_angle = 360f - transform.rotation.eulerAngles.x ;
		}

		if(zSpeed > 0.3f){
			velocity = new Vector3(-xSpeed, -ySpeed, zSpeed - _angle/25f);
		} else {
			velocity = new Vector3(-xSpeed, -ySpeed, zSpeed);
		}

		//Dont Change
		//float _xMove = Input.GetAxis("Horizontal");
		// float _zMove = Input.GetAxis("Vertical");

		// Vector3 _moveHorizontal = transform.right * _xMove;
		// Vector3 _moveVertical = transform.forward * _zMove;

		//velocity = (_moveHorizontal + _moveVertical).normalized * speed;



		//Turn Body
		if(leftTurn && yTurn > -1f){
			yTurn -= acceleration;
		} else if (!leftTurn && yTurn < 0f){
			yTurn += drag;
			if(yTurn > 0f){
				yTurn = 0f;
			}
		} 
		if(rightTurn && yTurn < 1f){
			yTurn += acceleration;
		} else if (!rightTurn && yTurn > 0f){
			yTurn -= drag;
			if(yTurn < 0f){
				yTurn = 0f;
			}
		}
	   	bodyRotation = new Vector3(0f, yTurn, 0f);

		//Animation
		if(playerAnimator && type != "pilot" && type != "gunner"){

			if(velocity!= Vector3.zero | bodyRotation !=Vector3.zero ){
				playerAnimator.SetBool("Forward", true);
			} else {
				playerAnimator.SetBool("Forward", false);
			}
		}

		//float _cameraMoveThreshold = 0.3f;

		//Turn Holder
		holderRotation = new Vector3(0f, yRot, 0f) * cameraSpeed;
		turretRotation = new Vector3(xRot, 0f, 0f);


		//Perform Movement
		if(velocity != Vector3.zero){
			velocity = transform.TransformDirection(velocity);
			rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
		}
		//Perform Rotation
		if(headFollow){
			FollowHead();
		}
		rb.transform.Rotate(bodyRotation);
		holder.transform.Rotate(holderRotation);
		turrent.transform.Rotate(-turretRotation);
	}

	void FollowHead() {

		holder.transform.Rotate(-bodyRotation);
		if(holder.eulerAngles.y > 270 && transform.eulerAngles.y < 90 ) {
			if( yTurn > -1f)
			yTurn -= acceleration;
		} else if (transform.eulerAngles.y > 270 && holder.eulerAngles.y < 90){
			if( yTurn < 1f)
			yTurn += acceleration;
		} else if (transform.eulerAngles.y > holder.eulerAngles.y + 10) {
			if( yTurn > -1f)
		   	yTurn -= acceleration;
		} else if (transform.eulerAngles.y < holder.eulerAngles.y - 10 && yTurn < 1f){
			if( yTurn < 1f)
		  	yTurn += acceleration;
		}
	}


	


	void command(){
		

		//Movement
		if (Input.GetKey(KeyCode.W)) {StartCoroutine (forwardOn());}   else {StartCoroutine (forwardOff());}
		if (Input.GetKey(KeyCode.S)) {StartCoroutine (backwardOn());}  else {StartCoroutine (backwardOff());}
		if (Input.GetKey(KeyCode.A)) {StartCoroutine (leftwardOn());}  else {StartCoroutine (leftwardOff());}
		if (Input.GetKey(KeyCode.D)) {StartCoroutine (rightwardOn());} else {StartCoroutine (rightwardOff());}
		if (Input.GetKey(KeyCode.Q)) {StartCoroutine (leftTurnOn());}  else {StartCoroutine (leftTurnOff());}
		if (Input.GetKey(KeyCode.E)) {StartCoroutine (rightTurnOn());} else {StartCoroutine (rightTurnOff());}

		if(type == "pilot"){
			
			if (Input.GetKey(KeyCode.LeftControl)) {StartCoroutine (upOn());}  else {StartCoroutine (upOff());}
			if (Input.GetKey(KeyCode.Space))  {StartCoroutine (downOn());} else {StartCoroutine (downOff());}
		}

		//Camera

		if(type != "pilot" && type != "engineer" ){
			if (Input.GetKeyUp(KeyCode.F)) {StartCoroutine (headFollowSwitch());}
			//Turn holder
			yRot = Input.GetAxis("Mouse X");
			// if(yRot > 0.2f ){StartCoroutine(holderLeftTurnOn());StartCoroutine(holderRightTurnOff());}
			// else if(yRot < -0.2f ){StartCoroutine(holderRightTurnOn());StartCoroutine(holderLeftTurnOff());} 
			// else {StartCoroutine(holderLeftTurnOff());StartCoroutine(holderRightTurnOff());} 
			xRot = Input.GetAxis("Mouse Y");
			// if(xRot > 0.2f ){StartCoroutine(turretUpTurnOn());StartCoroutine(turretDownTurnOff());}
			// else if(xRot < -0.2f ){StartCoroutine(turretDownTurnOn());StartCoroutine(turretUpTurnOff());} 
			// else {StartCoroutine(turretUpTurnOff());StartCoroutine(turretDownTurnOff());} 
		}

		//Fight
		lastShoot += Time.deltaTime;
		if (Input.GetButton("Fire1")){
			StartCoroutine (ShootOn());
		} else {
			StartCoroutine (ShootOff());
		}

	}

	void action(){

		if(state == "normal"){
			movement();
			shoot();
		} 

	}

	void shoot(){
		float _attackSpeed;
		// if(GetComponent<PlayerInteraction>().onSensor == "bunker" ||GetComponent<PlayerInteraction>().onSensor == "pass" ){
		// 	_attackSpeed = 0.18f;
		// } else {
			_attackSpeed = attackSpeed;
		// }


		if(type == "standard" || type == "gunner"){
			if(shooting && lastShoot >= _attackSpeed / asBonus && ammo >= 1){
				lastShoot = 0;
				ammo --;
				_attackPower = Random.Range(attackPower-attackPower_offset, attackPower+attackPower_offset);
				playerShoot.CmdShoot(_attackPower, damageBonus);
				playerShoot.LocalShoot(_attackPower);
				GetComponent<Heat>().Shoot17();
			}
		} else if(type == "hero"){
			if(shooting && lastShoot >= _attackSpeed / asBonus && ammo42 >= 1){
				lastShoot = 0;
				ammo42 --;
				_attackPower = Random.Range(attackPower-attackPower_offset, attackPower+attackPower_offset);
				playerShoot.CmdShoot42(_attackPower, damageBonus);
				playerShoot.LocalShoot42(_attackPower);
				GetComponent<Heat>().Shoot42();
			}
		}
	}

	public void RefreshText(){
		if(ammoNumberText)
		ammoNumberText.text = "17mm [" + ammo.ToString() + "] - 42mm [" + ammo42.ToString() + "]";
		GC.gc.currentHPText.text = Mathf.RoundToInt(hp).ToString();
	}

	void Dead(){

		hp = 0;
		state = "dead";
		if(isLocalPlayer){
			RefreshText();
		}
		GC.gc.GetComponent<GameState>().CheckSurvivor();


	}


	void CheckCondition(){
		speed = Mathf.Round( zSpeed * 100) / 100f;
		_power = Mathf.Round( (speed * Random.Range(10f,20f)) * 100) / 100f;
		GC.gc.speedText.text = Mathf.Round(_attackPower).ToString();
		if(_power < 0) {
			_power *= -1; 
		}
		GC.gc.powerText.text = _power.ToString();
	}

	void checkState(){
		if(hp <= 0 && state != "dead") {
			Dead();
		}
	}

	//Movement
	bool shooting = false;
	bool forward = false;
	bool backward = false;
	bool leftward = false;
	bool rightward = false;
	bool leftTurn = false;
	bool rightTurn = false;
	public bool headFollow = true;
	bool up = false;
	bool down = false;

	//Camera 
	// bool turretUpTurn = false;
	// bool turretDownTurn = false;
	// bool holderLeftTurn = false;
	// bool holderRightTurn = false;

	IEnumerator ShootOn() {      yield return new WaitForSeconds(GC.gc.delay * 0.001f); shooting = true;}
	IEnumerator ShootOff() {     yield return new WaitForSeconds(GC.gc.delay * 0.001f); shooting = false;}
	IEnumerator forwardOn() {    yield return new WaitForSeconds(GC.gc.delay * 0.001f); forward = true;}
	IEnumerator forwardOff() {   yield return new WaitForSeconds(GC.gc.delay * 0.001f); forward = false;}
	IEnumerator backwardOn() {   yield return new WaitForSeconds(GC.gc.delay * 0.001f); backward = true;}
	IEnumerator backwardOff() {  yield return new WaitForSeconds(GC.gc.delay * 0.001f); backward = false;}
	IEnumerator leftwardOn() {   yield return new WaitForSeconds(GC.gc.delay * 0.001f); leftward = true;}
	IEnumerator leftwardOff() {  yield return new WaitForSeconds(GC.gc.delay * 0.001f); leftward = false;}
	IEnumerator rightwardOn() {  yield return new WaitForSeconds(GC.gc.delay * 0.001f); rightward = true;}
	IEnumerator rightwardOff() { yield return new WaitForSeconds(GC.gc.delay * 0.001f); rightward = false;}
	IEnumerator leftTurnOn() {   yield return new WaitForSeconds(GC.gc.delay * 0.001f); leftTurn = true;}
	IEnumerator leftTurnOff() {  yield return new WaitForSeconds(GC.gc.delay * 0.001f); leftTurn = false;}
	IEnumerator rightTurnOn() {  yield return new WaitForSeconds(GC.gc.delay * 0.001f); rightTurn = true;}
	IEnumerator rightTurnOff() { yield return new WaitForSeconds(GC.gc.delay * 0.001f); rightTurn = false;}

	// IEnumerator turretUpTurnOn() {  yield return new WaitForSeconds(GC.gc.delay * 0.00001f); turretUpTurn = true;}
	// IEnumerator turretUpTurnOff() { yield return new WaitForSeconds(GC.gc.delay * 0.00001f); turretUpTurn = false;}
	// IEnumerator turretDownTurnOn() {  yield return new WaitForSeconds(GC.gc.delay * 0.00001f); turretDownTurn = true;}
	// IEnumerator turretDownTurnOff() { yield return new WaitForSeconds(GC.gc.delay * 0.00001f); turretDownTurn = false;}
	// IEnumerator holderLeftTurnOn() {  yield return new WaitForSeconds(GC.gc.delay * 0.00001f); holderLeftTurn = true;}
	// IEnumerator holderLeftTurnOff() { yield return new WaitForSeconds(GC.gc.delay * 0.00001f); holderLeftTurn = false;}
	// IEnumerator holderRightTurnOn() {  yield return new WaitForSeconds(GC.gc.delay * 0.00001f); holderRightTurn = true;}
	// IEnumerator holderRightTurnOff() { yield return new WaitForSeconds(GC.gc.delay * 0.00001f); holderRightTurn = false;}

	IEnumerator headFollowSwitch() { yield return new WaitForSeconds(GC.gc.delay * 0.001f); headFollow = !headFollow;}
	IEnumerator upOn() {    yield return new WaitForSeconds(GC.gc.delay * 0.001f); up = true;}
	IEnumerator upOff() {   yield return new WaitForSeconds(GC.gc.delay * 0.001f); up = false;}
	IEnumerator downOn() {  yield return new WaitForSeconds(GC.gc.delay * 0.001f); down = true;}
	IEnumerator downOff() { yield return new WaitForSeconds(GC.gc.delay * 0.001f); down = false;}

}
