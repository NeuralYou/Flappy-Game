using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy : MonoBehaviour
{
	NeuralNetwork brain;
	System.Action deathCallback;
	[SerializeField] float flapStrength;
	PipeGeneration generator;
	bool canRun;

	public void Init(NeuralNetwork brain, System.Action deathCallback)
	{
		this.brain = brain;
		this.brain.SetCallbacks(Flap);
		this.deathCallback = deathCallback;
		generator = GameObject.FindGameObjectWithTag("Generator").GetComponent<PipeGeneration>();
		canRun = true;
	}

	private void Update()
	{
		if(canRun)
		{
			Vector2[] poses = generator.GetPipePositions();
			float[] inputs = new float[] { transform.position.y, poses[0].x, poses[0].y, poses[1].x, poses[1].y };
			brain?.FeedForward(inputs);
		}
	}
	public void Flap()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * flapStrength, ForceMode2D.Impulse);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Pipe"))
		{
			deathCallback();
			Destroy(this.gameObject);
		}
	}
}
