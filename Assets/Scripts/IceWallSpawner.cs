using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWallSpawner : MonoBehaviour
{
	[SerializeField] GameObject iceWallPrefab;

	private Vector2 movementDirection;
    private float movementSpeed;
	private int maxSpawned;

	private int amountSpawned;
	private bool started = false;

	private int numOverlaps = 0;

	private void Start()
	{
		float yOff = GetComponent<Collider2D>().bounds.extents.y;
		transform.position = new Vector2(transform.position.x, transform.position.y-yOff);
	}

	public void SetOptions(Vector2 movementDir, float movementSpeed, int maxSpawned)
	{
		this.movementDirection = movementDir;
		this.movementSpeed = movementSpeed;
		this.maxSpawned = maxSpawned;

		started = true;
		amountSpawned = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (started)
		{
			Vector3 moveAmount = movementSpeed * Time.deltaTime * (Vector3)movementDirection.normalized;
			moveAmount.z = 0;
			transform.position += moveAmount;
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Destroy on re-entry
		if (amountSpawned > 0 && collision.GetComponent<IceWall>() == null)
			Destroy(gameObject);

		numOverlaps++;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		numOverlaps--;

		if (started && numOverlaps == 0)
		{
			float rotAroundZ = Mathf.Atan(movementDirection.y/movementDirection.x) * 180 / Mathf.PI;

			Instantiate(iceWallPrefab, transform.position, Quaternion.Euler(0, 0, rotAroundZ), null);

			amountSpawned++;
			if (amountSpawned >= maxSpawned)
			{
				Destroy(gameObject);
			}
		}
	}

}
