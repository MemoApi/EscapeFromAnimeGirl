using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] float moveSpeed = 8;
    [SerializeField] float turnSpeed = 1.4f;
    [SerializeField] float jumpForce = 2.5f;
    [SerializeField] float gravity = -18.53f;
    [SerializeField] float shootTimer = 0f;
    [SerializeField] float shootRate = .5f;
    [SerializeField] float fruitSpeed = 25f;
    
    Vector3 velocity;
    bool isGrounded;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform firePoint;

    [SerializeField] GameObject[] fruits;

    private void Awake() => controller = GetComponent<CharacterController>();
    private void Start() => Cursor.lockState = CursorLockMode.Locked;


    private void Update()
    {
        if (GameManager.instance.isDead) return;
        
        jump();
        move();
        rotate();
        shoot();

    }

    private void shoot()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(Time.time>= shootTimer)
            {
                GameObject fruit = Instantiate(fruits[UnityEngine.Random.Range(0, fruits.Length)], firePoint.position, Quaternion.identity);
                fruit.GetComponent<Rigidbody>().AddForce(firePoint.forward* fruitSpeed);
                shootTimer = Time.time + shootRate; 
            }
        }


    }
    private void rotate()
    {
        //We get the x value from the mouse and turn right or left according to this value.
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * turnSpeed);
    }
    private void move()
    {
        //we take vertical and horizontal values and move our character
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;

        controller.Move(moveDir * moveSpeed * Time.deltaTime);
    }
    void jump()
    {
        //we check if the character touches the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, .2f, groundLayer);

        if (isGrounded && velocity.y < 0)
        {   //we fix it so that the falling speed is not infinite
            velocity.y = gravity;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {   //If the character is on the ground and space is clicked, we make it jump
            //this is a motion formula i got from the internet
            velocity.y = Mathf.Sqrt(jumpForce * 2 * -gravity);
        }
        //we also provide gravity for the falling effect
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.PlayerDead();
        }
    }
}
