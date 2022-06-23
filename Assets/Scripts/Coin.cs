using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value {get; private set;}

    private void Awake()
    {
        Value = 1;
    }
}