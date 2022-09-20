using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
	public GameObject[] toggleObjects;
	public MonoBehaviour[] toggleMonobehaviors;

	private bool _activated = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_activated)
			return;

		foreach (GameObject go in toggleObjects)
		{
			go.SetActive(!go.activeInHierarchy);
		}

		foreach (MonoBehaviour mb in toggleMonobehaviors)
		{
			mb.enabled = !mb.enabled;
		}

		_activated = true;
		StartCoroutine(nameof(Anim));
	}

	IEnumerator Anim()
	{
		float curDuration = 0;
		while (curDuration < 1f)
		{
			yield return new WaitForSeconds(0.1f);
			curDuration += 0.1f;

			Vector2 curTrans = transform.position;
			curTrans.y -= 0.02f;
			transform.position = curTrans;
		}
	}
}
