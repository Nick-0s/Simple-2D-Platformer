using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private int _coins;

    private void Awake()
    {
        _coins = 0;
        DrawScore();
    }

    public void ChangeAmountBy(int value)
    {
        _coins += value;
        DrawScore();
    }

    private void DrawScore()
    {
        _text.text = _coins.ToString();
    }
}
