using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	NeuralNetwork[] elements;

	public void Start()
	{
		TCPClient client = GetComponent<TCPClient>();
		client.InitConnection(initPopulation);
	}


	private void initPopulation(NeuralNetwork[] elements)
	{
		this.elements = elements;
		foreach(NeuralNetwork n in elements)
		{

		}
	}
}

