using UnityEngine;

[ExecuteInEditMode()]
[RequireComponent(typeof(Rigidbody))]
public class Damping : MonoBehaviour {
	[Range(0, 20)]
	[SerializeField] private float _naturalFrequency = 10;

	[Range(0, 3)]
	[SerializeField] private float _dampingRatio = 0.8f;

	[Range(-1, 1)]
	[SerializeField] private float _forceShift = 0.03f;

	[SerializeField] private bool _setSuspensionDistance = true;

	private Rigidbody _rigidbody;
	private WheelCollider[] _wheelCollider;

	private void Awake()
    {
		_rigidbody = GetComponent<Rigidbody>();
		_wheelCollider = GetComponentsInChildren<WheelCollider>();
    }

    private void FixedUpdate()
	{
		RelativeBody();
	}

	private void RelativeBody()
    {
		foreach (WheelCollider wheelCollider in _wheelCollider)
		{
			JointSpring spring = wheelCollider.suspensionSpring;

			spring.spring = Mathf.Pow(Mathf.Sqrt(wheelCollider.sprungMass) * _naturalFrequency, 2);
			spring.damper = 2 * _dampingRatio * Mathf.Sqrt(spring.spring * wheelCollider.sprungMass);

			wheelCollider.suspensionSpring = spring;

			Vector3 wheelRelativeBody = transform.InverseTransformPoint(wheelCollider.transform.position);
			float distance = _rigidbody.centerOfMass.y - wheelRelativeBody.y + wheelCollider.radius;

			wheelCollider.forceAppPointDistance = distance - _forceShift;

			if (spring.targetPosition > 0 && _setSuspensionDistance)
				wheelCollider.suspensionDistance = wheelCollider.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
		}
	}
}