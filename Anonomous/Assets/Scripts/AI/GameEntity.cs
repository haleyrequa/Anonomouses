using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

	public abstract float siteDepth { get; }
	public abstract float cruisinSpeed { get; }

	protected StateMachine stateMachine;
	protected SteeringBehavoir steeringBehavior;

	public StateMachine GetFSM() { return stateMachine; }
	public SteeringBehavoir GetSB() { return steeringBehavior; }
}
