using UnityEngine;

[ExecuteInEditMode()]
[RequireComponent(typeof(Rigidbody))]
public class EasySuspension : MonoBehaviour {
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

/*		Debug.Log(_rigidbody.velocity.magnitude);*/
	}

	private void RelativeBody()
    {
		foreach (WheelCollider wc in _wheelCollider)
		{
			JointSpring spring = wc.suspensionSpring;

			spring.spring = Mathf.Pow(Mathf.Sqrt(wc.sprungMass) * _naturalFrequency, 2);
			spring.damper = 2 * _dampingRatio * Mathf.Sqrt(spring.spring * wc.sprungMass);

			wc.suspensionSpring = spring;

			Vector3 wheelRelativeBody = transform.InverseTransformPoint(wc.transform.position);
			float distance = _rigidbody.centerOfMass.y - wheelRelativeBody.y + wc.radius;

			wc.forceAppPointDistance = distance - _forceShift;

			if (spring.targetPosition > 0 && _setSuspensionDistance)
				wc.suspensionDistance = wc.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
		}
	}
}