using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float defaultSpeed;
	public float sprintSpeed;
	public float playerRotationSpeed = 20;
	float speed;
	//NetworkView nView;

	Vector3 movement;                  
	Animator anim;                      
	Rigidbody playerRigidbody;          
	//int floorMask;                      
	//float camRayLength = 100f;          
	
	void Awake ()
	{
		//floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
		//nView = GetComponent<NetworkView> ();
	}
	
	
	void FixedUpdate ()
	{
		/*if (!nView.isMine) {
			return;
		}*/
		float h = Input.GetAxisRaw ("Horizontal Left Stick");
		float v = Input.GetAxisRaw ("Vertical Left Stick");
		if (Input.GetButton("A")) {
			speed = sprintSpeed;
		} else {
			speed = defaultSpeed;
		}
		Move (h, v);
		Turning ();
		Animating (h, v);
	}
	
	void Move (float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}
	
	void Turning ()
	{
		transform.Rotate(0,Input.GetAxis("Horizontal Right Stick") * Time.deltaTime * playerRotationSpeed,0);
		//transform.Rotate(0,Input.GetAxis("Vertical Right Stick") * Time.deltaTime * playerRotationSpeed,0);
		/*Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerDirection;
			playerDirection.x = Input.GetAxis("Horizontal Right Stick");
			playerDirection.z = Input.GetAxis("Vertical Right Stick");
			playerDirection.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerDirection);
			playerRigidbody.MoveRotation (newRotation);
		}*/
	}
	void Animating (float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}