using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private ControllerColliderHit contact;
    private Animator animator;


    public float rotSpeed = 15.0f;
    public float moveSpeed = 6f;
    public float gravity = -9.81f;
    public float terminalVelocity = -10f;
    public float jumpSpeed = 15f;
    public float minFall = -1.5f;
    public float pushForce = 3f;

    private float verticalSpeed;

    private CharacterController character;

	private void Start()
	{
        verticalSpeed = minFall;
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	void Update()
    {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        if (horInput != 0 || verInput != 0)
		{
            
            movement.x = horInput * moveSpeed;
            movement.z = verInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);


            //Camera Movement///////////////////////////////////////////////
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            
            
            //Transform movement from local to world///////
            movement = target.TransformDirection(movement);
            
            

            target.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
            ///////////////////////////////////////////////////////////////
            
        }


        animator.SetFloat("Speed", movement.sqrMagnitude);

        
        //Ground check by raycasting. Because the character controller's ground check causes problems.///////////////
        bool hitGround = false;
        RaycastHit hit;
        if (verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
		{
            
            //float check is half height of the character controller. Because that's where the ray originates from. 1.9 is used instead of 2 to check a bit farther
            float check = (character.height + character.radius) / 1.9f;
            hitGround = hit.distance <= check;
		}
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////


        
        
        //jumping system//////////////////////////////////////////

        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpSpeed;
            }
            else
            {
                verticalSpeed = minFall;
                //verticalSpeed = -0.1f;
                animator.SetBool("Jumping", false);
            }
        }
        else
        {
            verticalSpeed += gravity * 5 * Time.deltaTime;
            if (verticalSpeed < terminalVelocity)
            {
                verticalSpeed = terminalVelocity;
            }

            if (contact != null)
			{
                animator.SetBool("Jumping", true);
			}


            //If the raycasting didn't detect ground but the character controller did, it'll give a little nudge to the player.
            if (character.isGrounded)
			{
                if (Vector3.Dot(movement, contact.normal) < 0)
				{
                    movement = contact.normal * moveSpeed;
				}
				else
				{
                    movement += contact.normal * moveSpeed;
				}
			}
        }
        movement.y = verticalSpeed;

        //////////////////////////////////////////////////////////
        


        // Character Movement
        movement *= Time.deltaTime;
        character.Move(movement);
    }


	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
		{
            body.velocity = hit.moveDirection * pushForce;
		}
	}
}
