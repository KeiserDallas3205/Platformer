using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

// States
public enum AIState {
	IDLE,
	PATROL_RIGHT,
	PATROL_LEFT,
	CAUGHT
}


public class EnemyState : MonoBehaviour
{
	
	// Speed
	public float speed = 5;
	
	// Speed increase for chase scenes 
	public float speedInc = 50;
	
	// Desired distance from player
	public float distFromPlayer;
	private float xTarget;
	
	// Distance enemy will roam/patrol
	public float patrolDist = 5;
	
	
	// Movement for enemy 
	private CharacterController2D charCon;
	
	// Player Reference 
	public Transform tfPlayer;
	
	// Holds the current state
	public AIState State {get; private set;}
	
	// Create a dictionary
	// Mapping AIState -> function
	private Dictionary<AIState, Action> stateStayMeths;
	private Dictionary<AIState, Action> stateEnterMeths;
	private Dictionary<AIState, Action> stateExitMeths;
	
	
	
# region Life Cycle Methods	
    // Start is called before the first frame update
    void Start()
    {
		stateStayMeths = new Dictionary<AIState, Action>() {
			{AIState.IDLE, StateStayIdle},
			{AIState.PATROL_RIGHT, StateStayPatrolRight},
			{AIState.PATROL_LEFT, StateStayPatrolLeft},
			{AIState.CAUGHT, StateStayCaught},
		};
		
		stateEnterMeths = new Dictionary<AIState, Action>() {
			{AIState.IDLE, StateEnterIdle},
			{AIState.PATROL_RIGHT, StateEnterPatrolRight},
			{AIState.PATROL_LEFT, StateEnterPatrolLeft},
			{AIState.CAUGHT, StateEnterCaught},
		};
		
		stateExitMeths = new Dictionary<AIState, Action>() {
			{AIState.IDLE, StateExitIdle},
			{AIState.PATROL_RIGHT, StateExitPatrolRight},
			{AIState.PATROL_LEFT, StateExitPatrolLeft},
			{AIState.CAUGHT, StateExitCaught},
		};
		
		// Set the first state 
		State = AIState.IDLE;
		
		// Access the character movements
		charCon = GetComponent<CharacterController2D>();
		
        
    }

	private void FixedUpdate(){
		stateStayMeths[State].Invoke();
	
	}
	
	private void OnTriggerEnter2D(Collider2D collision){
		// If collide w/ player
		if(collision.CompareTag("Player")){
			if(State == AIState.PATROL_LEFT || State == AIState.PATROL_RIGHT) {
				ChangeState(AIState.CAUGHT);
			}
		}
	}
	
	private void OnTriggerExit2D(Collider2D collision){
		if(collision.CompareTag("Player")){
			if(State == AIState.CAUGHT){
				ChangeState(AIState.PATROL_RIGHT);
			}
		}
	}
	
# endregion 

# region State Enter Methods
	private void StateEnterIdle(){
	
	}
	private void StateEnterPatrolLeft(){
		xTarget = transform.position.x - patrolDist;
	}
	
	private void StateEnterPatrolRight(){
		xTarget = transform.position.x + patrolDist;
	}
	
	private void StateEnterCaught(){
		speed += speedInc;
	}

# endregion

# region State Stay Methods
	private void StateStayIdle(){
		
		// If on the ground, patrol right
		if(charCon.IsPlayerOnGround()){
			ChangeState(AIState.PATROL_RIGHT);
		}
	}
	
	private void StateStayPatrolLeft(){
		if(transform.position.x <= xTarget){
			ChangeState(AIState.PATROL_RIGHT);
		}
		else{
			charCon.Move(-speed * Time.fixedDeltaTime,false);
		}
	}
	
	private void StateStayPatrolRight(){
		if(transform.position.x >= xTarget){
			ChangeState(AIState.PATROL_LEFT);
		}
		else{
			charCon.Move(speed * Time.fixedDeltaTime,false);
		}
	}
	
	private void StateStayCaught(){
		float xPlayerPos = tfPlayer.position.x;
		float xMyPos = transform.position.x;
		float yPlayerPos = tfPlayer.position.y;
		float yMyPos = transform.position.y;
		float dir = (xPlayerPos - xMyPos) < 0 ? -1 : 1;
		charCon.Move(dir * speed * Time.fixedDeltaTime,false);
		SceneManager.LoadScene(2);
		
	}

# endregion


# region State Exit Methods
	private void StateExitIdle(){
	
	}
	private void StateExitPatrolLeft(){
		xTarget = transform.position.x - patrolDist;
	}
	
	private void StateExitPatrolRight(){
		xTarget = transform.position.x + patrolDist;
	}
	
	private void StateExitCaught(){
		speed -= speedInc;
	}

# endregion

# region Helper Methods
	private void ChangeState(AIState newState) {
		if(State != newState){
			stateExitMeths[State].Invoke();
			State = newState;
			stateEnterMeths[State].Invoke();
		}
	}
	


# endregion
}
