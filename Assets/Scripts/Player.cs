using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IControllable
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _increaseSpeed;
    [SerializeField] private float _delayIncreaseSpeed;
    [SerializeField] private float _turningSpeed;
    [SerializeField] private Rigidbody _rigidbody;

    private Vector3 _direction;
    private Quaternion _rotation;
    private bool _isAccelerationUp = false;
    private bool _isAccelerationDown = false;
    private float _realSpeed = 0;

    private void Start()
    {

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var direction = transform.right;

        _rigidbody.velocity = new Vector3(direction.x * _realSpeed, _rigidbody.velocity.y, direction.z * _realSpeed);

        transform.Rotate(0, _direction.x * _turningSpeed * Time.deltaTime, 0);

        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }

        if (!_isAccelerationUp && _direction.z != 0)
        {
            StopCoroutine(AccelerationDown());
            StartCoroutine(AccelerationUp());
        }
        else if(!_isAccelerationDown && !_isAccelerationUp)
        {
            StopCoroutine(AccelerationUp());
            StartCoroutine(AccelerationDown());
        }

        Debug.Log(_realSpeed);
    }

    private IEnumerator AccelerationUp()
    {
        while (_direction.z != 0)
        {
            _isAccelerationUp = true;

            if (_direction.z > 0 && _realSpeed + _increaseSpeed <= _maxSpeed)
            {
                _realSpeed += _increaseSpeed;
            }
            else if (_realSpeed - _increaseSpeed >= -_maxSpeed)
            {
                _realSpeed -= _increaseSpeed;
            }

            yield return new WaitForSeconds(_delayIncreaseSpeed);
            _isAccelerationUp = false;
        }
    }

    private IEnumerator AccelerationDown()
    {
        _isAccelerationDown = true;

        if (_realSpeed - _increaseSpeed >= 0)
        {
            _realSpeed -= _increaseSpeed;
        }
        else if (_realSpeed + _increaseSpeed <= 0)
        {
            _realSpeed += _increaseSpeed;
        }

        yield return new WaitForSeconds(_delayIncreaseSpeed);

        _isAccelerationDown = false;
    }

    public void Move(Vector3 direction)
    {
        _direction = direction;
/*        Debug.Log(_direction);*/
    }
}