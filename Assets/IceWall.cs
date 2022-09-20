using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
	public delegate void IceWallDestroyed(Vector2 pos, float velocityToApply);
	public static IceWallDestroyed OnDestroyIceWall;

	public float velocityApplied;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<FireJump>() != null)
		{
			OnDestroyIceWall?.Invoke(transform.position, velocityApplied);
			Destroy(gameObject);
		}
	}
}
