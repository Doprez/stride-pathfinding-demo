namespace MagicAndMight.Code.Core.StateMachines;

public interface IState
{
	public string Name { get; set; }
	public IFiniteStateMachine FiniteStateMachine { get; set; }
	public bool IsDefaultState { get; set; }

	public void EnterState();

	public void ExitState();

	public void UpdateState();
}