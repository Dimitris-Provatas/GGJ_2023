using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(JournalController))]

public class FPSController : MonoBehaviour
{
  public float walkingSpeed = 7.5f;
  public float runningSpeed = 11.5f;
  public float jumpSpeed = 0f;
  public float gravity = 20.0f;
  public Camera playerCamera;
  public Camera inventoryCamera;
  public float lookSpeed = 2.0f;
  public float lookXLimit = 45.0f;

  CharacterController characterController;
  Vector3 moveDirection = Vector3.zero;
  float rotationX = 0;

  public Transform crouchingTransform;
  public LayerMask groundLayersMask;

  public static FPSController instance;

  [HideInInspector]
  public bool canMove = true; // for crouch locking
  public bool lockMovement = false; // for journal locking
  public bool canLook = true;
  public bool canCrouch = true;

  //private RaycastHit hitData;

  /// <summary>
  /// Entirely freezes the player. Disables movement, mouse looking and crouching.
  /// </summary>
  public void FreezePlayer()
  {
    lockMovement = true;
    canLook = false;
    canCrouch = false;
  }

  /// <summary>
  /// Re-enables movement for the player.
  /// </summary>
  public void UnfreezePlayer()
  {
    lockMovement = false;
    canLook = true;
    canCrouch = true;
  }

  private void Start()
  {
    characterController = GetComponent<CharacterController>();

    // Lock cursor
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    instance = this;
  }

  private void Update()
  {
    // We are grounded, so recalculate move direction based on axes
    Vector3 forward = transform.TransformDirection(Vector3.forward);
    Vector3 right = transform.TransformDirection(Vector3.right);


    // Press Left Shift to run
    bool isRunning = Input.GetKey(KeyCode.LeftShift);
    float curSpeedX = (canMove && !lockMovement) ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
    float curSpeedY = (canMove && !lockMovement) ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
    float movementDirectionY = moveDirection.y;
    moveDirection = (forward * curSpeedX) + (right * curSpeedY);
    //canMove = !Input.GetKey(KeyCode.C);

    // Jumping functionality.
    if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
    {
      moveDirection.y = jumpSpeed;
    }
    else
    {
      moveDirection.y = movementDirectionY;
    }

    // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
    // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
    // as an acceleration (ms^-2)
    if (!characterController.isGrounded)
    {
      moveDirection.y -= gravity * Time.deltaTime;
    }

    // Move the controller
    characterController.Move(moveDirection * Time.deltaTime);

    if (canMove && Mathf.Abs(moveDirection.x) > 0.0001f && Mathf.Abs(moveDirection.z) > 0.0001f)
    {
      Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Vector3.down, out RaycastHit hitData, 2f, groundLayersMask);

      SoundManager.instance.PlayWalkingSound(hitData.transform.gameObject.layer == LayerMask.NameToLayer("groundWood"));
    }

    // Player and Camera rotation
    if (canLook)
    {
      rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
      rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
      playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
      transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    //float cameraHeight = !Input.GetKey(KeyCode.C) ? 0 : -0.8f;

    if (Input.GetKey(KeyCode.C) && characterController.isGrounded && canCrouch)
    {
      crouchingTransform.localPosition = Vector3.Lerp(crouchingTransform.localPosition, new Vector3(crouchingTransform.localPosition.x, -0.8f, crouchingTransform.localPosition.z), 0.2f);
      canMove = false;
    }
    else
    {
      crouchingTransform.localPosition = Vector3.Lerp(crouchingTransform.localPosition, new Vector3(crouchingTransform.localPosition.x, 0, crouchingTransform.localPosition.z), 0.2f);
      canMove = true;
    }

    // Sync player and inventory camera
    //inventoryCamera.transform.position = playerCamera.transform.position;
    //inventoryCamera.transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);
  }
}