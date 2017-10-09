using UnityEngine;
using System.Collections;

public abstract class State {

	//this will execute when the state is entered
	public abstract void Enter(Entity GameEntity);

	//this is the states normal update function
	public abstract void Execute(Entity GameEntity);


	//this will execute when the state is exited.
	public abstract void Exit(Entity GameEntity);


}