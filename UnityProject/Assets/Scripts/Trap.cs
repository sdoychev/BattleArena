using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
    public float damage;


	void Start () 
	{
		;
	}


	void OnTrapStepped()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If player steps on the trap...
		if(col.tag == "Player")
		{
			// ... find the Enemy script and call the Hurt function.
			col.GetComponent<PlayerHealth>().ApplyDamage(damage);


			// Call the explosion instantiation.
			OnTrapStepped();

			// Destroy the trap.
			Destroy (gameObject);
		}
        /*
        else
        {
            // Call the explosion instantiation.
            OnTrapStepped();

            // Destroy the trap.
            Destroy(gameObject);
        }
		*/
	}

}
