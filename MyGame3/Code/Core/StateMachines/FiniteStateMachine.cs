using System.Collections.Generic;

namespace MagicAndMight.Code.Core.StateMachines;

public abstract class FiniteStateMachine : StartupScript, IFiniteStateMachine
{
	public Dictionary<int, IState> States { get; set; }
	public IState CurrentState { get; set; }

	public void AddState(int id, IState state)
	{
		States.Add(id, state);
	}

	public void SetCurrentState(IState state)
	{
		CurrentState?.ExitState();

		CurrentState = state;

		CurrentState?.EnterState();
	}

	public void SetCurrentState(int stateId)
	{
		CurrentState?.ExitState();

		CurrentState = States[stateId];

		CurrentState?.EnterState();
	}

	public virtual void UpdateState()
	{
		CurrentState?.UpdateState();
	}
}