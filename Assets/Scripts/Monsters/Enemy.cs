using System.Collections;
using System.Collections.Generic;
using EasyCharacterMovement;
using EasyCharacterMovement.Examples.Events.CharacterEventsExample;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  private StateMachine stateMachine;
  private NavMeshAgent agent;
  private GameObject player;
  private KillIsGoodCharacter playerCharacter;
  private Vector3 lastKnownPos;
  private Vector3 lastKnownDir;
  public ZombieCharacter character;
  public NavMeshAgent Agent { get => agent; }
  public GameObject Player { get => player; }
  public KillIsGoodCharacter PlayerCharacter {get => playerCharacter; }
  public Vector3 LastKnownPos { get => lastKnownPos; set => lastKnownPos = value; }
  public Vector3 LastKnownDir { get => lastKnownDir; set => lastKnownDir = value; }
  public Pathway path;
  [Header("Sight values")]
  public float sightDistance = 20f;
  public float fieldOfView = 85f;
  public float eyeHeight;

  [SerializeField]
  public GameObject debugSphereLastPos;
   [SerializeField]
  public GameObject debugSphereSearchPos;

  [Header("Weapon values")]
  [SerializeField]
  public Transform gunBarrel;
 
 [Range(1f, 10f)]
  public float fireRate;

  //For debugging
  [SerializeField]
  private string currentState;

  // Start is called before the first frame update
  void Start()
  {
    stateMachine = GetComponent<StateMachine>();
    agent = GetComponent<NavMeshAgent>();
    character = GetComponent<ZombieCharacter>();
    player = GameObject.FindGameObjectWithTag("Player"); 
    playerCharacter = player.GetComponent<KillIsGoodCharacter>();

    Debug.Log("yoo");
    stateMachine.Initialize();
  }

  // Update is called once per frame
  void Update()
  {
    CanSeePlayer();

    currentState = stateMachine.activeState.ToString();
  }

  public bool CanSeePlayer() {
    if (player != null) {
      //Is the player close enough to be seen?
      if (Vector3.Distance(transform.position, player.transform.position) < sightDistance) {
        Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
        float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

        if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView) {
          Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection + (Vector3.up * 1f));
          RaycastHit hitInfo = new RaycastHit();
          if (Physics.Raycast(ray, out hitInfo, sightDistance)) {
            if( hitInfo.transform.gameObject == player) {
              Debug.DrawRay(ray.origin, ray.direction * sightDistance); 
              return true;
            }
          }
        }
      }
    }
    return false;
  }
}
