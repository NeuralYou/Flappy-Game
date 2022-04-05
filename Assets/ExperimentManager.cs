using System.Collections;
using TMPro;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	NeuralNetwork[] elements;
	[SerializeField] PipeGeneration generator;
	[SerializeField] GameObject flappy;
	[SerializeField] Vector2 startingPosition;
	[SerializeField] TextMeshProUGUI scoring;
	[SerializeField] int deathCount;
	float populationFitness;
	float maxFitness;
	int generationCounter;

	public void Start()
	{
		Application.runInBackground = true;
		TCPClient client = GetComponent<TCPClient>();
		client.InitConnection(initPopulation);
	}

	private void Update()
	{
		//populationFitness += 0.1f;
		scoring.text = $"Fitness: {populationFitness.ToString("0.0")}\nGeneration: {generationCounter}\n Top Fitness:{maxFitness.ToString("0.0")}";

		GameObject[] birds = GameObject.FindGameObjectsWithTag("Player");
		if(birds.Length == 0)
		{
			onFlappyDeath();
		}
	}

	private void initPopulation(NeuralNetwork[] elements)
	{
		this.elements = elements;
		for (int i = 0; i < elements.Length; i++)
		{

			GameObject bird = Instantiate(flappy, startingPosition, Quaternion.identity);
			bird.GetComponent<Flappy>().Init(elements[i], onFlappyDeath);
		}
		GameObject[] flappys = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject f in flappys)
		{
		}
	}

	private void onFlappyDeath()
	{
		generationCounter++;

		if (populationFitness > maxFitness)
			maxFitness = populationFitness;

		populationFitness = 0;
		deathCount = 0;
		generator.ResetGenerator();
		TCPClient client = GetComponent<TCPClient>();
		client.SendMultipleNNs(elements,  initPopulation);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Checkpoint"))
		{
			populationFitness += 1;
		}
	}
}

