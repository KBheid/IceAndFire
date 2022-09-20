using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
	public float knockback;

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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Moveables
		if (collision.gameObject.layer == 8)
		{
			collision.gameObject.GetComponent<Rigidbody2D>().AddForce( ((Vector2) collision.gameObject.transform.position - collision.GetContact(0).point).normalized * knockback );
		}

		Destroy(gameObject);
	}

}
