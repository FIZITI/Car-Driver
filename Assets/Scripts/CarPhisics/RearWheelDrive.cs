using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class RearWheelDrive : MonoBehaviour {
	[SerializeField] private float _maxAngle = 30;
	[SerializeField] private float _maxTorque = 300;
	[SerializeField] private GameObject _wheelShape;
	private WheelCollider _wheel;
	private Transform _wheelTransform;

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
		float angle = _maxAngle * Input.GetAxis("Horizontal");
		float torque = _maxTorque * Input.GetAxis("Vertical");

		if (_wheelTransform.localPosition.z > 0)
			_wheel.steerAngle = angle;

		if (_wheelTransform.localPosition.z < 0)
			_wheel.motorTorque = torque;

		if (_wheelShape)
		{
			Quaternion rotation;
			Vector3 position;
			_wheel.GetWorldPose(out position, out rotation);

			Transform shapeTransform = _wheelTransform.GetChild(0);
			shapeTransform.position = position;
			shapeTransform.rotation = rotation;
			shapeTransform.localScale = new Vector3(1, 1, 1);
		}
	}
}