using UnityEngine;

public class ShapeRotate : MonoBehaviour
{
    [SerializeField] private WheelCollider _wheel;
    [SerializeField] private GameObject _wheelShape;

    private void Update()
    {
        WheelShapeRotate();
    }

    private void WheelShapeRotate()
    {
        if (_wheelShape)
        {
            _wheel.GetWorldPose(out Vector3 position, out Quaternion rotation);

            _wheelShape.transform.SetPositionAndRotation(position, rotation);
        }
    }
}