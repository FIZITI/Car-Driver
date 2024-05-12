using UnityEngine;

[ExecuteInEditMode()]
[RequireComponent(typeof(Rigidbody))]
public class EasySuspension : MonoBehaviour {
	[Range(0, 20)]
	[SerializeField] private float naturalFrequency = 10;
	[Range(0, 3)]
	[SerializeField] private float dampingRatio = 0.8f;
	[Range(-1, 1)]
	[SerializeField] private float forceShift = 0.03f;
	[SerializeField] private bool setSuspensionDistance = true;
	private Rigidbody _rigidbody;
	private WheelCollider[] _wheelCollider;

	private void Awake()
    {
		_rigidbody = GetComponent<Rigidbody>();
		_wheelCollider = GetComponentsInChildren<WheelCollider>();
    }

    private void FixedUpdate() {
		foreach (WheelCollider wc in _wheelCollider) {
			JointSpring spring = wc.suspensionSpring;

			spring.spring = Mathf.Pow(Mathf.Sqrt(wc.sprungMass) * naturalFrequency, 2);
			spring.damper = 2 * dampingRatio * Mathf.Sqrt(spring.spring * wc.sprungMass);

			wc.suspensionSpring = spring;

			Vector3 wheelRelativeBody = transform.InverseTransformPoint(wc.transform.position);
			float distance = _rigidbody.centerOfMass.y - wheelRelativeBody.y + wc.radius;

			wc.forceAppPointDistance = distance - forceShift;

			if (spring.targetPosition > 0 && setSuspensionDistance)
				wc.suspensionDistance = wc.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
		}
	}
}
