using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		GameObject[] birds = GameObject.FindGameObjectsWithTag("Player");

		scoring.text = $"Remaining: {birds.Length}" +
			$"\nFitness: {populationFitness.ToString("0.0")}" +
			$"\nGeneration: {generationCounter}" +
			$"\nTop Fitness:{maxFitness.ToString("0.0")}";

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

		StoreTop5Locally();
		TCPClient client = GetComponent<TCPClient>();
		client.SendMultipleNNs(elements,  initPopulation);
	}

	private void StoreTop5Locally()
	{
		List<NeuralNetwork> elems = new List<NeuralNetwork>(elements);
		elems.Sort();
		List<NeuralNetwork> list = new List<NeuralNetwork>();
		for (int i = elems.Count - 6; i < elems.Count; i++)
		{
			list.Add(elems[i]);
		}

		string path = Path.Combine(Application.dataPath);
		string full = Directory.CreateDirectory(Path.Combine(path, "Top_5s")).FullName;

		int numberOfFolders = Directory.GetDirectories(full).Length;
		string newPath = Directory.CreateDirectory(Path.Combine(full, numberOfFolders.ToString())).FullName;
		
		for(int i = 0; i < list.Count; i++)
		{

			string fileName = "network_" + i + ".json";
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(list[0], Newtonsoft.Json.Formatting.Indented);
			File.WriteAllText(Path.Combine(newPath, fileName), json);

		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Checkpoint"))
		{
			populationFitness += 1;
		}
	}
}

