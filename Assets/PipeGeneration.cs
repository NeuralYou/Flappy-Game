using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGeneration : MonoBehaviour
{
	[SerializeField] GameObject Pipe;
	[SerializeField] float startingX;
	[SerializeField] int gapSize;

	private void Start()
	{
		StartCoroutine(Generate());
	}

	public void CreatePair()
	{
		float upperHeight = Random.Range(0.4f, 1f) * 4;
		GameObject upper = Instantiate(Pipe, new Vector2(startingX, upperHeight), Quaternion.identity);
		upper.GetComponent<Pipe>().Init(Direction.DOWNWARDS, 20);

		int currentGapSize = (int) (gapSize * Random.Range(0.8f, 1f));
		GameObject bottom = Instantiate(Pipe, new Vector2(startingX, upperHeight - currentGapSize), Quaternion.identity);
		bottom.GetComponent<Pipe>().Init(Direction.UPWARDS, 20);
	}

	public IEnumerator Generate()
	{
		while(true)
		{
			CreatePair();
			yield return new WaitForSeconds(4f);
		}

	}
}
