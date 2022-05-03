using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	[SerializeField] float speed;
	public void Update()
	{
		transform.position += (Vector3)Vector2.left * speed * Time.deltaTime;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("boundry"))
		{
			Destroy(this.gameObject);
		}
	}
}
