 public abstract class BaseState
{
  public Monster monster;
  public StateMachine stateMachine;

  public abstract void Enter();
  public abstract void Perform();
  public abstract void Exit();

}
