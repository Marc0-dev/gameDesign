using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

public class Player : Entity
{
    private float damage;
    private float crit;
    private CharacterController controller;
    private Vector3 verticalVelocity;
    private bool grounded;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravity = -9.81f;
    private Vector3 inputVector;
    private GameObject mainCharacter;
    public void instantiate(){
        mainCharacter = GameObject.Find("mainCharacter");
        controller = mainCharacter.GetComponent<CharacterController>();
        controller.detectCollisions = true;
    }
    private void move(){
        grounded = controller.isGrounded;
        if(grounded && verticalVelocity.y < 0){
            verticalVelocity.y = 0f;
        }
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(inputVector*Time.deltaTime*playerSpeed);
        if(inputVector != Vector3.zero){
            mainCharacter.transform.forward = inputVector;
        }
        if(Input.GetButton("Jump") && grounded){
            verticalVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        verticalVelocity.y += gravity*Time.deltaTime;
        controller.Move(verticalVelocity*Time.deltaTime);
    }
    private void shoot(){}
    private void look(){
        
    }
    private void dodge(){}
    private void reload(){}
    private void interact(){}
    public void react(){
        move();
        look();
    }
}
