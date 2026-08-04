using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
    
public class CharacterAi : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private AIState _currentstate;
    private float _timer;
    private Transform _targetObj;


    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _currentstate = AIState.Idle;
    }
    public void Update()
    {
        switch (_currentstate)
        {
            case AIState.Idle:
                Idle();
                break;

            case AIState.Search:
                Search();
                break;

            case AIState.Collect:
                Collect();
                break;
        }
    }

    public enum AIState
    {
        Idle,
        Search,
        Collect
    }
    public void Idle()
    {
        _timer += Time.deltaTime;
        if (_timer >= 5f)
        {
            _currentstate = AIState.Search; 
             _timer = 0;
        }
    }
    public void Search()
    {
        if (_agent.pathPending)
        {
            return;
        }
        if (_agent.remainingDistance <= 0.2f)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomZ = Random.Range(-10f, 10f);
            Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);


            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 20f, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
               
            }
          
        
        
        }
        Collider[] _foundItem = Physics.OverlapSphere(_agent.transform.position, 5f);
        foreach (Collider item in _foundItem)
        {
            if (item.CompareTag("Collectable"))
            {
                _targetObj = item.transform;
                _currentstate = AIState.Collect;
                break;
            }
        }

    }
    public void Collect()
    {
        if(_targetObj == null)
        {
            _currentstate= AIState.Idle;
          return;
        }
      
      _agent.SetDestination(_targetObj.position);
        if (_agent.pathPending)
        {
            return;
        }
        if (_agent.remainingDistance <= 0.2f)
        {
            Destroy(_targetObj.gameObject);
            _timer = 0; 
            _targetObj = null;
            _currentstate = AIState.Idle;
        }



    }















}
