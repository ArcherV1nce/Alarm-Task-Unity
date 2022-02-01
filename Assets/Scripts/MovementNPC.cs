using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovementNPC : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _idleTime = 2;
    [SerializeField] private bool _isMoving;
    [SerializeField] private int _currentTargetId;
    [SerializeField] private List<Vector3> _targetPositions;
    
    private Animator _animator;
    private bool _isTargerReached;
    private float _idleTimer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isMoving = false;
        _isTargerReached = false;
        _currentTargetId = 0;
    }

    private void Update ()
    {
        UpdateStatus();
    }

    private void OnValidate()
    {
        if (_speed < 0)
            _speed = -_speed;
        
        if (_currentTargetId < 0)
            _currentTargetId = 0;
    }

    private void Move (Vector3 direction)
    {
        float horizontalSpeed = 0f;

        if (_isTargerReached == false)
        {
            if (transform.position.x < direction.x)
            {
                horizontalSpeed = _speed * Time.deltaTime;
            }

            else if (transform.position.x > direction.x)
            {
                horizontalSpeed = -_speed * Time.deltaTime;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, direction, _speed * Time.deltaTime);
            _animator.SetFloat("HorizontalSpeed", horizontalSpeed);
        }

        else
        {
            _isMoving = false;
        }
    }

    private void UpdateStatus ()
    {
        if (_currentTargetId < _targetPositions.Count)
        {
            if (_isTargerReached == false)
            {
                if (transform.position == _targetPositions[_currentTargetId])
                {
                    _isTargerReached = true;
                    _idleTimer = _idleTime;
                    _currentTargetId++;
                    _isMoving = false;
                }
                
                else
                {
                    _isMoving = true;
                    Move(_targetPositions[_currentTargetId]);
                }
            }

            else
            {
                if (_idleTimer > 0)
                {
                    _idleTimer -= Time.deltaTime;
                }

                else
                {
                    _isMoving = true;
                    _isTargerReached = false;
                    Move(_targetPositions[_currentTargetId]);
                }
            }
        }

        _animator.SetBool("IsMoving", _isMoving);
    }
}