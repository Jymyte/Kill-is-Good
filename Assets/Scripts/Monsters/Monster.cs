using System.Collections;
using System.Collections.Generic;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
  private StateMachine stateMachine;
  private NavMeshAgent agent;
  public ZombieCharacter character;
  public NavMeshAgent Agent { get => agent; }
  [SerializeField]
  private string currentState;
  public Pathway path;
  private GameObject player;
  public float sightDistance = 20f;
  public float fieldOfView = 85f;

  // Start is called before the first frame update
  void Start()
  {
    stateMachine = GetComponent<StateMachine>();
    agent = GetComponent<NavMeshAgent>();
    character = GetComponent<ZombieCharacter>();
    player = GameObject.FindGameObjectWithTag("Player"); 

    Debug.Log("yoo");
    stateMachine.Initialize();
  }

  // Update is called once per frame
  void Update()
  {
    CanSeePlayer();
  }

  public bool CanSeePlayer() {
    if (player != null) {
      //Is the player close enough to be seen?
      if (Vector3.Distance(transform.position, player.transform.position) < sightDistance) {
        Vector3 targetDirection = player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

        if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView) {
          Ray ray = new Ray(transform.position, targetDirection);
          Debug.DrawRay(ray.origin, ray.direction * sightDistance);    
        }
      }
    }
    return false;
  }
}
