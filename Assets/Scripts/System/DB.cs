using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour {

	static public DB db;
	public Mesh standard_body;
	public Mesh standard_holder;
	public Mesh standard_turret;
	public Mesh hero_body;
	public Mesh hero_holder;
	public Mesh hero_turret;
	public Mesh drone_body;
	public Mesh drone_holder;
	public Mesh drone_turret;
	public Mesh drone_wing;
	public Mesh wheel;
	public Mesh engineer_body;
	public Mesh engineer_arms;
	public Mesh engineer_hands;
	public Mesh engineer_leg_lf;
	public Mesh engineer_leg_rf;
	public Mesh engineer_leg_lb;
	public Mesh engineer_leg_rb;


	// Use this for initialization
	void Start () {
		db = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
