using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _centralObject;
    [SerializeField] private float _positionOffsetX;
    [SerializeField] private float _positionOffsetY;
    [SerializeField] private float _rotationSpeed;
    private Vector3 _newPosition;
    private Transform _thisTransform;
    private Transform _centralObjectTransform;

    private void Awake()
    {
        _centralObjectTransform = _centralObject.transform;
        _thisTransform = transform;
    }

    private void Start()
    {
        if (_target != null)
        {
            SetCameraPosition();
        }

        Cursor.visible = false;
    }

    private void Update()
    {
        SetCameraRotation();
    }

    private void SetCameraPosition()
    {
        _newPosition.x = _centralObjectTransform.position.x + _positionOffsetX;
        _newPosition.y = _centralObjectTransform.position.y + _positionOffsetY;
        _newPosition.z = _centralObjectTransform.position.z;
        _thisTransform.position = _newPosition;
    }

    private void SetCameraRotation()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
        float verticalRotation = -Input.GetAxis("Mouse Y") * _rotationSpeed * Time.deltaTime;

        _centralObjectTransform.rotation = Quaternion.Euler(0f, _centralObjectTransform.eulerAngles.y + horizontalRotation, _centralObjectTransform.eulerAngles.z + verticalRotation);
    }
}