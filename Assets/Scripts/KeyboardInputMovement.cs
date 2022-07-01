using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]

public class KeyboardInputMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;

    private Rigidbody2D _rigidBody;
    private GroundChecker _groundChecker;
    private AnimatorPlayerController _animatorPlayerController;
    private Vector2 _moveVector;
    private float _jumpForce;

    private void Awake()
    {
        int jumpCoefficient = -2;

        _rigidBody = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
        TryGetComponent<AnimatorPlayerController>(out _animatorPlayerController);
        _jumpForce = Mathf.Sqrt(_jumpHeight * jumpCoefficient * (Physics2D.gravity.y * _rigidBody.gravityScale));
    }
    
    private void Update()
    {
        Run();
        Jump();
    }

    private void Run()
    {
        string horizontal = "Horizontal";

        _moveVector.x = Input.GetAxis(horizontal);
        transform.Translate(_moveVector.x * _speed * Time.deltaTime, 0, 0);

        if(_animatorPlayerController)
            _animatorPlayerController.Run(_moveVector.x);
    }

    private void Jump()
    {
        bool onGround = _groundChecker.GetStatus();

        if(onGround && Input.GetKeyDown(KeyCode.Space))
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        if(_animatorPlayerController)
            _animatorPlayerController.Jump(onGround);
    }
}