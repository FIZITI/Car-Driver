using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _positionOffset;
    private Vector3 _newPosition;

    private void LateUpdate()
    {
        if (_target != null)
        {
            _newPosition.x = _target.transform.position.x - _positionOffset;
            _newPosition.y = _target.transform.position.y - _positionOffset;
            _newPosition.z = _target.transform.position.z;
            transform.position = _newPosition;
        }
    }
}