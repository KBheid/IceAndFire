using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : MonoBehaviour
{
	[SerializeField] GameObject icewallSpawnerPrefab;

	float movementSpeed;
	Vector2 direction;
	bool started = false;

	float maxDist;
	float distTravelled;
	
	public void SetOptions(float movementSpeed, Vector2 direction, float maxDist)
	{
		this.direction = direction;
		this.movementSpeed = movementSpeed;
		this.maxDist = maxDist;

		started = true;
	}

	private void Update()
	{
		if (started)
		{
			Vector3 movement = Time.deltaTime * movementSpeed * (Vector3)direction.normalized;
			transform.position += movement;

			distTravelled += movement.magnitude;

			if (distTravelled > maxDist)
				Destroy(gameObject);

		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (started)
		{
			// Spawn a spawner going up
			IceWallSpawner spawner = Instantiate(icewallSpawnerPrefab).GetComponent<IceWallSpawner>();
			spawner.transform.position = collision.gameObject.transform.position;
			spawner.SetOptions(new Vector2(0, 1), movementSpeed, 3);

			Destroy(gameObject);
		}
	}
}
