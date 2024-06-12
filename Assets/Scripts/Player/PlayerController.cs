using UnityEngine;
using UnityEngine.InputSystem;

//Declare Required Componenets: 
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //Test Mode Toggle;
    public bool testMode = true;

    //Player Camera:
    public float senseX;
    public float senseY;
    public Camera playerCamera;

    //Player Movement Variables:
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float jumpForce = 10f;
    public float gravity = -9.81f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    //private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private float rotationX = 0;
    private float rotationY = 0;

    private readonly bool canMove = true;

    //Unity Map Center Coordinates:
    private Vector3 mapCenter = new Vector3(1f, 0f, 14f);

    // Start is called before the first frame update
    void Start()
    {
      charController = GetComponent<CharacterController>();
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      //gravity = Physics.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Mouse Input:
        float mouseX = Input.GetAxis("Mouse X") * senseX;
        float mouseY = Input.GetAxis("Mouse Y") * senseY;
        //Handling rotations and camera movement the way Unity does it:
        rotationY += mouseX;
        rotationX -= mouseY;

        //Player Movement:
        Vector3 forwardMovement = transform.TransformDirection(Vector3.forward);
        Vector3 rightMovement = transform.TransformDirection(Vector3.right);

        //Get Input from Player:
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currntSpdX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float currntSpdY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forwardMovement * currntSpdX) + (rightMovement * currntSpdY);

        //Jumping:
        if (Input.GetButtonDown("Jump") && canMove && charController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!charController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //if (Input.GetKey(KeyCode.R) && canMove)
        //{
        //    characterController.height = crouchHeight;
        //    walkSpeed = crouchSpeed;
        //    runSpeed = crouchSpeed;

        //}
        //else
        //{
        //    characterController.height = defaultHeight;
        //    walkSpeed = 6f;
        //    runSpeed = 12f;
        //}

        charController.Move(moveDirection * Time.deltaTime);

        //Apply Movement:
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -90, 90);
            Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void Respawn()
    {
        //Reset Player Position:
        charController.enabled = false;
        transform.position = mapCenter;
        charController.enabled = true;
    }

}
