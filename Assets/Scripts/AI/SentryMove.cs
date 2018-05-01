using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryMove : MonoBehaviour {

	public List<Transform> trackPoints = new List<Transform>();
	public Transform path;
	public Transform body;
	public Vector3 destination;
	public int currentPoint;
	public float speed;
	public bool back;
	public float distance;
	//public Transform targetPoint;
	public int targetIndex;
	public SentryShoot ss;


	// Use this for initialization
	void Start () {
		ss = GetComponent<SentryShoot>();
		for (int i = 0; i < path.childCount; i++){	
			trackPoints.Add(path.GetChild(i));
		}
		transform.position = trackPoints[0].position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(GetComponent<SentryShoot>().blackOut == false){
			Move();
			TurnTo();
		}
	}

	void Move(){
		MoveToCloset();
		if(transform.position != trackPoints[currentPoint].position){
			moveToDestination();
		} else {
			if(!back && currentPoint < trackPoints.Count-1){
				if(targetIndex == 0){
					currentPoint ++; 
				} else if (currentPoint != targetIndex -1){
					currentPoint ++; 
				}
			} else if(back && currentPoint > 0){
				if(targetIndex == 0){
					currentPoint --; 
				} else if (currentPoint != targetIndex -1){
					currentPoint --; 
				}
			} else if (currentPoint ==  trackPoints.Count-1) {
				back = true;
			} else if (currentPoint == 0) {
				back = false;
			}
			if(targetIndex > 0){
				if (currentPoint > targetIndex -1) {
					back = true;
				} else if (currentPoint < targetIndex -1) {
					back = false;
				}
			}
		}
	}



	float CountDis(Transform _target){
		distance = Vector3.Distance(ss.target.position, _target.position );
		return distance;
	}

public float currestClosetDis;
public float _dis ;
	void MoveToCloset(){
		if(ss.target){
			currestClosetDis = ss.range;
			for (int i = 0; i < trackPoints.Count; i++)
			{
				
				_dis = CountDis(trackPoints[i]);
				if(_dis < currestClosetDis){
					//currentPoint = i;
					targetIndex = i + 1;
					//targetPoint = trackPoints[i];
					currestClosetDis = _dis;
				}
			}
		} else {
			targetIndex = 0;
		}
		//destination = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime );
	}

	void moveToDestination(){
		destination = Vector3.MoveTowards(transform.position, trackPoints[currentPoint].position, speed * Time.deltaTime );

		GetComponent<Rigidbody>().MovePosition(destination);		
	}

	float yTurn;

	void TurnTo(){
		

		if(trackPoints[currentPoint].eulerAngles.y > 270 && transform.eulerAngles.y < 90 ) {
			if( yTurn > -speed)
			yTurn -= speed;
		} else if (transform.eulerAngles.y > 270 && trackPoints[currentPoint].eulerAngles.y < 90){
			if( yTurn < speed)
			yTurn += speed;
		} else if (transform.eulerAngles.y > trackPoints[currentPoint].eulerAngles.y ) {
			if( yTurn > -speed)
		   	yTurn -= speed;
		} else if (transform.eulerAngles.y < trackPoints[currentPoint].eulerAngles.y  && yTurn < 1f){
			if( yTurn < speed)
		  	yTurn += speed;
		}
	   	Vector3 bodyRotation = new Vector3(0f, yTurn, 0f);

		transform.Rotate(bodyRotation);
	}
}
