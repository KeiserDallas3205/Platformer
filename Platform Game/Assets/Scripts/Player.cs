using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour {
  private CharacterController2D charCon;
  private float move;
  public Animator animator;
  // Start is called before the first frame update
  void Start() {
    charCon = GetComponent<CharacterController2D>();
  }

  // Update is called once per frame
  void Update() {
    move = 0;
    move += (Input.GetKey(KeyCode.A) ? -1 : 0);
    move += (Input.GetKey(KeyCode.D) ? +1 : 0);
  }
  private void FixedUpdate() {
    charCon.Move(move, false);
    if (charCon.IsPlayerOnGround()) {
      animator.SetTrigger("Grounded");
    }
    animator.SetFloat("Idle Run", Mathf.Abs(move));
  }
}
