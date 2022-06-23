using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _coins;

    private void Awake()
    {
        _coins = 0;
    }

    public void ChangeAmountBy(int value)
    {
        _coins += value;
    }
}
