using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class RearWheelDrive : MonoBehaviour {
	[SerializeField] private float maxAngle = 30;
	[SerializeField] private float maxTorque = 300;
	[SerializeField] private GameObject wheelShape;
	private WheelCollider[] wheels;

    private void Awake()
    {
		wheels = GetComponentsInChildren<WheelCollider>();
	}

	public void Update()
	{
		float angle = maxAngle * Input.GetAxis("Horizontal");
		float torque = maxTorque * Input.GetAxis("Vertical");

		foreach (WheelCollider wheel in wheels)
		{
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
				wheel.motorTorque = torque;

			if (wheelShape) 
			{
				Quaternion rotation;
				Vector3 position;
				wheel.GetWorldPose (out position, out rotation);

				Transform shapeTransform = wheel.transform.GetChild(0);
				shapeTransform.position = position;
				shapeTransform.rotation = rotation;
				shapeTransform.localScale = new Vector3(1, 1, 1);
			}
		}
	}
}