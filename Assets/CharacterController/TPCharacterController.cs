using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPCharacterController : MonoBehaviour
{
    private CharterControllerInput playerActionsAsset;

    private InputAction move;

    private Rigidbody rb;

    [SerializeField]
    private float movementForce = 1.0f;

    [SerializeField]
    private float jumpForce = 5.0f;

    [SerializeField]
    private float maxSpeed = 5.0f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;


    [SerializeField]
    private bool coyoteTime = false;

    [SerializeField]
    private bool canCoyote = false;

    [SerializeField]
    float coyoteTimeDelay = .2f;

    [SerializeField]
    float fasterFallDelay = .2f;

    [SerializeField]
    LayerMask attackable;

    [SerializeField]
    int attackStrentgh = 5;

    [SerializeField]
    float attackRadius = 5.0f;

   
    public int playerHealth = 5;

    public int colletablCount;

    public MainMenu menu;

    private void OnMenu(InputAction.CallbackContext ctx)
    {
        Debug.Log("OnMenu");
        menu.ToggleMenu();
    }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        playerActionsAsset = new CharterControllerInput();
        playerActionsAsset.Character.Menu.started += OnMenu;
    }

    private void OnEnable()
    {
        playerActionsAsset.Character.Jump.started += DoJump;
        playerActionsAsset.Character.Attack.started += DoAttack;
        move = playerActionsAsset.Character.Movement;
        playerActionsAsset.Character.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Character.Jump.started -= DoJump;
        playerActionsAsset.Character.Attack.started -= DoAttack;
        playerActionsAsset.Character.Disable();
    }

    private void FixedUpdate()
    {
        //check if grounded
        if (IsGrounded())
        {
            Physics.gravity = new Vector3(0, -9.8f, 0);
            canCoyote = true;
        }
        else
        {
            if (canCoyote)
            {
                //canCoyote = false;
                StartCoroutine(CoyoteTimer());
            }

            StartCoroutine(FallFaster());
        }

        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = rb.linearVelocity;
        horizontalVelocity.y = 0;

        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.linearVelocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.linearVelocity.y;
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = rb.linearVelocity;
        direction.y = 0f;


        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
       
        canCoyote = false;
        //coyoteTime = false;

        //Debug.Log("Jumped Pressed");
        if (IsGrounded())
        {
            coyoteTime = false;

            forceDirection += Vector3.up * jumpForce;
            StartCoroutine(FallFaster());
        }
        else if(!IsGrounded() && coyoteTime) 
        {
            coyoteTime = false;
            forceDirection += Vector3.up * jumpForce;
            StartCoroutine(FallFaster());
        }

        //coyoteTime = false;
        canCoyote = false;
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("attack");
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, attackable);

        foreach (Collider col in hits)
        {
            col.GetComponent<Enemy>().health-= attackStrentgh;
            col.GetComponent<Enemy>().CheckDie();
        }
        
    }

    //TODO: Add double jump logic here
    //Also remove ability to stick to walls
    //And fix camera problems
    private bool IsGrounded() 
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 2.0f)) 
        {
    
            return true;
           
           
        }
        else 
        {
            //Debug.Log("Not Grounded");
            //coyoteTime = true;
            return false;
        }
    }

    

    //This will have to be checked in some sort of update function to always keep track of if the player is on the ground or not
    IEnumerator CoyoteTimer()
    {
        if (canCoyote) 
        {
            coyoteTime = true;
        }
        
        yield return new WaitForSeconds(coyoteTimeDelay);
        //coyoteTime = false;
    }

    //TODO: Tweak this to actually work
    IEnumerator FallFaster() 
    {
        yield return new WaitForSeconds(fasterFallDelay);

        //rb.linearDamping = .25f;
        Physics.gravity = new Vector3(0, -9.8f * 3.0f, 0);

    }

}
