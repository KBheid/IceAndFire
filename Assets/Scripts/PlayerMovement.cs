using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{


    public float movementSpeed = 5f;
    public float maxSpeed = 10f;

    public GameObject activePlayerObject;

    protected bool _facingLeft = false;

    protected Rigidbody2D _rb;
    protected bool _grounded = false;

    [SerializeField]
    private bool isActivePlayer = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (isActivePlayer)
		{
            activePlayerObject.transform.position = transform.position;
            activePlayerObject.transform.parent = transform;
		}

        IceWall.OnDestroyIceWall += OnIcewallBreak;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _grounded = IsGrounded();

        if (isActivePlayer)
        {

            // Jump
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
            // Right
            if (Input.GetKey(KeyCode.D))
            {
                _facingLeft = false;
                if (_rb.velocity.magnitude < maxSpeed)
                    _rb.AddForce(new Vector2(Time.deltaTime * movementSpeed, 0));
            }
            // Left
            if (Input.GetKey(KeyCode.A))
            {
                _facingLeft = true;
                if (_rb.velocity.magnitude < maxSpeed)
                    _rb.AddForce(new Vector2(Time.deltaTime * -movementSpeed, 0));
            }

            // Shoot
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShootDown();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShootRight();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShootLeft();
            }
        }

        // Switch player
        if (Input.GetKeyDown(KeyCode.Space)) {
            isActivePlayer = !isActivePlayer;
            if (isActivePlayer)
            {
                activePlayerObject.transform.position = transform.position;
                activePlayerObject.transform.parent = transform;
            }
        }
    }


    protected bool IsGrounded()
	{
        Vector2 bounds = GetComponent<Collider2D>().bounds.size;
        float yBot = GetComponent<Collider2D>().bounds.center.y - bounds.x/2;
        Vector2 pos = transform.position;
        pos.y = yBot;

        RaycastHit2D rch = Physics2D.BoxCast(pos, new Vector2(bounds.x, 0.35f), 0f, new Vector2(0, -1), distance: 0f, layerMask: LayerMask.GetMask(new string[] { "Wall", "Moveable" }));

        return rch.collider != null;
	}
    protected abstract void Jump();
    protected abstract void ShootDown();
    protected abstract void ShootLeft();
    protected abstract void ShootRight();

    protected virtual void OnIcewallBreak(Vector2 wallPos, float velocity)
	{
        // Get difference, apply velocity
        Vector2 direction = (Vector2) transform.position - wallPos;
        if (direction.magnitude < 2f)
        {
            _rb.velocity = direction.normalized * velocity;
        }
	}
}
