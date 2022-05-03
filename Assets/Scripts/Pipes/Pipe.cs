using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
	UPWARDS, DOWNWARDS
}

public class Pipe : MonoBehaviour
{
	[SerializeField] GameObject body;
	[SerializeField] int length;
	[SerializeField] float speed;

	public void Init(Direction direction, int length)
	{
		this.length = length;
		if (direction == Direction.UPWARDS)
		{
			InitUpwards();
		}

		else { InitDownwards(); }
	}
	public void Update()
	{
		transform.position += (Vector3)Vector2.left * speed * Time.deltaTime ;
	}

	private void InitUpwards()
	{
		for (int i = 1; i < length + 1; i++)
		{
			Vector2 pos = new Vector2(transform.position.x, transform.position.y + (-0.45f * i));
			GameObject piece = Instantiate(body, pos, Quaternion.identity);
			piece.transform.SetParent(transform);
		}
	}
	
	private void InitDownwards()
	{
		for (int i = 1; i < length + 1; i++)
		{
			Vector2 pos = new Vector2(transform.position.x, transform.position.y + (0.45f * i));
			GameObject piece = Instantiate(body, pos, Quaternion.identity);
			piece.transform.SetParent(transform);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("boundry"))
		{
			Destroy(this.gameObject);
		}
	}

	//private IEnumerator destroy()
	//{
	//	yield return new WaitForSeconds(10f);
	//	Destroy(this.gameObject);
	//}

}
