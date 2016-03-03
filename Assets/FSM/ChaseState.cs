using UnityEngine;
using System.Collections;

public class ChaseState : IState 
	
{
	private readonly Guard enemy;
		
	public ChaseState (Guard guard)
	{
		enemy = guard;
	}
	
	public void UpdateState()
	{
		Look ();
		Chase ();
	}
	
	public void OnTriggerEnter (Collider other)
	{

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
		//Can't access.
	}

	public void ToDistractedState ()
	{

	}

	public void ToTrailState()
	{

	}

	private void Look()
	{
		//TODO Max's code here.
	}
	
	private void Chase()
	{
        GameObject player = GameObject.FindWithTag("Player");
       
        float step = enemy.speed * Time.deltaTime;
		
		enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,player.transform.position,step);

	}
}