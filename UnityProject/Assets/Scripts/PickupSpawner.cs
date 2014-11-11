using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour
{
	public GameObject[] pickups;				// Array of pickup prefabs with the bomb pickup first and health second.
	public float pickupDeliveryTime = 1f;	// Delay on delivery.
	public float dropRangeLeft;					// Smallest value of x in world coordinates the delivery can happen at.
	public float dropRangeRight;				// Largest value of x in world coordinates the delivery can happen at.
	public float highHealthThreshold = 75f;		// The health of the player, above which only bomb crates will be delivered.
	public float lowHealthThreshold = 25f;		// The health of the player, below which only health crates will be delivered.

	private float lastDropTime = 0f;

	private PlayerHealth playerHealth;			// Reference to the PlayerHealth script.


	void Awake ()
	{
		// Setting up the reference.
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}


	void Start ()
	{
		// Start the first delivery.
	}

	void Update()
	{
		if( lastDropTime < 0 )
		{
			lastDropTime = pickupDeliveryTime;
			DeliverPickup();
		}else
		{
			lastDropTime -= Time.deltaTime;

		}

	}

	public void DeliverPickup()
	{

		// Create a random x coordinate for the delivery in the drop range.
		float dropPosX = Random.Range(dropRangeLeft, dropRangeRight);

		// Create a position with the random x coordinate.
		Vector3 dropPos = new Vector3(dropPosX, 15f, 1f);
			Instantiate(pickups[0], dropPos, Quaternion.identity);

	}
}
