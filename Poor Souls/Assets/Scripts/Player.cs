using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
public class Player : Entity
{
    private float damage;
    private float crit;
    private CharacterController controller;
    private Vector3 verticalVelocity;
    private bool grounded;
    private float playerSpeed = 15f;
    private float jumpHeight = 5f;
    private float gravity = -25f;
    private Vector3 inputVector = Vector3.zero;
    private GameObject mainCharacter;
    public Camera playerCamera;
    private GameObject mouseRotation;
    private Animator playerAnimator;
    private static int bulletCap = 30;
    private int bulletCount = bulletCap;
    private float mouseSensitivity = 2.0f;
    private float horizontalRotation = 0;
    private float verticalRotation = 0;
    public void instantiate(){
        mainCharacter = GameObject.Find("mainCharacter");
        playerCamera = GameObject.Find("playerCamera").GetComponent<Camera>();
        mouseRotation = GameObject.Find("mouseRotation");
        controller = mainCharacter.GetComponent<CharacterController>();
        playerAnimator = mainCharacter.GetComponent<Animator>();
        controller.detectCollisions = true;

    }
    public void move(){
        horizontalRotation += mouseSensitivity * Input.GetAxis("Mouse X");
        verticalRotation += mouseSensitivity * Input.GetAxis("Mouse Y");
        mouseRotation.transform.rotation = Quaternion.Euler(-verticalRotation, horizontalRotation, 0);
        grounded = controller.isGrounded;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right  = transform.TransformDirection(Vector3.right);
        float speedX = Input.GetAxis("Vertical") * playerSpeed;
        float speedY = Input.GetAxis("Horizontal") * playerSpeed;
        inputVector = (forward * speedX) + (right * speedY);
        if(grounded && verticalVelocity.y < 0){
            verticalVelocity.y = 0f;
        }
        controller.Move(inputVector*Time.deltaTime);
        if(Input.GetButton("Jump") && grounded){
            verticalVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        verticalVelocity.y += gravity*Time.deltaTime;
        controller.Move(verticalVelocity*Time.deltaTime);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
    }
    bool hasBullets(){
        return bulletCount > 0;
    }
    private void shoot(){
        RaycastHit hitEntity;
        Ray ray;
        ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitEntity)) {
            Debug.Log("hit");
        }
    }
    private void decreaseBulletCount(){
        if(hasBullets()){
            bulletCount = Mathf.Clamp(bulletCount-1, 0, bulletCap);
            Debug.Log(bulletCount);
            shoot();
        }
    }
    private void ResetBulletCount(){
        bulletCount = bulletCap;
        Debug.Log(bulletCount);
    }
    public void handleGuns(){
        playerAnimator.SetBool("fire", false);
        if(Input.GetButton("Fire1")){
            playerAnimator.SetBool("fire", true);
        }
        else if(Input.GetKeyDown(KeyCode.R))
            playerAnimator.SetTrigger("reload");  
    }
    private void interact(){}
}
