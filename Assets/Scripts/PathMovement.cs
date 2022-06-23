using UnityEngine;

public class PathMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private float _movementDeviation;

    private SpriteRenderer _spriteRenderer;
    private bool _hasSpriteRenderer;
    private Transform[] _wayPoints;
    private Transform _targetPoint;
    private int _targetPointIndex;

    private void Awake()
    {
        _hasSpriteRenderer = TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        _wayPoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _wayPoints[i] = _path.GetChild(i);

        _targetPointIndex = 0;
    }

    private void FixedUpdate()
    {
        Move();
        ReflectObjectInMovementDirection();
    }

    private void Move()
    {
        _targetPoint = _wayPoints[_targetPointIndex];

        transform.position = Vector2.MoveTowards(transform.position, _targetPoint.position, _speed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x - _targetPoint.position.x) < _movementDeviation && Mathf.Abs(transform.position.y - _targetPoint.position.y) < _movementDeviation)
        {
            _targetPointIndex++;

            if(_targetPointIndex >= _wayPoints.Length)
                _targetPointIndex = 0;
        }
    }

    private void ReflectObjectInMovementDirection()
    {
        _spriteRenderer.flipX = _targetPoint.position.x > transform.position.x;
    }
}