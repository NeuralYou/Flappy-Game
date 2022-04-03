using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeGeneration : MonoBehaviour
{
	[SerializeField] GameObject Pipe;
	[SerializeField] float startingX;
	[SerializeField] int gapSize;

	Vector2[] currentPipesPositions;
	List<GameObject[]> pairs;
	[SerializeField] float flappyX;
	private void Start()
	{
		pairs = new List<GameObject[]>();
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
		float upperHeight = Random.Range(0.4f, 1f) * 4;
		GameObject upper = Instantiate(Pipe, new Vector2(startingX, upperHeight), Quaternion.identity);
		upper.GetComponent<Pipe>().Init(Direction.DOWNWARDS, 20);

		//int currentGapSize = (int) (gapSize * Random.Range(0.8f, 1f));
		GameObject bottom = Instantiate(Pipe, new Vector2(startingX, upperHeight - gapSize), Quaternion.identity);
		bottom.GetComponent<Pipe>().Init(Direction.UPWARDS, 20);

		pairs.Add(new GameObject[] { upper, bottom });
	}

	public IEnumerator Generate()
	{
		while(true)
		{
			CreatePair();
			yield return new WaitForSeconds(4f);
		}

	}

	public Vector2[] GetPipePositions()
	{
		GameObject[] last = pairs.First();
		return new Vector2[] { last[0].transform.position, last[1].transform.position };
	}

	public void ResetGenerator()
	{
		foreach(GameObject[] pair in pairs)
		{
			Destroy(pair[0]);
			Destroy(pair[1]);
		}
		StopAllCoroutines();
		pairs.Clear();
		StartCoroutine(Generate());
	}
}
