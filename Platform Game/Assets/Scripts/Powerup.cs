using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public SpriteRenderer face;
    public SpriteRenderer body;
    public SpriteRenderer leftarm;
    public SpriteRenderer rightarm;
    public SpriteRenderer leftleg;
    public SpriteRenderer rightleg;
    private Color color;
    private float activationTime;
    private bool invisible;


    void Start()
    {
        activationTime = 0;
        invisible = false;
        color = body.color;
    }

    // Update is called once per frame
    void Update()
    {
        activationTime += Time.deltaTime;
        if(invisible && activationTime >= 10)
        {
            invisible = false;
            color.a = 1;
            face.color = color;
            body.color = color;
            leftarm.color = color;
            rightarm.color = color;
            leftleg.color = color;
            rightleg.color = color;
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Invisible")
        {
            invisible = true;
            activationTime = 0;
            color.a = .2f;
            face.color = color;
            body.color = color;
            leftarm.color = color;
            rightarm.color = color;
            leftleg.color = color;
            rightleg.color = color;
        }
    }
}
