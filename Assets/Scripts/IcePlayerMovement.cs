using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlayerMovement : PlayerMovement
{
	[SerializeField] GameObject iceWallSpawnerPrefab;
	[SerializeField] GameObject iceProjectilePrefab;

	protected override void Jump()
	{
		if (_grounded)
		{
			_rb.AddForce(new Vector2(0f, 250f));
		}
	}

	protected override void ShootDown()
	{
		if (_grounded)
		{
			float facingLeft = (_facingLeft) ? -1 : 1;

			Vector2 bounds = GetComponent<Collider2D>().bounds.extents;
			float yBot = GetComponent<Collider2D>().bounds.center.y - bounds.y;
			Vector2 pos = transform.position;
			pos.y = yBot;

			IceWallSpawner spawner = Instantiate(iceWallSpawnerPrefab).GetComponent<IceWallSpawner>();
			spawner.transform.position = pos;
			spawner.SetOptions(new Vector2(facingLeft, 0), 4.5f, 3);
		}
	}

	protected override void ShootLeft()
	{
		IceProjectile ip = Instantiate(iceProjectilePrefab).GetComponent<IceProjectile>();
		ip.transform.position = transform.position;
		ip.SetOptions(4, new Vector2(-1, 0), 30);
	}

	protected override void ShootRight()
	{
		IceProjectile ip = Instantiate(iceProjectilePrefab).GetComponent<IceProjectile>();
		ip.transform.position = transform.position;
		ip.SetOptions(4, new Vector2(1, 0), 30);
	}
}
