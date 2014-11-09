using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D flintgun;
	public Rigidbody2D minigun;
	public Rigidbody2D cannon;
	public Rigidbody2D pig;
	public Rigidbody2D trap;
	public Rigidbody2D activeWeapon;

    public GameObject explosion;

    private Transform flitngunObj;
    private Transform minigunObj;
    private Transform cannonObj;
    private Transform trapObj;

	public float speed = 20f;				// The speed the rocket will fire at.
	public float fireRate;
	private float fireTimer = 0f;
	public bool applyForceToBullet;
	
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

        switch (transform.root.gameObject.transform.name) 
		{
		case "hero1":
                print("hero1");
			FireButton += "Player1";
			AimHorizontalAxis += "Player1";
			AimVerticalAxis += "Player1";
			break;
		case "hero2":
            print("hero2");
			FireButton += "Player2";
			AimHorizontalAxis += "Player2";
			AimVerticalAxis += "Player2";
			break;
		case "hero3":
            print("hero3");
			FireButton += "Player3";
			AimHorizontalAxis += "Player3";
			AimVerticalAxis += "Player3";
			break;
		case "hero4":
            print("hero4");
			FireButton += "Player4";
			AimHorizontalAxis += "Player4";
			AimVerticalAxis += "Player4";
			break;
		}

        flitngunObj = transform.Find("Weapon/Flintlock");
        minigunObj = transform.Find("Weapon/MachineGun");
        cannonObj = transform.Find("Weapon/Cannon");
        trapObj = transform.Find("Weapon/Trap");
	}

    public void SwitchWeapon(int weapon)
    {
       // int weapon = Random.Range(0, 3);
        flitngunObj.active = false;
        minigunObj.active = false;
        cannonObj.active = false;
        trapObj.active = false;

        switch (weapon)
        {
            case 0:
                flitngunObj.active = true;
                activeWeapon = flintgun;
                speed = 50;
                fireRate = 0.5f;
                break;
            case 1:
                minigunObj.active = true;
                activeWeapon = minigun;
                speed = 50;
                fireRate = 0.2f;
                break;
            case 2:
                cannonObj.active = true;
                activeWeapon = cannon;
                speed = 20;
                fireRate = 1;
                break;
            case 3:
                trapObj.active = true;
                activeWeapon = trap;
                break;
        }
    }

	void Update ()
	{
		// If the fire button is pressed...
		//if(Input.GetButtonDown(FireButton))
		
		fireTimer -= Time.deltaTime;
		
		if( Input.GetAxisRaw(FireButton) != 0  && fireTimer <= 0.1f)
		{
			fireTimer = fireRate;
			
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Shoot");
			audio.Play();
			
			float v1,v2;
			v1 = Input.GetAxis (AimHorizontalAxis);
			v2 = Input.GetAxis (AimVerticalAxis);
			
			Vector2 v = new Vector2(v1, v2);
			v.Normalize();
			v1 = v.x;
			v2 = v.y;
			
			
			
			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				if (0.01f > v1 && v1 > -0.01 && 0.01f  > v2 && v2 > -0.01)
				{
					v1 = 1.0f;
					v2 = 0.0f;
				}

				Vector3 offset = new Vector3(v1, -v2, 0);

				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(activeWeapon, transform.position + offset*3, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
                // Create a quaternion with a random rotation in the z-axis.
                Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                // Instantiate the explosion where the rocket is with the random rotation.
                Instantiate(explosion, transform.position + offset * 3, randomRotation);
				Vector2 vel = new Vector2(v1*speed, -v2*speed);
				if (applyForceToBullet)
				{
					bulletInstance.AddForce(vel);
				}
				else
				{
					bulletInstance.velocity = vel;
				}
			}
			else
			{
				if (0.01f > v1 && v1 > -0.01 && 0.01f  > v2 && v2 > -0.01)
				{
					v1 = -1.0f;
					v2 = 0.0f;
				}

				Vector3 offset = new Vector3(v1, -v2, 0);

				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(activeWeapon, transform.position + offset*3, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
                Instantiate(explosion, transform.position + offset * 3, randomRotation);
				Vector2 vel = new Vector2(v1 * speed, -v2 * speed);
				if (applyForceToBullet)
				{
					bulletInstance.AddForce(vel);
				}
				else
				{
					bulletInstance.velocity = vel;
				}
			}
		}
	}
}