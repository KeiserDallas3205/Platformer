using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
	// Patrol boundaries
	public GameObject leftPoint;
	public GameObject rightPoint;
	
	// For accessing sprite velocity
	private Rigidbody2D rb;
	
	// Current position
	private Transform currentPoint;
	
	// Speed of enemy
	public float speed;
	
	
	
    // Start is called before the first frame update
    void Start()
    {
		// Grab rigid body compenent 
		rb = GetComponent<Rigidbody2D>();
		
		// The enemy patrols towards the right first
		currentPoint = rightPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {	
		Vector2 point = currentPoint.position - transform.position;
		
		// Sets directional movement
		if(currentPoint == rightPoint.transform){
			rb.velocity = new Vector2(speed,0);
		}else if(currentPoint == leftPoint.transform){
			rb.velocity = new Vector2(-speed,0);
		}
		
		// When reaching end of patrol zone 
		if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == rightPoint.transform){
			// Head back in opposite direction
			currentPoint = leftPoint.transform; 
		}
		if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == leftPoint.transform){
			// Head back in opposite direction
			currentPoint = rightPoint.transform; 
		}
    }
	
	// Makes the points more visible in the scene editor
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(leftPoint.transform.position,0.5f);
		Gizmos.DrawWireSphere(rightPoint.transform.position,0.5f);
		
	}
}
