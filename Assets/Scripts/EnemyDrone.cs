using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class EnemyDrone : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private float _movementDeviation;

    private SpriteRenderer _spriteRenderer;
    private Transform[] _wayPoints;
    private int _targetPointIndex;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _wayPoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _wayPoints[i] = _path.GetChild(i);

        _targetPointIndex = 0;
    }

    private void Update()
    {
        Transform targetPoint = _wayPoints[_targetPointIndex];

        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, _speed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x - targetPoint.position.x) < _movementDeviation && Mathf.Abs(transform.position.y - targetPoint.position.y) < _movementDeviation)
        {
            _targetPointIndex++;

            if(_targetPointIndex >= _wayPoints.Length)
                _targetPointIndex = 0;
        }

        if(targetPoint.position.x - transform.position.x > 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }
}