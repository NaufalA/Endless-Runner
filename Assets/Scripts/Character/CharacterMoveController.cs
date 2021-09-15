using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement")] 
    public float moveAccel = 2;
    public float maxSpeed = 4;

    [Header("Jumping")] 
    public float jumpAccel;
    private bool _isJumping;
    private bool _isOnGround;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    [Header("Scoring")]
    public ScoreController scoring;
    public int scoringRatio = 2;
    private int _lastPositionX;

    [Header("Game Over")]
    public GameObject gameOverScreen;
    public float fallPositionY;

    [Header("Camera")]
    public CameraMoveController gameCamera;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        _animator.SetBool("isOnGround", _isOnGround);

        int distancePassed = Mathf.FloorToInt(transform.position.x - _lastPositionX);
        int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);

        if (scoreIncrement > 0)
        {
            scoring.IncreaseCurrenScore(scoreIncrement);
            _lastPositionX += distancePassed;
        }

        if (transform.position.y <= fallPositionY)
        {
            GameOver();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
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

    public void Jump()
    {
        if (!_isOnGround) return;
        
        _isJumping = true;
        _soundController.PlayJump();
    }

    private void GameOver()
    {
        scoring.FinishScoring();

        gameCamera.enabled = false;
        
        gameOverScreen.SetActive(true);

        this.enabled = false;
    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        Debug.DrawLine(position, position + Vector3.down * groundRaycastDistance, Color.white);
    }
}
