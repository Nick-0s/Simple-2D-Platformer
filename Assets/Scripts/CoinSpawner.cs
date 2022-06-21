using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Transform _spawnPoints;

    private Transform[] _points;

    private void Awake()
    {
        _points = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.childCount; i++)
            _points[i] = _spawnPoints.GetChild(i);

        foreach(Transform point in _points)
        {
            CreateNewCoinAtPoint(point);
        }
    }
    
    private void Start()
    {
        Coroutine spawn = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var spawnDelay = new WaitForSeconds(2f);
        int minIndexValue = 0;
        int pointIndex;

        while(true)
        {
            pointIndex = Random.Range(minIndexValue, _points.Length);
            CreateNewCoinAtPoint(_points[pointIndex]);

            yield return spawnDelay;
        }
    }

    private void CreateNewCoinAtPoint(Transform point)
    {
        Coin newCoin = Instantiate(_coin, point.position, Quaternion.identity);
    }
}
