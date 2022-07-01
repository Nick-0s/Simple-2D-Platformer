using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AnimatorPlayerController : MonoBehaviour
{
    public const string SpeedX = "SpeedX";
    public const string IsOnGround = "IsOnGround";

    private Animator _animator;
    private bool _isFacedToTheRight;

    private void Awake()
    {
        _animator = GetComponent<Animator>();    
        _isFacedToTheRight = true;
    }

    private void ReflectPlayer(float offsetAlongX)
    {
        if (offsetAlongX > 0 && _isFacedToTheRight == false || offsetAlongX < 0 && _isFacedToTheRight)
        {
            Vector2 vectorToReflectPlayer = new Vector2(-1, 1);

            transform.localScale *= vectorToReflectPlayer;
            _isFacedToTheRight = !(_isFacedToTheRight);
        }
    }

    public void Run(float offsetAlongX)
    {
        _animator.SetFloat(AnimatorPlayerController.SpeedX, Mathf.Abs(offsetAlongX));
        ReflectPlayer(offsetAlongX);
    }

    public void Jump(bool isGrounded)
    {
        _animator.SetBool(AnimatorPlayerController.IsOnGround, isGrounded);
    }
}