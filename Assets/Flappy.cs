using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy : MonoBehaviour
{
	[SerializeField] NeuralNetwork brain;
	[SerializeField] float flapStrength;
	PipeGeneration generator;
	bool canRun;
	public float fitness;

	public void Init(NeuralNetwork brain)
	{
		this.brain = brain;
		this.brain.SetCallbacks(Flap);
		generator = GameObject.FindGameObjectWithTag("Generator").GetComponent<PipeGeneration>();
		canRun = true;
	}

	private void Update()
	{

		//ManualControls!!
		//if(Input.GetKeyDown(KeyCode.Space))
		//{
		//	Flap();
		//}

		if (canRun)
		{
			Vector2[] poses = generator.GetPipePositions();
			float[] inputs = new float[] { transform.position.y, poses[0].x, poses[0].y, poses[1].x, poses[1].y };
			brain?.FeedForward(inputs);
			//fitness += 0.1f;
		}
	}
	public void Flap()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * flapStrength, ForceMode2D.Impulse);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Pipe") || collision.collider.CompareTag("Floor"))
		{
			brain.Fitness = fitness;
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Checkpoint"))
		{
			fitness += 1;
		}
	}
}
