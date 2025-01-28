using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
public class Player : Entity
{
    [SerializeField] private float damage = 10;
    private CharacterController controller;
    private Vector3 verticalVelocity;
    private bool grounded;
    [SerializeField] private float playerSpeed = 10f;
    private float jumpHeight = 5f;
    private float gravity = -25f;
    private Vector3 inputVector = Vector3.zero;
    private float lookLimit = 45;
    public Camera playerCamera;
    private GameObject mouseRotation;
    private Animator playerAnimator;
    public GameObject leftBarrel;
    public GameObject rightBarrel;
    public GameObject bullet;
    private bool barrelTurn = true;
    private static int bulletCap = 30;
    private int bulletCount = bulletCap;
    private float mouseSensitivity = 2.0f;
    private float verticalRotation = 0;
    PlayerInteract personalSpace;
    public AudioSource leftBarrelSound;
    public AudioSource rightBarrelSound;
    public AudioSource hitSound;
    void Start(){
        playerCamera = GameObject.Find("playerCamera").GetComponent<Camera>();
        mouseRotation = GameObject.Find("mouseRotation");
        leftBarrel = GameObject.Find("leftBarrel");
        rightBarrel = GameObject.Find("rightBarrel");
        leftBarrelSound = leftBarrel.GetComponent<AudioSource>();
        rightBarrelSound = rightBarrel.GetComponent<AudioSource>();
        hitSound = this.gameObject.GetComponent<AudioSource>();
        controller = this.gameObject.GetComponent<CharacterController>();
        playerAnimator = this.gameObject.GetComponent<Animator>();
        personalSpace = mouseRotation.GetComponent<PlayerInteract>();
        controller.detectCollisions = true;
    }
    public void Move(){
        verticalRotation += mouseSensitivity * Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -lookLimit, lookLimit);
        mouseRotation.transform.localRotation = Quaternion.Euler(-verticalRotation, 0, 0);
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
        GameObject firedBullet = null;
        if(barrelTurn == true){
            leftBarrelSound.Play(0);
            firedBullet = Instantiate(bullet, leftBarrel.transform.position, leftBarrel.transform.rotation);
            barrelTurn = false;
        }
        else if(barrelTurn == false){
            rightBarrelSound.Play(0);
            firedBullet = Instantiate(bullet, rightBarrel.transform.position, rightBarrel.transform.rotation);
            barrelTurn = true;
        }
        if (Physics.Raycast(ray, out hitEntity)) {
            firedBullet.GetComponent<Bullet>().Move(hitEntity.point, hitEntity.collider, damage);
        }
        else{
            firedBullet.GetComponent<Bullet>().Move(damage);
        }
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
        else if(Input.GetKeyDown(KeyCode.R))
            playerAnimator.SetTrigger("reload");
        else if(Input.GetKeyDown(KeyCode.E))
            personalSpace.Interact();  
    }
    public override void TakeDamage(float damageNumber)
    {
        base.TakeDamage(damageNumber);
        hitSound.Play(0);
    }
    public override void Die()
    {
        SceneManager.LoadScene(1);
    }
    public void SetDamage(float damage_){damage = damage_;}
    public Vector3 GetPosition(){return this.gameObject.transform.position;}
    public void SetPosition(Vector3 position){this.gameObject.transform.position = position;}
    public float GetDamage(){return damage;}
    public void SetPlayerSpeed(float playerSpeed_){playerSpeed = playerSpeed_;}
    public float GetPlayerSpeed(){return playerSpeed;}
    public void SetHealth(float health_){health = health_;}
    public float GetHealth(){return health;}
    public float GetBulletCount(){return bulletCount;}
    public float GetBulletCap(){return bulletCap;}
}
