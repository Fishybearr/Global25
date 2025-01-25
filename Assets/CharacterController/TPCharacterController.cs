using System;
using System.Collections;
using UnityEditorInternal;
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
    private float fallDelay = .2f;

    private float rbDamping;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        playerActionsAsset = new CharterControllerInput();

        rbDamping = rb.linearDamping;


    }

    private void OnEnable()
    {
        playerActionsAsset.Character.Jump.started += DoJump;
        move = playerActionsAsset.Character.Movement;
        playerActionsAsset.Character.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Character.Jump.started -= DoJump;
        playerActionsAsset.Character.Disable();
    }

    private void FixedUpdate()
    {
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
            this.rb.rotation = Quaternion.LookRotation(direction,Vector3.up);
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
        Debug.Log("Jumped Pressed");
        if (IsGrounded()) 
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded() 
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 2.0f)) 
        {
           // Debug.Log("Grounded");
            rb.linearDamping = rbDamping;
            StartCoroutine(FallFaster());
            return true;
           
           
        }
        else 
        {
            //Debug.Log("Not Grounded");
            return false;
        }
    }

    //TODO: Tweak this to actually work
    IEnumerator FallFaster() 
    {
        yield return new WaitForSeconds(fallDelay);
        rb.linearDamping = .25f;
      //  yield return new WaitForSeconds(2);
      //  rb.linearDamping = rbDamping;

    }

}
