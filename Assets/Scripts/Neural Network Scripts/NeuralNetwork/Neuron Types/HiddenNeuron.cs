using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

[System.Serializable]
public class HiddenNeuron: Neuron
{
	[JsonIgnore] public float CurrentValue { get; private set; }

	//For construction of an input\hidden neuron.
	public HiddenNeuron(int i_SizeOfNextLayer) : base(i_SizeOfNextLayer)
	{

	}

	public void FeedForward(OutputNeuron[] i_OutputLayer)
	{
		for(int i = 0; i < i_OutputLayer.Length; i++)
		{
			i_OutputLayer[i].RecieveInput(outputWeights[i] * currentValue);
		}
	}
}
