using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value {get; private set;}

    private void Awake()
    {
        value = 1;
    }
}
