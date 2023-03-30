using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Win : MonoBehaviour
{
  
	
	private void OnTriggerEnter2D(Collider2D collision){
		// If collide w/ player
		if(collision.CompareTag("Player")){
			SceneManager.LoadScene(3);
		}
	}
}
