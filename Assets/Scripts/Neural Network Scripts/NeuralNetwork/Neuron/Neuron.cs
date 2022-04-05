using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public abstract class Neuron
{
	protected float currentValue;
	[JsonProperty] protected float[] outputWeights;
	protected bool active;

	public Neuron(int i_SizeOfNextLayer)
	{
		outputWeights = new float[i_SizeOfNextLayer];

		for (int i = 0; i < outputWeights.Length; i++)
		{
			outputWeights[i] = Utils.RandomRange(-1f, 1f);
		}
	}


	public virtual void RecieveInput(float i_InputVal)
	{
		currentValue += i_InputVal;
	}

	public virtual void Activate(float i_Threshold)
	{
		currentValue = (float)Math.Tanh(currentValue);
		
		if(currentValue > i_Threshold) { active = true; }
		else { active = false; }
	}

	public void Reset()
	{
		currentValue = 0;
	}

	public void FeedForward(Neuron[] i_NextLayerNeurons)
	{
		for(int i = 0; i < i_NextLayerNeurons.Length; i++)
		{
			i_NextLayerNeurons[i].RecieveInput(currentValue * outputWeights[i]);
		}
	}

	public void Mutate(float i_MutationRate)
	{
		for (int i = 0; i < outputWeights.Length; i++)
		{
			if (Utils.RandomRange(0, 1f) <= i_MutationRate)
			{
				outputWeights[i] += Utils.RandomRange(-3f, 3f);
			}
		}
	}
}
