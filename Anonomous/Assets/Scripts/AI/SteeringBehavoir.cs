using UnityEngine;
using System.Collections;

public class SteeringBehavoir {

	public enum Steering
	{
		Flee, Chase, Wander, Idle
	};

	Steering steering;
	private Entity m_Entity;
	private float m_speed;
	private Vector3 m_vTarget;
	private Vector3 m_minBorder;
	private Vector3 m_maxBorder;
	private float m_zOffset;
	private float m_targetAngle;

	public SteeringBehavoir(Entity Entity, Vector3 minBorder, Vector3 maxBorder, float zOffset)
	{
		m_Entity = Entity;
		m_zOffset = zOffset;
		m_speed = 3;
		m_minBorder = minBorder;
		m_maxBorder = maxBorder;
		m_vTarget = getRandomVector();
	}

	public void UpdateSteering()
	{
		switch (steering) {
		case Steering.Chase:
			Chase ();
			break;
		case Steering.Flee:
			Flee ();
			break;
		case Steering.Idle:
			Idle ();
			break;
		case Steering.Wander:
			Wander ();
			break;
		}
	}

	/**Steering Behavoirs*/
	private void Chase()
	{
		float rotationSpeed = 1;
		Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.localPosition;

		playerPos.z = m_zOffset;
		//		m_Entity.transform.rotation = Quaternion.Slerp(m_Entity.transform.rotation, Quaternion.LookRotation(playerPos - m_Entity.transform.position), rotationSpeed * Time.deltaTime);

		m_Entity.transform.position = Vector3.MoveTowards( m_Entity.transform.position, playerPos, Time.deltaTime * m_speed);

	}
	private void Wander()
	{
		float rotationSpeed = 100;

		float disToTarget = Vector3.Distance(m_Entity.transform.position,m_vTarget);
		if (disToTarget < 7f) {
			m_vTarget = getRandomVector (); 
			m_Entity.transform.position = Vector3.MoveTowards( m_Entity.transform.position, m_vTarget, Time.deltaTime * m_speed);

			m_targetAngle = Vector3.Angle (m_vTarget - m_Entity.transform.position, m_Entity.transform.up);
			m_targetAngle = m_vTarget.x > m_Entity.transform.position.x ? 360 - m_targetAngle : m_targetAngle;

			m_Entity.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, m_targetAngle));// Quaternion.Euler(0f, 0f, ); // (Vector3.Angle(m_vTarget,m_Entity.transform.position) * Mathf.Sign(Vector3.Dot(new Vector3(0f, 0f, 1f),Vector3.Cross(m_vTarget,m_Entity.transform.position))) + 180) % 360);

		}
		m_Entity.transform.position = Vector3.MoveTowards( m_Entity.transform.position, m_vTarget, Time.deltaTime * m_speed);
		m_Entity.transform.rotation = Quaternion.Slerp(m_Entity.transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, m_targetAngle)), rotationSpeed * Time.deltaTime);

	}
	private void Idle()
	{
		m_Entity.GetComponent<Rigidbody>().velocity = new Vector3(0,0,m_zOffset);
	}
	private void Flee()
	{
	}

	/**Setters*/

	public void SetSpeed(float speed)
	{
		m_speed = speed;
	}

	public void SetSteering(Steering steeringType){
		steering = steeringType;
	}

	private Vector3 getRandomVector()
	{
		Vector3 rand = new Vector3(Random.Range(m_minBorder.x, m_maxBorder.x), Random.Range(m_minBorder.y, m_maxBorder.y));

		rand.z = m_zOffset;

		return rand;
	}
	public void SetBounds(Vector3 lower, Vector3 upper)
	{
		m_minBorder = lower;
		m_maxBorder = upper;
	}
}
