using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    public bool FacingLeft{
        get { return facingLeft; }
        set { facingLeft = value;}
    }

    public static Player Instance;

    [SerializeField] private float moveSpeed = 1f;
 
    private PlayerControls playerControls;
 
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
 
    private Vector2 movement;

    private bool facingLeft = false;

     
    private void Awake() {
        Instance = this;
        playerControls = new PlayerControls();
 
        rb = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
 
    }
 
    private void OnEnable() {
 
        playerControls.Enable();
 
    }
 
    private void Update() {
 
        PlayerInput();
 
    }
 
    private void FixedUpdate() {
        AjustPlayerFacingPosition();
        Move();
    }
 
    private void PlayerInput() {
 
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }
 
    private void Move() {
 
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
 
    }

    private void AjustPlayerFacingPosition(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            FacingLeft = false; 
       }
    }
    
}
