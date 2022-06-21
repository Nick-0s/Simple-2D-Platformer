using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _distanceToCheck;
    [SerializeField] private CircleCollider2D _groundChecker;
    [SerializeField] private LayerMask Ground;

    static event UnityAction OnDeath;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Vector2 _moveVector;
    private bool _isGrounded;
    private bool _isFacedToTheRight;
    private int _coins;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _isFacedToTheRight = true;
        _coins = 0;
        OnDeath += DisablePlayer;
    }
    
    private void Update()
    {
        CheckingGround();
        Run();
        ReflectPlayer();
        Jump();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Coin>(out Coin coin))
            PickCoin(coin);
        else if (other.TryGetComponent<Enemy>(out Enemy enemy))
            OnDeath.Invoke();
    }

    private void ReflectPlayer()
    {
        if (_moveVector.x > 0 && _isFacedToTheRight == false || _moveVector.x < 0 && _isFacedToTheRight)
        {
            Vector2 vectorToReflectPlayer = new Vector2(-1, 1);

            transform.localScale *= vectorToReflectPlayer;
            _isFacedToTheRight = !(_isFacedToTheRight);
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
        int jumpCoefficient = -2;

        return Mathf.Sqrt(jumpHeight * jumpCoefficient * (Physics2D.gravity.y * _rigidBody.gravityScale));
    }

    private void CheckingGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.transform.position, _groundChecker.radius, Ground);
        _animator.SetBool(AnimatorPlayerController.IsOnGround, _isGrounded);
    }

    private void PickCoin(Coin coin)
    {
        _coins++;
        Destroy(coin.gameObject);
    }

    private void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
}
