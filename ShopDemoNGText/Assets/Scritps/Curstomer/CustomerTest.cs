using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerTest : MonoBehaviour
{
    public Transform target;
    public float angle = 60f;
    public float distance = 5f;
    private NavMeshAgent agent;
    GameObject _PPD;
    // Use this for initialization
    void Awake()
    {
        _PPD = GameObject.Find("PDD");
        Debug.Log(_PPD.name);
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(_PPD.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 direction = target.position - transform.position;
            if (Vector3.Angle(direction, transform.forward) < angle)
            {
                if (Vector3.Distance(target.position, transform.position) < distance)
                {
                    Debug.Log("我看见你了");
                }
            }
        }
    }
