using UnityEngine;
using System.Collections;

public class PatrolState : IState 
	
{
	public Collider contact;
	private readonly Guard enemy;
	private int nextWayPoint = 0;
	private bool reverse = false;

	public bool once = false;

	Vector3 curDir;
	Vector3 newDir;
	Rigidbody rBody;
	float lastSqrMag = Mathf.Infinity;

	Transform waypoint_position;
	Transform guardPosition;
	float startTime;
	float journey_length;


	
	public PatrolState (Guard guard)
	{
		enemy = guard;
		rBody = enemy.GetComponent<Rigidbody> ();


	}
	
	public void UpdateState()
	{
		Patrol ();
	}
	
	public void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Scent")) {
			Debug.Log ("Enter scent");
			enemy.trailState.curTile = other.gameObject;

			ToTrailState ();
			
		}
	}

	public void ToGuardState()
	{
		//enemy.currentState = enemy.guardState;
	}

	public void ToPatrolState()
	{
		//Can't access.
	}
	
	public void ToAlertState()
	{
		//enemy.currentState = enemy.alertState;
	}
	
	public void ToChaseState()
	{
		enemy.currentState = enemy.chaseState;
	}

	public void ToDistractedState ()
	{
        GameObject manager = GameObject.Find("manager");
        enemy.distractedState.currentTurn = manager.GetComponent<GameManager>().turnsPassed;
		enemy.currentState = enemy.distractedState;
	}

	public void ToTrailState()
	{
		enemy.currentState = enemy.trailState;
	}
	private void Look()
	{

	}
	
	void Patrol()
	{	
		waypoint_position = enemy.wayPoints [nextWayPoint].gameObject.transform;
		if (once == false) {
			if (nextWayPoint == enemy.wayPoints.Length - 1) {
				reverse = true;
			}
			if (nextWayPoint == 0) {
				reverse = false; 
			}
			if (reverse) {
				nextWayPoint--;
				//	Debug.Log(nextWayPoint);
			} else {
				nextWayPoint++;
				//	Debug.Log(nextWayPoint);
			}

			//newDir = (waypoint_position.position - enemy.transform.position).normalized * 1.0f;
			once = true;
		}


		Vector3 calculations = enemy.transform.position - waypoint_position.transform.position;

		if (calculations.magnitude == 0f) {
			enemy.animation.Play ("idle");
		} else {
			
			enemy.animation.Play ("walking-dog");
		}


		enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, waypoint_position.position, 0.02f);
	}
}