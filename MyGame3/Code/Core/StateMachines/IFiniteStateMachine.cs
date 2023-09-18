using System.Collections.Generic;

namespace MagicAndMight.Code.Core.StateMachines;

public interface IFiniteStateMachine
{
	public Dictionary<int, IState> States { get; set; }
	public IState CurrentState { get; protected set; }

	public void UpdateState();

	public void AddState(int id, IState state);

	public void SetCurrentState(IState state);

	public void SetCurrentState(int stateId);
}