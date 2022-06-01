using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _distanceToCheck;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Vector2 _moveVector;
    private bool _isGrounded;
    private bool _isFaceToTheRight;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _isFaceToTheRight = true;
    }
    
    private void Update()
    {
        CheckingGround();
        Run();
        ReflectPlayer();
        Jump();
    }

    private void ReflectPlayer()
    {
        if (_moveVector.x > 0 && _isFaceToTheRight == false || _moveVector.x < 0 && _isFaceToTheRight)
        {
            transform.localScale *= new Vector2(-1, 1);
            _isFaceToTheRight = !(_isFaceToTheRight);
        }
    }

    private void Run()
    {
        string horizontal = "Horizontal";

        _moveVector.x = Input.GetAxis(horizontal);
        transform.Translate(_moveVector.x * _speed * Time.deltaTime, 0, 0);

        _animator.SetFloat(AnimatorPlayerController.SpeedX, Mathf.Abs(_moveVector.x));
    }

    private void Jump()
    {
        if(_isGrounded && Input.GetKeyDown(KeyCode.Space))
            _rigidBody.AddForce(Vector2.up * GetJumpForce(_jumpHeight), ForceMode2D.Impulse);
    }

    private float GetJumpForce(float jumpHeight)
    {
        return Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _rigidBody.gravityScale));
    }

    private void CheckingGround()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _distanceToCheck);
        Debug.Log(_isGrounded);
    }
}
