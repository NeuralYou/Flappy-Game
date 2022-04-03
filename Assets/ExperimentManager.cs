using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	NeuralNetwork[] elements;
	[SerializeField] PipeGeneration generator;
	[SerializeField] GameObject flappy;
	[SerializeField] Vector2 startingPosition;
	int deathCount;

	public void Start()
	{
		Application.runInBackground = true;
		TCPClient client = GetComponent<TCPClient>();
		client.InitConnection(initPopulation);
	}

	private void initPopulation(NeuralNetwork[] elements)
	{
		this.elements = elements;
		foreach(NeuralNetwork n in elements)
		{
			GameObject bird = Instantiate(flappy, startingPosition, Quaternion.identity);
			bird.GetComponent<Flappy>().Init(n, onFlappyDeath);
		}
	}

	private void onFlappyDeath()
	{
		deathCount++;
		if(deathCount >= elements.Length)
		{
			deathCount = 0;
			generator.ResetGenerator();
			TCPClient client = GetComponent<TCPClient>();
			client.SendMultipleNNs(elements, initPopulation);
		}
	}
}

