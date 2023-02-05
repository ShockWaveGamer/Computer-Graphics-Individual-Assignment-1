using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Components



    #endregion

    #region Inspector

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float jumpPower = 5f;

    #endregion

    #region Variables

    Transform cameraFollowTarget;

    PlayerInput playerInput;
    InputAction movement, look, jump;

    Rigidbody body;

    Vector3 groundNormal;

    bool isGrounded;

    #endregion

    private void Awake()
    {
        cameraFollowTarget = gameObject.transform.Find("Follow Target").transform;

        playerInput = GetComponent<PlayerInput>();
        #region player input

        movement = playerInput.actions["Movement"];
        look = playerInput.actions["Look"];
        jump = playerInput.actions["Jump"];

        #endregion

        body = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        jump.started += ctx => Jump();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        cameraFollowTarget.rotation = Camera.main.transform.rotation;
    }

    private void Move()
    {
        Vector3 forwardVector, rightVector;

        forwardVector = Vector3.Cross(cameraFollowTarget.transform.right, groundNormal);
        rightVector = Vector3.Cross(cameraFollowTarget.transform.forward, groundNormal);

        Vector3 movementVector = (forwardVector * movement.ReadValue<Vector2>().y + rightVector * -movement.ReadValue<Vector2>().x).normalized;
        if (isGrounded) body.velocity = Vector3.Lerp(body.velocity, movementVector * moveSpeed, Time.deltaTime * 10f);
        else body.AddForce(movementVector, ForceMode.Force);

        if (forwardVector != Vector3.zero) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forwardVector), Time.deltaTime * 10f);
    }

    private void Jump()
    {
        if (!isGrounded) return;
        
        body.AddForce(groundNormal * jumpPower, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.y >= 0.5f)
        {
            if (isGrounded) groundNormal = collision.contacts[0].normal;
            else isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(CoyoteTime());
        IEnumerator CoyoteTime()
        {
            yield return new WaitForSeconds(0.1f);
            isGrounded = false;
            groundNormal = Vector3.up;
        }
    }
}
