using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  private CharacterController2D charCon;
  private float move;
  private bool jump = false;
  // Start is called before the first frame update
  void Start() {
    charCon = GetComponent<CharacterController2D>();
  }

  // Update is called once per frame
  void Update() {
    move = 0;
    move += (Input.GetKey(KeyCode.A) ? -1 : 0);
    move += (Input.GetKey(KeyCode.D) ? +1 : 0);
    jump = (Input.GetKey(KeyCode.Space) ? true : false);
  }
  private void FixedUpdate() {
    charCon.Move(move, false, jump);
  }
}
