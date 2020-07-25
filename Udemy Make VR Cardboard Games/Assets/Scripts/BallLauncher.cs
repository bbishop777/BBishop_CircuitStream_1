using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {

	public GameObject ballPrefab;
	public float ballSpeed = 5.0f;
	private Camera cam;
	// Use this for initialization
	void Start ()
	{
		cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			GameObject instance = Instantiate(ballPrefab);
			instance.transform.position = transform.position;
			Rigidbody rb = instance.GetComponent<Rigidbody>();
			rb.velocity = cam.transform.rotation * Vector3.forward * ballSpeed;
		}
	}
}
