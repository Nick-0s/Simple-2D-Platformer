using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value {get; private set;} = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        
            Debug.Log($"Trigger: {other.name}");
    }
}