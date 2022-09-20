using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireJump : MonoBehaviour {
	public float duration = 5f;

	private float _curDuration;

	private void Update()
	{
		_curDuration += Time.deltaTime;
		if (_curDuration >= duration)
			Destroy(gameObject);
	}
}
