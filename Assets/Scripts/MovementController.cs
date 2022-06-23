using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private CircleCollider2D _groundChecker;
    [SerializeField] private LayerMask Ground;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Vector2 _moveVector;
    private bool _isGrounded;
    private bool _isFacedToTheRight;
    private float _jumpForce;

    private void Awake()
    {
        int jumpCoefficient = -2;

        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _isFacedToTheRight = true;
        _jumpForce = Mathf.Sqrt(_jumpHeight * jumpCoefficient * (Physics2D.gravity.y * _rigidBody.gravityScale));
    }
    
    private void Update()
    {
        CheckForGround();
        Run();
        ReflectPlayer();
        Jump();
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
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckForGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.transform.position, _groundChecker.radius, Ground);
        _animator.SetBool(AnimatorPlayerController.IsOnGround, _isGrounded);
    }
}