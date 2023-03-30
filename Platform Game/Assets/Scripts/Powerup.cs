using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public SpriteRenderer character;
    private Color col;
    private float activationTime;
    private bool invisible;


    void Start()
    {
        character = GetComponent<SpriteRenderer>();
        activationTime = 0;
        invisible = false;
        col = character.color;
    }

    // Update is called once per frame
    void Update()
    {
        activationTime += Time.deltaTime;
        if(invisible && activationTime >= 3)
        {
            invisible = false;
            col.a = 1;
            character.color = col;
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Invisible")
        {
            invisible = true;
            activationTime = 0;
            col.a = .2f;
            character.color = col;
        }
    }
}
