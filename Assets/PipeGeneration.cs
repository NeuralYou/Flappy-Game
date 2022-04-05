using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeGeneration : MonoBehaviour
{
	[SerializeField] GameObject Pipe;
	[SerializeField] GameObject checkpoint;
	[SerializeField] float startingX;
	[SerializeField] int gapSize;

	RandomNumbers random;
	List<GameObject[]> pairs;
	[SerializeField] float flappyX;
	int randomNumberIndex;

	private void Start()
	{
		pairs = new List<GameObject[]>();
		random = GetComponent<RandomNumbers>();
		StartCoroutine(Generate());
	}

	private void Update()
	{

		if (pairs.Count > 0)
		{
			GameObject[] first = pairs.First();
			if(first[0].transform.position.x < flappyX)
			{
				pairs.RemoveAt(0);
			}
		}
	}

	public void CreatePair()
	{
		float upperHeight = random.GetRandomSequence()[(randomNumberIndex++) % 100];
		GameObject upper = Instantiate(Pipe, new Vector2(startingX, upperHeight), Quaternion.identity);
		upper.GetComponent<Pipe>().Init(Direction.DOWNWARDS, 20);

		GameObject bottom = Instantiate(Pipe, new Vector2(startingX, upperHeight - gapSize), Quaternion.identity);
		bottom.GetComponent<Pipe>().Init(Direction.UPWARDS, 20);

		float checkpointY = (upper.transform.position.y + bottom.transform.position.y) / 2f;
		GameObject checkpointCollider = Instantiate(checkpoint, new Vector2(startingX, checkpointY), Quaternion.identity);

		pairs.Add(new GameObject[] { upper, bottom });
	}

	public IEnumerator Generate()
	{
		while(true)
		{
			CreatePair();
			yield return new WaitForSeconds(3f);
		}
	}

	public Vector2[] GetPipePositions()
	{
		GameObject[] last = pairs.First();
		return new Vector2[] { last[0].transform.position, last[1].transform.position };
	}

	public void ResetGenerator()
	{
		randomNumberIndex = 0;

		GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
		foreach(GameObject p in pipes)
		{
			Destroy(p);
		}
		foreach(GameObject[] pair in pairs)
		{
			Destroy(pair[0]);
			Destroy(pair[1]);
		}

		GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
		foreach(GameObject c in checkpoints)
		{
			Destroy(c);
		}

		StopAllCoroutines();
		pairs.Clear();
		StartCoroutine(Generate());
	}
}
