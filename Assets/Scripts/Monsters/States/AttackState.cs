using UnityEngine;

public class AttackState : BaseState
{
  private float moveTimer;
  private float losePlayerTimer;
  private float shotTimer;
  private GameObject projectile;

  public override void Enter()
  {
    projectile = Resources.Load("Prefabs/Bullet") as GameObject;
    Debug.Log("paukku: ", projectile);
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
      enemy.LastKnownPos = enemy.Player.transform.position;
      enemy.debugSphereLastPos.transform.position = enemy.LastKnownPos;
      enemy.LastKnownDir = enemy.PlayerCharacter.GetMovementDirection();
    } else {
      losePlayerTimer += Time.deltaTime;
      if (losePlayerTimer > 8) {
        //Change to search state
        stateMachine.ChangeState(new SearchState());
      }
    }
  }

  public void Shoot() {
    //refence to gun barrell
    Transform gunBarrel = enemy.gunBarrel;

    //instiating bullet
    GameObject bullet = GameObject.Instantiate(projectile, gunBarrel.position, enemy.transform.rotation);

    //Bullet direction and force
    Vector3 shootDirection = (enemy.Player.transform.position  + (Vector3.up * 1f) - gunBarrel.transform.position).normalized;
    bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 40f;

    Debug.Log("shoot");
    shotTimer = 0;
  }
}
