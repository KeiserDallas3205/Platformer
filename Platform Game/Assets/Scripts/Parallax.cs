using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	public float scale;
	public Transform tfPlayer;
	public float origXPos;
    // Start is called before the first frame update
    void Start()
    {
		origXPos = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
		// Move the background in respect to the player
        transform.position = new Vector3(origXPos-(tfPlayer.position.x * scale), transform.position.y, transform.position.z);
    }
}
