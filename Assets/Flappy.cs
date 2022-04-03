using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy : MonoBehaviour
{
	[SerializeField] float flapStrength;
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Flap();
		}
	}
	public void Flap()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * flapStrength, ForceMode2D.Impulse);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Pipe"))
		{
			Destroy(this.gameObject);
		}
	}
}
