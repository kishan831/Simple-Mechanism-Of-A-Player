using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variable
    [SerializeField] private float movespeed;
    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isgrounded;
    [SerializeField] private float groundcheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumphight;



    //references

    private CharacterController controller;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
          StartCoroutine(Attack());
        }
    }

    private void Move()
    {
        isgrounded = Physics.CheckSphere(transform.position,groundcheckDistance, groundMask);
        
        if(isgrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        
        moveDirection = new Vector3(0, 0,moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if(isgrounded)
      {

        
        
        if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
        
          Walk();
        }
        else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
           //run
           Run();

        }
        else if(moveDirection == Vector3.zero)
        {
          //idle
          Idle();
        }
          moveDirection *= movespeed;

          if(Input.GetKey(KeyCode.Space))
          {
              jump();
          }


      }
     

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
       anim.SetFloat("Speed", 0,0.1f,Time.deltaTime);
    }

    private void Walk()
    {
       movespeed = walkspeed;
       anim.SetFloat("Speed",.5f,0.1f,Time.deltaTime);
    }


    private void Run()
    {
       movespeed = runspeed;
       anim.SetFloat("Speed",1,0.1f,Time.deltaTime);
    }

    private void jump()
    {
      velocity.y = Mathf.Sqrt(jumphight * -2 * gravity);
    }
    
    private IEnumerator Attack()
    {
      anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"),1);
      anim.SetTrigger("Attack");

     yield return new WaitForSeconds(0.9f);
      anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"),0);

    }

}
