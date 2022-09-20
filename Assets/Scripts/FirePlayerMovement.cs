using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayerMovement : PlayerMovement
{
	[SerializeField] GameObject fireProjectilePrefab;
	[SerializeField] GameObject fireJumpArea;

	ParticleSystem _particleSystem;

	bool _doubleJumped = false;

	float _groundCheckDelay = 0.25f;
	float _curDelay = 0f;

	protected override void Start()
	{
		base.Start();
		_particleSystem = GetComponent<ParticleSystem>();
	}

	protected override void Update()
	{
		base.Update();

		_curDelay += Time.deltaTime;

		if (_grounded && _curDelay >= _groundCheckDelay)
		{
			_doubleJumped = false;
		}
	}

	protected override void Jump()
	{
		if (_grounded)
		{
			_rb.AddForce(new Vector2(0f, 300f));
			_grounded = false;
			_curDelay = 0;
		}
		else if (!_doubleJumped)
		{
			_rb.velocity = new Vector2(_rb.velocity.x, 5f);
			_doubleJumped = true;
			Vector2 areaPos = transform.position;
			areaPos.y -= 0.5f;
			Instantiate(fireJumpArea, areaPos, Quaternion.identity);
			_particleSystem.Play();
		}
	}

	protected override void ShootDown() { }

	protected override void ShootLeft()
	{
		FireProjectile fp = Instantiate(fireProjectilePrefab).GetComponent<FireProjectile>();
		fp.transform.position = transform.position;
		fp.SetOptions(6, new Vector2(-1, 0), 5);
	}

	protected override void ShootRight()
	{
		FireProjectile fp = Instantiate(fireProjectilePrefab).GetComponent<FireProjectile>();
		fp.transform.position = transform.position;
		fp.SetOptions(6, new Vector2(1, 0), 5);
	}

	protected override void OnIcewallBreak(Vector2 wallPos, float velocity)
	{
		base.OnIcewallBreak(wallPos, velocity);
		_doubleJumped = false;
	}
}
