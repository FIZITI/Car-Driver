using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class RearWheelDrive : MonoBehaviour, IControllable {
	[SerializeField] private float _maxAngle = 30;
	[SerializeField] private float _maxTorque = 300;
	[SerializeField] private GameObject _wheelShape;
	private WheelCollider _wheel;
	private Transform _wheelTransform;
	private Vector3 _moveDirection;

    private void Awake()
    {
		_wheel = GetComponent<WheelCollider>();
		_wheelTransform = GetComponent<Transform>();
	}

	private void Update()
	{
		WheelDrive();
	}

	public void WheelDrive()
    {
		if (_wheelTransform.localPosition.z > 0)
			_wheel.steerAngle = _moveDirection.x * _maxAngle;

		if (_wheelTransform.localPosition.z < 0)
			_wheel.motorTorque = _moveDirection.z * _maxTorque;

		if (_wheelShape)
		{
            _wheel.GetWorldPose(out Vector3 position, out Quaternion rotation);

            Transform shapeTransform = _wheelTransform.GetChild(0);
			shapeTransform.SetPositionAndRotation(position, rotation);
			shapeTransform.localScale = new Vector3(1, 1, 1);
		}
    }

    public void Move(Vector3 direction)
    {
		_moveDirection = direction;
    }
}