using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float moveSpeed = 6;

	Rigidbody2D rigidbody;
	Camera viewCamera;
	Vector2 movement;
	Vector2 mousePos;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
		viewCamera = Camera.main;
	}

	void Update () {
		mousePos = viewCamera.ScreenToWorldPoint(Input.mousePosition);
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate() {
		rigidbody.MovePosition (rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
		Vector2 lookDir = mousePos - rigidbody.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rigidbody.rotation = angle;
	}
}