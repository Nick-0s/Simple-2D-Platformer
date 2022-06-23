using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private float _spawnPeriod;

    private CoinSpawnPoint[] _points;

    private void Awake()
    {
        _points = _spawnPoints.GetComponentsInChildren<CoinSpawnPoint>();

        foreach(CoinSpawnPoint point in _points)
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
        var spawnDelay = new WaitForSeconds(_spawnPeriod);
        int minIndexValue = 0;
        int pointIndex;
        bool isCoinSpawened;

        while(true)
        {
            isCoinSpawened = false;

            for(int i = 0; i < _points.Length && isCoinSpawened == false; i++)
            {
                pointIndex = Random.Range(minIndexValue, _points.Length);

                if(_points[pointIndex].CheckForCoin() == false)
                {
                    CreateNewCoinAtPoint(_points[pointIndex]);                    
                    isCoinSpawened = true;
                }
            }

            yield return spawnDelay;
        }
    }

    private void CreateNewCoinAtPoint(CoinSpawnPoint point)
    {
        Coin newCoin = Instantiate(_coin, point.transform.position, Quaternion.identity);
        point.SetCoin(newCoin);
    }
}