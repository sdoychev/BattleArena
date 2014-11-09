using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.

	private string JumpButton = "Jump";
	private string HorizontalAxis = "Horizontal";

	private string AimHorizontalAxis = "AimHorizontal";
	private string AimVerticalAxis = "AimVertical";

	private float jumpTimer = 0f;

	private char heroIndex = '0';

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
		
		switch (gameObject.name) 
		{
		case "hero1": 
			JumpButton += "Player1";
			HorizontalAxis += "Player1";
			AimHorizontalAxis += "Player1";
			AimVerticalAxis += "Player1";
			heroIndex = '1';
			break;
		case "hero2":
			JumpButton += "Player2";
			HorizontalAxis += "Player2";
			AimHorizontalAxis += "Player2";
			AimVerticalAxis += "Player2";
			heroIndex = '2';
			break;
		case "hero3":
			JumpButton += "Player3";
			HorizontalAxis += "Player3";
			AimHorizontalAxis += "Player3";
			AimVerticalAxis += "Player3";
			heroIndex = '3';
			break;
		case "hero4":
			JumpButton += "Player4";
			HorizontalAxis += "Player4";
			AimHorizontalAxis += "Player4";
			AimVerticalAxis += "Player4";
			heroIndex = '4';
			break;
		}
		
		int roundIndex = 1; //TODO - get round index

		heroIndex = (char)(heroIndex + roundIndex - 1);
		if (heroIndex > '4')
		    heroIndex = '1';

		GameObject spawnerObj = GameObject.Find ("hero_spawner" + heroIndex);
		//TODO this.transform.position = spawnerObj.transform.position;
	}


	void Update()
	{
		if (jumpTimer > 0) {
						jumpTimer -= Time.deltaTime;
				}

		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		//if (gameObject.name == "hero4")

			//Debug.Log (grounded);

		// If the jump button is pressed and the player is grounded then the player should jump.
		if((Input.GetAxisRaw(JumpButton) != 0) && grounded && jumpTimer <= 0f)
			jump = true;
	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis(HorizontalAxis);
		float hd = Input.GetAxis(AimHorizontalAxis);

		if (hd == 0) //if no aim - get the move direction
			hd = h;
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left...
		if(hd > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(hd < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
			jumpTimer = 0.09f;
		}

		if (gameObject.name == "hero"+heroIndex) 
		{
			if (gameObject.rigidbody2D.velocity.x != 0)
			{
				GameObject.Find("Krok"+heroIndex).GetComponent<Animation>().Play("Krokodil_Armature|RunCycle", PlayMode.StopAll);
			}
			else 
			{
				GameObject.Find("Krok"+heroIndex).GetComponent<Animation>().Play("Krokodil_Armature|Idle", PlayMode.StopAll);
			}
		}

	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!audio.isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				audio.clip = taunts[tauntIndex];
				audio.Play();
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}
