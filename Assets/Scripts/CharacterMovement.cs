using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Button DashButton;
    
    //get boundaries, come back to
    // private float horizrange = 70f;
    // private float vertirange = 40f;

    private Vector3 input;

    

    #if UNITY_ANDROID
        public Joystick leftjoystick;
        public Joystick rightjoystick;
    #endif

    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    public Transform moveDirectionTransform;


    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (trailRenderer != null) {
            trailRenderer.emitting = false;
        }
    }

    void FixedUpdate()
    {
       handleMovementInput();  
       handleRotationInput();
       HandleShootInput();
       
    }

    void handleMovementInput()
    {
        float horizontal;
        float vertical;

        #if UNITY_ANDROID
             horizontal = leftjoystick.Horizontal;
             vertical = leftjoystick.Vertical;
        #else
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        #endif
        

        //clamp players position to screen bounds, come back to
        // float clampedHorizpos = Mathf.Clamp(horizontal,-horizrange,horizrange);
        // float clampedVertipos = Mathf.Clamp(vertical,-vertirange,vertirange);
        // input = new Vector3(clampedHorizpos,0,clampedVertipos);

        input = new Vector3(horizontal,0,vertical);
        
        
        //movement
        rb.MovePosition(rb.position + input.normalized * movementSpeed * Time.deltaTime);
        //get dash direction
        moveDirectionTransform.position = transform.position + input.normalized * 2f;

    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Wall")) 
        {
            Debug.Log("wall");
            // Stop movement when colliding with a wall
            rb.velocity = Vector3.zero;
            // Stop rotation when colliding with a wall
            rb.angularVelocity = Vector3.zero;
            // Reset dash if colliding while dashing
            if (isDashing) 
            {
                StopCoroutine(DoDash());
                isDashing = false;
                if (trailRenderer != null) 
                {
                    trailRenderer.emitting = false;
                }
            }
        }
    }


    void handleRotationInput()
    {
        //mobile joystick input
        #if UNITY_ANDROID
            // Get right joystick thumbstick position
            Vector3 rightjoystickPos = new Vector3(rightjoystick.Horizontal, 0, rightjoystick.Vertical);
            //get left joystick thumbstick position
            Vector3 leftjoystickPos = new Vector3(leftjoystick.Horizontal, 0, leftjoystick.Vertical);

            // calculate player moving directon
            Vector3 movepos = transform.position + rightjoystickPos;

            // Calculate player aiming direction
            Vector3 aimPos = transform.position + rightjoystickPos;

            // Rotate object to look at the calculated position
            transform.LookAt(aimPos);

            // Get the child object's transform MUST BE DASH AS SECOND CHILD!!
            Transform childTransform = transform.GetChild(1);

            // Rotate the child object to look at the calculated position
            childTransform.LookAt(movepos);
        #else
            //for mouse input 
          
            RaycastHit hit;
            Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit))
            {
                transform.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));
            }
        #endif


    }


    public void Dash()
    {
        if (!isDashing && Time.time > lastDashTime + dashCooldown)
        {
        isDashing = true;
        lastDashTime = Time.time;
        StartCoroutine(DoDash());
        }
    }

    IEnumerator DoDash()
    {
        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }

        Vector3 startPosition = transform.position;
        Vector3 direction = -transform.GetChild(1).forward;
        Vector3 endPosition = transform.position + direction * dashDistance;
        float startTime = Time.time;
        float endTime = startTime + dashDuration;
        

        while (Time.time < endTime)
        {
            float timeRatio = (Time.time - startTime) / dashDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, timeRatio);
            yield return null;
        }

        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }

        isDashing = false;
    }

    void HandleShootInput()
    {
        #if UNITY_ANDROID
            if (rightjoystick.Horizontal != 0 || rightjoystick.Vertical != 0 )
            {
                shooting.Instance.Shoot();
            }
        #else
            if (Input.GetButton("Fire1"))
            {
                shooting.Instance.Shoot();
            }
        #endif
    }



}









