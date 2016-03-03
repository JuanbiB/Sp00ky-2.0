using UnityEngine;
using System.Collections;

public class TrailState : IState 

{
	private Transform target;
	private int index;
	private bool start = true; 
	private readonly Guard enemy;

	public TrailState (Guard guard)
	{
		enemy = guard;
	}

	public void UpdateState()
	{
		//Look ();
		Follow ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Cover") {
			enemy.distractedState.start = Time.time;
			ToDistractedState ();
		}
		//Debug.Log ("Test");
		index++;
	}

	public void ToGuardState()
	{
		//Can't access - Have to go through alert.
	}


	public void ToPatrolState()
	{
		enemy.currentState = enemy.patrolState;
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
		enemy.currentState = enemy.distractedState;
	}

	public void ToTrailState ()
	{

	}

	private void Look()
	{
		//TODO Max's code here.
	}

	private void Follow()
	{
		GameObject golist = GameObject.Find ("trailColliders");
		//Debug.Log (golist);
		if (start) {
			target = enemy.patrolState.contact.transform;
			index =  int.Parse(enemy.patrolState.contact.name) + 1;
			start = false;
		} else {
			//Debug.Log (index.ToString ());
			target = golist.transform.Find (index.ToString ()).gameObject.transform ;
		}
        
        // This section done so that the dog faces the right way during his chase, same as in patrol state.
        
        float target_position = target.transform.position.x;
        float enemy_position = enemy.transform.position.x;

        //enemy.gameObject.GetComponent<Animator>().Play("walking-side");

        if (target_position < enemy_position)
        {
            enemy.transform.eulerAngles = new Vector3(45, 0, 0);
        }
        else if (target_position > enemy_position)
        {
            enemy.transform.eulerAngles = new Vector3(-45, 180, 0);
        }
        

        float step = (enemy.speed + 1) * Time.deltaTime;
		enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,target.position,step);
	}
}