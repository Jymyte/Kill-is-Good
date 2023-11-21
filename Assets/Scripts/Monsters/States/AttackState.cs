using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
  private float moveTimer;
  private float losePlayerTimer;
  private float shotTimer;

  public override void Enter()
  {
    
  }

  public override void Exit()
  {
    
  }

  public override void Perform()
  {
    if (enemy.CanSeePlayer()) {
      losePlayerTimer = 0;
      moveTimer += Time.deltaTime;
      shotTimer += Time.deltaTime;
      enemy.transform.LookAt(enemy.Player.transform);
      if (shotTimer > enemy.fireRate) {
        Shoot();
      }
      if (moveTimer > Random.Range(3,7)) {
        enemy.character.MoveToLocation(enemy.transform.position + (Random.insideUnitSphere * 5));
        moveTimer = 0;
      }
    } else {
      losePlayerTimer += Time.deltaTime;
      if (losePlayerTimer > 8) {
        //Change to search state
        stateMachine.ChangeState(new PatrolState());
      }
    }
  }

  public void Shoot() {
    //refence to gun barrell
    Transform gunBarrel = enemy.gunBarrel;

    //instiating bullet
    GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, enemy.transform.rotation);

    //Bullet direction and force
    Vector3 shootDirection = (enemy.Player.transform.position  + (Vector3.up * 1f) - gunBarrel.transform.position).normalized;
    bullet.GetComponent<Rigidbody>().velocity = shootDirection * 40f;

    Debug.Log("shoot");
    shotTimer = 0;
  }

  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
