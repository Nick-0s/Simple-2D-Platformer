using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    private Coin _coin;

    public bool CheckForCoin()
    {
        return _coin != null;
    }

    public void SetCoin(Coin coin)
    {
        if(_coin == null)
            _coin = coin;
    }
}