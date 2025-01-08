using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
public class Player : Entity
{
    private float damage = 10;
    private CharacterController controller;
    private Vector3 verticalVelocity;
    private bool grounded;
    private float playerSpeed = 15f;
    private float jumpHeight = 5f;
    private float gravity = -25f;
    private Vector3 inputVector = Vector3.zero;
    private float lookLimit = 45;
    private GameObject mainCharacter;
    public Camera playerCamera;
    private GameObject mouseRotation;
    private Animator playerAnimator;
    public GameObject leftBarrel;
    public GameObject rightBarrel;
    public Bullet bullet;
    private bool barrelTurn = true;
    private static int bulletCap = 30;
    private int bulletCount = bulletCap;
    private float mouseSensitivity = 2.0f;
    private float horizontalRotation = 0;
    private float verticalRotation = 0;
    private bool special = true;
    PlayerInteract personalSpace;
    public AudioSource leftBarrelSound;
    public AudioSource rightBarrelSound;
    public void Instantiate(){
        mainCharacter = GameObject.Find("mainCharacter");
        playerCamera = GameObject.Find("playerCamera").GetComponent<Camera>();
        mouseRotation = GameObject.Find("mouseRotation");
        leftBarrel = GameObject.Find("leftBarrel");
        rightBarrel = GameObject.Find("rightBarrel");
        leftBarrelSound = leftBarrel.GetComponent<AudioSource>();
        rightBarrelSound = rightBarrel.GetComponent<AudioSource>();
        controller = mainCharacter.GetComponent<CharacterController>();
        playerAnimator = mainCharacter.GetComponent<Animator>();
        personalSpace = mouseRotation.GetComponent<PlayerInteract>();
        controller.detectCollisions = true;
    }
    public void Move(){
        horizontalRotation += mouseSensitivity * Input.GetAxis("Mouse X");
        verticalRotation += mouseSensitivity * Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -lookLimit, lookLimit);
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
    private void Shoot(){
        RaycastHit hitEntity;
        Ray ray;
        Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2);
        ray = playerCamera.ScreenPointToRay(screenCenter);
        Bullet firedBullet = null;
        if(barrelTurn == true){
            firedBullet = Instantiate(bullet, leftBarrel.transform.position, leftBarrel.transform.rotation);
            leftBarrelSound.Play(0);
            barrelTurn = false;
        }
        else if(barrelTurn == false){
            firedBullet = Instantiate(bullet, rightBarrel.transform.position, rightBarrel.transform.rotation);
            rightBarrelSound.Play(0);
            barrelTurn = true;
        }
        if (Physics.Raycast(ray, out hitEntity)) {
            firedBullet.Move(hitEntity.point, hitEntity.collider, damage);
        }
        else{
            firedBullet.Move(damage);
        }
    }
    private void ShootSpecial(){
        RaycastHit hitEntity;
        Ray ray;
        Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2);
        ray = playerCamera.ScreenPointToRay(screenCenter);
        Bullet firedBulletLeft = null;
        Bullet firedBulletRight = null;
        Vector3 specialScale = new Vector3(20, 20, 20);
        float specialDamage = damage * 1.5f;
        firedBulletLeft = Instantiate(bullet, leftBarrel.transform.position, leftBarrel.transform.rotation);
        firedBulletRight = Instantiate(bullet, rightBarrel.transform.position, rightBarrel.transform.rotation);
        firedBulletLeft.transform.localScale = specialScale;
        firedBulletRight.transform.localScale = specialScale;
        if (Physics.Raycast(ray, out hitEntity)) {
            firedBulletLeft.Move(hitEntity.point, hitEntity.collider, specialDamage);
            firedBulletRight.Move(hitEntity.point, hitEntity.collider, specialDamage);
        }
        else{
            firedBulletLeft.Move(damage);
            firedBulletRight.Move(damage);
        }
        special = false;
        StartCoroutine(SpecialCooldown());
    }
    IEnumerator SpecialCooldown(){
        yield return new WaitForSeconds(60);
        special = true;
    }
    IEnumerator ChangeAmmoAlphaValue(){
        Renderer ammoRendererLeft = GameObject.Find("leftSphere").GetComponent<Renderer>();
        Renderer ammoRendererRight = GameObject.Find("rightSphere").GetComponent<Renderer>();
        Color ammoColor = ammoRendererLeft.material.color;
        if(bulletCount == bulletCap){
            ammoColor.a = 1f;
        }
        else{
            ammoColor.a = Mathf.Clamp(ammoColor.a-(1f/bulletCap), 0f, 1f);
        }
        ammoRendererLeft.material.color = ammoColor;
        ammoRendererRight.material.color = ammoColor;
        yield return null;
    }
    private void CheckSpecial(){
        if(special == true){
            Debug.Log("specialInbound");
            ShootSpecial();
        }
    }
    private void DecreaseBulletCount(){
        if(bulletCount > 0){
            bulletCount = Mathf.Clamp(bulletCount-1, 0, bulletCap);
            Shoot();
            StartCoroutine(ChangeAmmoAlphaValue());
        }
    }
    private void ResetBulletCount(){
        bulletCount = bulletCap;
        Debug.Log(bulletCount);
        StartCoroutine(ChangeAmmoAlphaValue());
    }
    public void HandleGuns(){
        playerAnimator.SetBool("fire", false);
        if(Input.GetButton("Fire1")){
            playerAnimator.SetBool("fire", true);
        }
        else if(Input.GetButton("Fire2")){
            playerAnimator.SetTrigger("special");
        }
        else if(Input.GetKeyDown(KeyCode.R))
            playerAnimator.SetTrigger("reload");
        else if(Input.GetKeyDown(KeyCode.E))
            personalSpace.Interact();  
    }
    public void SetDamage(float damage_){damage = damage_;}
    public float GetDamage(){return damage;}
    public void SetPlayerSpeed(float playerSpeed_){playerSpeed = playerSpeed_;}
    public float GetPlayerSpeed(){return playerSpeed;}
    public void SetHealth(float health_){health = health_;}
    public float GetHealth(){return health;}
}
