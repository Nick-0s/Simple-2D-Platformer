using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    private bool _isEmpty;
    private Coin _coin;

    public bool CheckForCoin()
    {
        return _coin;
    }

    public void SetCoin(Coin coin)
    {
        _coin = coin;
    }
}