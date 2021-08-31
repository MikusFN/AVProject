using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class following : MonoBehaviour {
    NavMeshAgent enemy;
    public Transform target;
    // Use this for initialization
    void Start () {
        enemy = GetComponentInChildren<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        enemy.SetDestination(target.position);
	}
}
