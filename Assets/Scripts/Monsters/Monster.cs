using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
  private StateMachine stateMachine;
  private NavMeshAgent agent;
  public NavMeshAgent Agent { get => agent; }
  [SerializeField]
  private string currentState;
  public Pathway path;

  // Start is called before the first frame update
  void Start()
  {
    stateMachine = GetComponent<StateMachine>();
    agent = GetComponent<NavMeshAgent>();
    Debug.Log("yoo");
    stateMachine.Initialize();
  }

  // Update is called once per frame
  void Update()
  {
    
  }
}
