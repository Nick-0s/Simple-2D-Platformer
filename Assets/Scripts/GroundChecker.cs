using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] LayerMask _playerLayer;

    private LayerMask _layerMask;
    private float _raycastDistance;

    private void Awake()
    {
        float raycastDistanceExtension = 0.05f;
        _layerMask = ~(_playerLayer);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, _layerMask);
        _raycastDistance = hit.distance + raycastDistanceExtension;
    }

    public bool GetStatus()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance, _layerMask);
    }
}
