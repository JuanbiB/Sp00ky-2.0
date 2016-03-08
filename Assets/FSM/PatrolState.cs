using UnityEngine;
using System.Collections;

public class PatrolState : IState 
	
{
	public Collider contact;
	private readonly Guard enemy;
	private int nextWayPoint;
	private bool reverse = false;
	
	public PatrolState (Guard guard)
	{
		enemy = guard;
	}
	
	public void UpdateState()
	{
	//	Look ();
		Patrol ();
	}
	
	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Scent"))
			contact = other;
			ToTrailState ();
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
		
		GameObject player = GameObject.FindWithTag("Player");
		Vector3 toPlayer = enemy.transform.position - player.transform.position;
		if (toPlayer.magnitude <= enemy.sightClose) {
			//Debug.Log ("Close");
			//Debug.Log (Vector3.Angle (toPlayer, -enemy.transform.forward));
			if (Mathf.Abs (Vector3.Angle (toPlayer, -enemy.transform.forward)) <= enemy.sightNarrow * .5f) {
				Debug.Log ("in sight");
				Debug.Log (Physics.Linecast (enemy.transform.position, player.transform.position));
				if (Physics.Linecast (enemy.transform.position, player.transform.position)) {
				//	ToChaseState ();
				}	
			}
		}
//		if (toPlayer.magnitude <= far) {
//			if (Mathf.Abs (Vector3.Angle (toPlayer, -enemy.transform.forward)) <= wide * .5f) {
//				//ToAlertState ();
//			}
//		}
	}
	
	void Patrol()
	{
		float step = enemy.speed * Time.deltaTime;
		enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,enemy.wayPoints[nextWayPoint].position,step);


        // This section done so that the dog faces the right way during his animation.
        float waypoint_math = enemy.wayPoints[nextWayPoint].position.x;
        float player_math = enemy.transform.position.x;

        enemy.gameObject.GetComponent<Animator>().Play("walking-dog");

        if (waypoint_math < player_math)
        {
            enemy.transform.eulerAngles = new Vector3(45, 0, 0);
        }
        else if (waypoint_math > player_math)
        {
            enemy.transform.eulerAngles = new Vector3(-45, 180, 0);
        }
     

		//Have we reached our next point?
		if (enemy.transform.position == enemy.wayPoints [nextWayPoint].position) {
			//Are we at the last point?
			if(nextWayPoint + 1 == enemy.wayPoints.Length){
				reverse = true;
			}
			//Are we at the first?
			if(nextWayPoint == 0){
				reverse = false;
			}
			if(reverse){
				nextWayPoint -= 1;
			} else{
				nextWayPoint += 1;
			}
		}
	}
}