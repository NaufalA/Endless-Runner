using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement")] 
    public float moveAccel = 2;
    public float maxSpeed = 4;

    [Header("Jumping")] 
    public float jumpAccel;


    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;
    
    private bool _isJumping;
    private bool _isOnGround;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private CharacterSoundController _soundController;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _soundController = gameObject.GetComponent<CharacterSoundController>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isOnGround)
            {
                _isJumping = true;
                _soundController.PlayJump();
            }
        }
        _animator.SetBool("isOnGround", _isOnGround);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            Debug.Log("hit");
            if (!_isOnGround && _rigidbody.velocity.y <= 0.0f)
            {
                _isOnGround = true;
            }
        }
        else
        {
            _isOnGround = false;
        }
        
        Vector2 velocityVector = _rigidbody.velocity;

        if (_isJumping)
        {
            velocityVector.y += jumpAccel;
            _isJumping = false;
        }
        
        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        _rigidbody.velocity = velocityVector;
    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        Debug.DrawLine(position, position + Vector3.down * groundRaycastDistance, Color.white);
    }
}
