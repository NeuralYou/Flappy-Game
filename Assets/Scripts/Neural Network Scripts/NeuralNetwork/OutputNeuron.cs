using Newtonsoft.Json;
using System;

public class OutputNeuron
{
	[JsonIgnore] public Action m_Callback { get; set; }
	private float currentValue;

	public OutputNeuron()
	{

	}

	public OutputNeuron(Action i_Callback)
	{
		m_Callback = i_Callback;
	}

	public void RecieveInput(float i_InputVal)
	{
		currentValue += i_InputVal;
	}

	public void Activate(float i_Threshold)
	{
		if(currentValue > i_Threshold)
		{
			m_Callback();
		}
	}

	public void Reset()
	{
		currentValue = 0;
	}
}
