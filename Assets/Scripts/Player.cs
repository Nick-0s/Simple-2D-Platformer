using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Wallet))]

public class Player : MonoBehaviour
{
    static event UnityAction OnDeath;

    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        OnDeath += DisablePlayer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Coin>(out Coin coin))
            PickCoin(coin);
        else if (other.TryGetComponent<Enemy>(out Enemy enemy))
            OnDeath.Invoke();
    }

    private void PickCoin(Coin coin)
    {
        _wallet.ChangeAmountBy(coin.value);
        Destroy(coin.gameObject);
    }

    private void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
}