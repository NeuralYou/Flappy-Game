using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public delegate void NNCallback(NeuralNetwork[] elements);
public class TCPClient : MonoBehaviour
{
	private string ip;

	private void Awake()
	{
		//ip = "54.74.9.169";
		ip = "127.0.0.1";
	}
	public void SendMultipleNNs(NeuralNetwork[] elements, NNCallback callback)
	{
		TcpClient client = new TcpClient(ip, 1234);
		if (client.Connected)
		{
			NetworkStream stream = client.GetStream();
		try
		{

			NetworkUtils.WriteInt(stream, 1);
			NetworkUtils.WriteInt(stream, elements.Length);

			for(int i = 0; i < elements.Length; i++)
			{
				NetworkUtils.WriteNN(stream, elements[i]);
	
			}

			List<string> stringReps = new List<string>();

			StartCoroutine(GetAnswer(stream, callback));

		}
		catch(Exception)
			{
				stream.Close();
			}
		}
	}

	public IEnumerator GetAnswer(NetworkStream i_Stream, NNCallback callback)
	{
		while (!i_Stream.CanRead) { yield return null; }

		List<NeuralNetwork> list = new List<NeuralNetwork>();

		int size = NetworkUtils.ReadInt(i_Stream);

		for (int i = 0; i < size; i++)
		{
			int currentSize = NetworkUtils.ReadInt(i_Stream);
			NeuralNetwork n = NetworkUtils.ReadNN(i_Stream, currentSize);
			list.Add(n);
			yield return null;
		}

		print("Got all networks");

		callback(list.ToArray());
	}

	public void InitConnection(NNCallback callback)
	{
		TcpClient client = new TcpClient(ip, 1234);
		if(client.Connected)
		{
			NetworkStream stream = client.GetStream();
			NetworkUtils.WriteInt(stream, 0);

			StartCoroutine(GetAnswer(stream, callback));
		}
	}
}
