using UnityEngine;
using System.Collections;

public class ChangeLight : MonoBehaviour {

	private Light _light;
	public Gradient gradient;
	float duration = 4.0f;

	// Use this for initialization
	void Start () {
		_light = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		float t = Mathf.Repeat(Time.time, duration) / duration;
		_light.color = gradient.Evaluate(t);
	}
}
