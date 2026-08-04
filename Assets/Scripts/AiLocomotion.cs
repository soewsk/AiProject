using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private NavMeshAgent _agent;
    [SerializeField] private float _maxTime;
    private float _timer;
    private Animator _animator;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _timer = _maxTime;
    }
    private void Update()
    {
        
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _agent.destination = _playerTransform.position;
        }

        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }
}
