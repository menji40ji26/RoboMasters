using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ComponentChange : NetworkBehaviour {

	public MeshFilter body;
	public MeshFilter holder;
	public MeshFilter turret;
	public MeshFilter wheel_fl;
	public MeshFilter wheel_fr;
	public MeshFilter wheel_bl;
	public MeshFilter wheel_br;
	public Transform leg_fl;
	public Transform leg_fr;
	public Transform leg_bl;
	public Transform leg_br;
	public Animator animator;
	public Camera playerCam;
	public Camera engineerCam;
	public Camera upperCam;
	public GameObject upper;
	public GameObject engineer;
	public GameObject hero;
	public GameObject cameraBlue;
	public GameObject cameraRed;
	public MeshRenderer monitorBlue;
	public MeshRenderer monitorRed;
	public GameObject upperCam_HighPoint;
	public GameObject upperCam_LowPoint;

	Player p;
	Camera pilotCam;
	public GameObject drone;
	public GameObject droneBody;


	public GameObject minimapDot_Red;
	public GameObject minimapDot_Blue;


	public void SetupComponent(string _type){
		p = GetComponent<Player>();
		animator = GetComponent<Animator>();
		if(_type == "standard"){
			ToStandard();
		} else if(_type == "hero"){
			ToHero();
		} else if(_type == "pilot"){
			ToDrone();
		} else if(_type == "gunner"){
			ToTurret();
		} else if(_type == "engineer"){
			ToEngineer();
		}
		SetupMinimapDot();
	}

	void SetupMinimapDot(){
		if(p.faction == "red"){
			minimapDot_Red.SetActive(true);
		} else if(p.faction == "blue"){
			minimapDot_Blue.SetActive(true);
		}
	}

	void FixedUpdate(){
		if(pilotCam){
			pilotCam.transform.LookAt(this.transform);
		}
		AttachToDrone();
	}

	void AttachToDrone(){
		if(p)
		if(p.type == "gunner"){
			if(p.faction == "red"){
				drone = GameObject.Find("pilotred");
			} else if(p.faction == "blue"){
				drone = GameObject.Find("pilotblue");
			}
			if(drone)
			this.transform.position = drone.transform.GetChild(0).position;
		}
	}

	void ToStandard(){
		animator.SetTrigger("Standard");
		body.mesh = DB.db.standard_body;
		holder.mesh = DB.db.standard_holder;
		turret.mesh = DB.db.standard_turret;
	}


	void ToEngineer(){
		engineer.SetActive(true);
		animator.SetTrigger("Engineer");
		playerCam.enabled = false;
		engineerCam.enabled = true;
		p.headFollow = false;
		body.mesh = DB.db.engineer_body;
		holder.mesh = null;
		turret.mesh = null;
		wheel_fl.transform.parent.SetParent(leg_fl);
		wheel_fr.transform.parent.SetParent(leg_fr);
		wheel_bl.transform.parent.SetParent(leg_bl);
		wheel_br.transform.parent.SetParent(leg_br);
	}

	void ToHero(){
		hero.SetActive(true);
		upper.SetActive(true);
		if(p.faction == "blue"){
			cameraRed.SetActive(false);
			monitorRed.enabled = false;
			cameraBlue.SetActive(true);
			monitorBlue.enabled = true;
		} else if(p.faction == "red"){
			cameraBlue.SetActive(false);
			monitorBlue.enabled = false;
			cameraRed.SetActive(true);
			monitorRed.enabled = true;
		}
		GetComponent<SentryShoot>().faction = p.faction;
		animator.SetTrigger("Hero");
		body.mesh = DB.db.hero_body;
		holder.mesh = DB.db.hero_holder;
		turret.mesh = DB.db.hero_turret;
	}

	void ToDrone(){
		gameObject.layer = LayerMask.NameToLayer("Drone");
		droneBody.layer = LayerMask.NameToLayer("Drone");
		animator.SetTrigger("Drone");
		animator.SetBool("Spin", true);
		//GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		playerCam.enabled = false;
		p.GetComponent<Rigidbody>().useGravity = false;
		p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
		p.GetComponent<Rigidbody>().angularDrag = 1f;
		p.GetComponent<Rigidbody>().drag = 1f;
		p.headFollow = false;
		
		body.mesh = DB.db.drone_body;
		wheel_fl.mesh = DB.db.drone_wing;
		wheel_fr.mesh = DB.db.drone_wing;
		wheel_bl.mesh = DB.db.drone_wing;
		wheel_br.mesh = DB.db.drone_wing;
		holder.mesh = null;
		turret.mesh = null;
		if(p.isLocalPlayer){
			if(p.faction == "red"){
			GC.gc.pilotRedCam.enabled = true;
			pilotCam = GC.gc.pilotRedCam;
			} else if(p.faction == "blue"){

				GC.gc.pilotBlueCam.enabled = true;
				pilotCam = GC.gc.pilotBlueCam;
			}
		}
	}


	void ToTurret(){
		this.gameObject.layer = LayerMask.NameToLayer("Drone");
		GetComponent<Collider>().enabled = false;
		animator.SetTrigger("Gunner");
		GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
		p.GetComponent<Rigidbody>().angularDrag = 100f;
		p.GetComponent<Rigidbody>().drag = 100f;
		p.headFollow = false;
		body.mesh = null;
		holder.mesh = DB.db.drone_holder;
		turret.mesh = DB.db.drone_turret;
		wheel_fl.mesh = null;
		wheel_fr.mesh = null;
		wheel_bl.mesh = null;
		wheel_br.mesh = null;
	}
}
