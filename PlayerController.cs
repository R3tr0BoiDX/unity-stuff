using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpHeigt;
    [SerializeField] private float deathzone;
    [SerializeField] private float subsideSpeed;
    [SerializeField] private float edgeJumpTime;
    [SerializeField] private float gravity;
    [SerializeField] private float maxFallSpeed;

	//groundCheck is a empty gameobject which sits direct under your players feets
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    private bool grounded;
    private bool jump;
    private bool edgeJump;
    private bool jumpLock;
    private bool changeJumpLock;
    private bool? walkRight;
    private float edgeJumpTimeStore;
    private Rigidbody2D rigid;

    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        edgeJumpTimeStore = edgeJumpTime;
    }

    void Update() {
		//Check if you standig on a ground. Add a "Ground" layer to you LayerMasks and assign them to your ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		//Get input for walking
        if (Input.GetAxisRaw("Horizontal") > deathzone) {
            walkRight = true;
        } else if (Input.GetAxisRaw("Horizontal") < -deathzone) {
            walkRight = false;
        } else {
            walkRight = null;
        }

		//Get input for jumping
        if (grounded && Input.GetButtonDown("Jump")) {
            jump = true;
        }
		
		//Get input for jumping after falling of a edge. Note: "edgeJumpTime" should be a very small value, like about 0.125
        if (!grounded && edgeJumpTime > 0 && Input.GetButtonDown("Jump")) {
            edgeJump = true;
        }
    }

    void FixedUpdate() {
		
		//Walk
        if (walkRight != null) {
            if (walkRight == true) {
                rigid.velocity = new Vector2(walkSpeed, rigid.velocity.y);
                transform.localScale = Vector3.one;
            } else if (walkRight == false) {
                rigid.velocity = new Vector2(-walkSpeed, rigid.velocity.y);
                transform.localScale = Vector3.one - 2 * Vector3.right;
            }
		//Slow down after key up
        } else {
            if (rigid.velocity.x != 0) {
                if (rigid.velocity.x > 0) {
                    rigid.velocity -= Vector2.right * subsideSpeed;
                }
                if (rigid.velocity.x < 0) {
                    rigid.velocity += Vector2.right * subsideSpeed;
                }
            }
        }

		//Jump
        if (jump || edgeJump) {
            if (!jumpLock) {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpHeigt);
                jumpLock = true;
            }
            jump = false;
            edgeJump = false;
        }

		//Do grounding check stuff
        if (!grounded) {
			
			//Gravity with limited fall speed
            if (rigid.velocity.y < -maxFallSpeed) {
                rigid.velocity = Vector2.up * -maxFallSpeed;
            } else {
                rigid.velocity -= Vector2.up * gravity;
            }
			
			//Counting time down, where you can jump after you fall of a edge
            if (edgeJumpTime >= 0) {
                edgeJumpTime -= Time.deltaTime;
            }
            changeJumpLock = true;
		//Reseting the edge jump things. Note: After you press your jump key, there a still a short time where grounded equals true,
		//because the overlap circle is still overlapping with the ground collider. That's why we need the changeJumpLock variable.
        } else {
            if (edgeJumpTime != edgeJumpTimeStore) {
                edgeJumpTime = edgeJumpTimeStore;
            }
            if (changeJumpLock) {
                jumpLock = false;
                changeJumpLock = false;
            }
        }
    }
}