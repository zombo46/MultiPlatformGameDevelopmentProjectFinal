using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Public variables for player settings

    public GameObject playerMenu;
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float baseLookSpeed = 1.0f;
    public float mouseSensitivty = 1.0f; 
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchMultiplier = 0.5f;

    // Player movement
    private PlayerInputActions inputActions;
    private InputAction moveAction;
    private InputAction lookAction; 
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction sprintAction;

    // Interaction settings
    public Transform interactionPoint;
    public float interactionRange = 1.5f;

    // Private/internal state
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f; // pitch
    private float rotationY = 0f; // yaw
    private CharacterController characterController;
    private bool isCrouched = false;
    private bool canMove = true;

    // Keep originals to avoid repeatedly multiplying/dividing speeds
    private float baseWalkSpeed;
    private float baseRunSpeed;

    // Vertical velocity tracked separately for consistent grounding/jumping
    private float verticalVelocity = 0f;

    [Header("Invert Y")]
    public bool invertY = false;

    void Awake()
    {
        inputActions = InputManager.Instance.inputActions;
        moveAction = inputActions.Gameplay.Move;
        lookAction = inputActions.Gameplay.Look;
        jumpAction = inputActions.Gameplay.Jump;
        interactAction = inputActions.Gameplay.Interact;
        sprintAction = inputActions.Gameplay.Sprint;

        inputActions.Gameplay.Enable();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        LoadGameplaySettings();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        baseWalkSpeed = walkSpeed;
        baseRunSpeed = runSpeed;

        rotationY = transform.eulerAngles.y;
        if (playerCamera != null)
        {
            float camPitch = playerCamera.transform.localEulerAngles.x;
            if (camPitch > 180f) camPitch -= 360f;
            rotationX = camPitch;
        }

        if (interactionPoint == null)
        {
            interactionPoint = this.transform;
        }

        // Ensure controller center corresponds to default height
        characterController.height = Mathf.Max(0.1f, defaultHeight);
        characterController.center = new Vector3(0f, characterController.height / 3f, 0f);
    }

    void Update()
    {
        Movement();
        Interaction();
    }

    public void setMovable(bool movability)
    {
        canMove = movability;
    }

    private void Movement()
    {
        if (!canMove)
            return;

        // Input System:
        float inputZ = 0f; // forward/back
        float inputX = 0f; // left/right

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        inputZ = moveInput.y;
        inputX = moveInput.x;
        

        // Running (Keyboard shift)
        bool isRunning = sprintAction.IsPressed();

        // Determine horizontal speed (applies crouch multiplier if active)
        float speed = (isRunning ? baseRunSpeed : baseWalkSpeed) * (isCrouched ? crouchMultiplier : 1f);

        // Horizontal movement (relative to player)
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 horizontalMove = (forward * inputZ + right * inputX);
        if (horizontalMove.sqrMagnitude > 1f) horizontalMove = horizontalMove.normalized;
        horizontalMove *= speed;

        // Grounding & vertical velocity
        bool jumpPressed = jumpAction.triggered;
        
        if (characterController.isGrounded)
        {
            // small downward force to keep grounded
            if (verticalVelocity < 0f) verticalVelocity = -1f;

            if (jumpPressed && !isCrouched)
            {
                verticalVelocity = jumpPower;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Assemble final move vector and move controller
        moveDirection = horizontalMove;
        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * Time.deltaTime);

        // Crouch toggle
        bool crouchToggled = (Keyboard.current != null && Keyboard.current.leftCtrlKey.wasPressedThisFrame);
        if (crouchToggled)
            ToggleCrouch();

        // Mouse look (Input System)
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

        float mouseX = mouseDelta.x;
        float mouseY = mouseDelta.y;

        // Invert Y if enabled
        float yMultiplier = invertY ? 1f : -1f;

        // Sensitivity level
        float sensitivity = baseLookSpeed * mouseSensitivty;

        rotationX += mouseY * sensitivity * yMultiplier;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        rotationY += mouseX * sensitivity;

        if (playerCamera != null)
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    private void ToggleCrouch()
    {
        if (!isCrouched)
        {
            // Crouch
            characterController.height = Mathf.Max(0.1f, defaultHeight * crouchMultiplier);
            characterController.center = new Vector3(0f, characterController.height / 2f, 0f);
            isCrouched = true;
        }
        else
        {
            // Stand up
            characterController.height = Mathf.Max(0.1f, defaultHeight);
            characterController.center = new Vector3(0f, characterController.height / 3f, 0f);
            isCrouched = false;
        }
    }
    // Public to allow other scripts to enable / disable player movement.
    public void SetCanMove(bool enabled)
    {
        canMove = enabled;
        if (!canMove)
        {
            // stop any residual movement and rotation immediately
            moveDirection = Vector3.zero;
            verticalVelocity = 0f;
        }
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    private void Interaction()
    {
        bool interactPressed = interactAction.triggered;

        if (!interactPressed) return;

        Collider[] colliders = Physics.OverlapSphere(interactionPoint.position, interactionRange);
        foreach (Collider collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(collider);
                break;
            }
        }
    }

    void OnGUI()
    {
        if (!playerMenu.transform.GetChild(1).gameObject.activeInHierarchy && Event.current.Equals(Event.KeyboardEvent("tab")))
        {            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            canMove = false;

            playerMenu.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnPlayerMenuExit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        LoadGameplaySettings();
        canMove = true;
    }

    public void setInvertY(bool value)
    {
        invertY = value;
    }

    public void LoadGameplaySettings()
    {
        invertY = PlayerPrefs.GetInt("masterInvertY", 0) == 1;
        mouseSensitivty = PlayerPrefs.GetFloat("masterSen", 1.0f);
    }
}