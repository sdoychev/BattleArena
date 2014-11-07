using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.


	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.

	private string FireButton = "Fire";
	private string AimHorizontalAxis = "AimHorizontal";
	private string AimVerticalAxis = "AimVertical";

	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
		
		switch (gameObject.transform.parent.transform.name) 
		{
		case "hero1":  
			FireButton += "Player1";
			AimHorizontalAxis += "Player1";
			AimVerticalAxis += "Player1";
			break;
		case "hero2":
			FireButton += "Player2";
			AimHorizontalAxis += "Player2";
			AimVerticalAxis += "Player2";
			break;
		case "hero3":
			FireButton += "Player3";
			AimHorizontalAxis += "Player3";
			AimVerticalAxis += "Player3";
			break;
		case "hero4":
			FireButton += "Player4";
			AimHorizontalAxis += "Player4";
			AimVerticalAxis += "Player4";
			break;
		}
	}


	void Update ()
	{
		if (gameObject.transform.parent.transform.name == "hero4")
			Debug.Log (AimHorizontalAxis + "   " + Input.GetAxis (AimHorizontalAxis) + "   " + Input.GetAxis (AimVerticalAxis));

		// If the fire button is pressed...
		//if(Input.GetButtonDown(FireButton))
		if( Input.GetAxisRaw(FireButton) != 0)
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Shoot");
			audio.Play();

			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);
			}
			else
			{
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-speed, 0);
			}
		}
	}
}
