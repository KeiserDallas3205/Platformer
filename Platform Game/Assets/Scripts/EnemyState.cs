using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// States
public enum AIState {
	IDLE,
	RUN_AWAY,
	PATROL_RIGHT,
	PATROL_LEFT,
	CHASE
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
			{AIState.RUN_AWAY, StateStayRunAway},
			{AIState.PATROL_RIGHT, StateStayPatrolRight},
			{AIState.PATROL_LEFT, StateStayPatrolLeft},
			{AIState.CHASE, StateStayChase},
		};
		
		stateEnterMeths = new Dictionary<AIState, Action>() {
			{AIState.IDLE, StateEnterIdle},
			{AIState.RUN_AWAY, StateEnterRunAway},
			{AIState.PATROL_RIGHT, StateEnterPatrolRight},
			{AIState.PATROL_LEFT, StateEnterPatrolLeft},
			{AIState.CHASE, StateEnterChase},
		};
		
		stateExitMeths = new Dictionary<AIState, Action>() {
			{AIState.IDLE, StateExitIdle},
			{AIState.RUN_AWAY, StateExitRunAway},
			{AIState.PATROL_RIGHT, StateExitPatrolRight},
			{AIState.PATROL_LEFT, StateExitPatrolLeft},
			{AIState.CHASE, StateExitChase},
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
				ChangeState(AIState.CHASE);
			}
		}
	}
	
	private void OnTriggerExit2D(Collider2D collision){
		if(collision.CompareTag("Player")){
			if(State == AIState.CHASE){
				ChangeState(AIState.PATROL_RIGHT);
			}
		}
	}
	
# endregion 

# region State Enter Methods
	private void StateEnterIdle(){
	
	}
	private void StateEnterRunAway(){
		
	}
	
	private void StateEnterPatrolLeft(){
		xTarget = transform.position.x - patrolDist;
	}
	
	private void StateEnterPatrolRight(){
		xTarget = transform.position.x + patrolDist;
	}
	
	private void StateEnterChase(){
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
	private void StateStayRunAway(){
		if(Mathf.Abs(transform.position.x - tfPlayer.position.x) > distFromPlayer) {
			ChangeState(AIState.PATROL_RIGHT);
		}
		else{
		charCon.Move(speed * Time.fixedDeltaTime,false);
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
	
	private void StateStayChase(){
		float xPlayerPos = tfPlayer.position.x;
		float xMyPos = transform.position.x;
		float dir = (xPlayerPos - xMyPos) < 0 ? -1 : 1;
		charCon.Move(dir * speed * Time.fixedDeltaTime,false);
	}

# endregion


# region State Exit Methods
	private void StateExitIdle(){
	
	}
	private void StateExitRunAway(){
		
	}
	
	private void StateExitPatrolLeft(){
		xTarget = transform.position.x - patrolDist;
	}
	
	private void StateExitPatrolRight(){
		xTarget = transform.position.x + patrolDist;
	}
	
	private void StateExitChase(){
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
