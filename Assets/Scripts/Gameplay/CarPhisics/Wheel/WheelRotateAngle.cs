using UnityEngine;

public class WheelRotateAngle : MonoBehaviour, IControllable
{
    [SerializeField] private WheelCollider _wheel;
    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private float _maxAngle;
    private Vector3 _moveDirection;

    private void Update()
    {
        RotateAngle();
    }

    private void RotateAngle()
    {
        _wheel.steerAngle = _moveDirection.x * _maxAngle;
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }
}