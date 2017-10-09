using UnityEngine;
using System.Collections;

public class StateMachine {

	private Entity          m_Owner;
	private State			   m_CurrentState;

	//a record of the last state the agent was in
	private State			   m_PreviousState;

	//this is called every time the FSM is updated
	private State			   m_GlobalState;


	// Use this for initialization
	void Start () {

	}

	public StateMachine(Entity owner)
	{
		m_Owner = owner;
	}
	public void SetCurrentState(State s)
	{ 
		m_CurrentState = s; 
		m_CurrentState.Enter(m_Owner);
	}
	public void SetGlobalState(State s) 
	{ 
		m_GlobalState = s; 
	}
	public void SetPreviousState(State s)
	{ 
		m_PreviousState = s; 
	}

	// Update is called once per frame
	public void UpdateState () 
	{
		//if a global state exists, call its execute method, else do nothing
		m_GlobalState.Execute(m_Owner);

		//same for the current state
		m_CurrentState.Execute(m_Owner);
	}

	//change to a new state
	public void  ChangeState(State newState)
	{
		//assert(pNewState && "<StateMachine::ChangeState>:trying to assign null state to current");

		//keep a record of the previous state
		m_PreviousState = m_CurrentState;

		//call the exit method of the existing state
		m_CurrentState.Exit(m_Owner);

		//change state to the new state
		m_CurrentState = newState;

		//call the entry method of the new state
		m_CurrentState.Enter(m_Owner);
	}

	//change state back to the previous state
	void  RevertToPreviousState()
	{
		ChangeState(m_PreviousState);
	}


	public State  CurrentState()  { return m_CurrentState; }
	public State  GlobalState()   { return m_GlobalState; }
	public State  PreviousState() { return m_PreviousState; }

}
