using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Wallet))]

public class Player : MonoBehaviour
{
    public event UnityAction Died;

    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        Died += DisablePlayer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Coin>(out Coin coin))
            PickCoin(coin);
        else if (other.TryGetComponent<Enemy>(out Enemy enemy))
            Died.Invoke();
    }

    private void PickCoin(Coin coin)
    {
        _wallet.ChangeAmountBy(coin.Value);
        Destroy(coin.gameObject);
    }

    private void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
}