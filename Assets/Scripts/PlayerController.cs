using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;

	public GameObject player;

	private int count;

	public float timerInSeconds;
	public int startTime;
	public int minutes;
	public int seconds;

	public int pointsRed = 10;
	public int pointsBlue = 20;
	public int pointsYellow = 30;
	public int pointsGreen = 50;

	public Text countText;
	public Image winPanel;
	public Text timeText;
	public Text left;
	public Text winText;
	public Text collectedText;
	bool textSet = false;

	GameObject[] pickupsRed, pickupsBlue, pickupsYellow, pickupsGreen;

	private Rigidbody rb;

	public int objectsLeft;

	void Start(){
		pickupsRed = GameObject.FindGameObjectsWithTag("Pick Up Red");
		pickupsBlue = GameObject.FindGameObjectsWithTag("Pick Up Blue");
		pickupsYellow = GameObject.FindGameObjectsWithTag("Pick Up Yellow");
		pickupsGreen = GameObject.FindGameObjectsWithTag("Pick Up Green");

		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText();
		timerInSeconds = startTime;
		objectsLeft = pickupsRed.Length + pickupsBlue.Length + pickupsYellow.Length + pickupsGreen.Length;


		SetStartText ();

	}

	void Update(){
		timerInSeconds -= Time.deltaTime;
		minutes = (int)(timerInSeconds / 60f);
		seconds = (int)(timerInSeconds % 60f);
		CheckFinish ();
		SetTexts ();
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
		CheckFalldownReset ();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Pick Up Red")) {
			count += pointsRed;
			other.gameObject.SetActive (false);
			objectsLeft--;
			SetCountText();
			StartCoroutine(ShowMessage("+10", 2));
		}
		if (other.gameObject.CompareTag ("Pick Up Blue")) {
			count += pointsBlue;
			other.gameObject.SetActive (false);
			objectsLeft--;
			SetCountText();
			StartCoroutine(ShowMessage("+20", 2));
		}
		if (other.gameObject.CompareTag ("Pick Up Yellow")) {
			count += pointsYellow;
			other.gameObject.SetActive (false);
			objectsLeft--;
			SetCountText();
			StartCoroutine(ShowMessage("+30", 2));
		}
		if (other.gameObject.CompareTag ("Pick Up Green")) {
			count += pointsGreen;
			other.gameObject.SetActive (false);
			objectsLeft--;
			SetCountText();
			StartCoroutine(ShowMessage("+50", 2));
		}
	}

	void SetCountText(){
		countText.text = "Points: " + count.ToString ();
	}

	void SetTexts(){
		timeText.text = "Time: " + minutes.ToString ("00") + ":" + seconds.ToString ("00");
		left.text = "To collect: " + objectsLeft.ToString ();
		if(timerInSeconds < startTime-5 && !textSet){
			winPanel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
			winText.text = "";
			textSet = true;
		}
	}

	void CheckFinish(){
		if(objectsLeft == 0){
			winText.text = "Game finished, all objects collected!";
			winPanel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
			Time.timeScale = 0.0f;
			timerInSeconds += Time.deltaTime;
		}
		if(timerInSeconds <= 0){
			winText.text = "Game finished, time over!";
			winPanel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
			Time.timeScale = 0.0f;
			timerInSeconds += Time.deltaTime;
		}
	}

	void SetStartText(){
		winPanel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		winText.text = "Collect all objects before the time runs out!";
		collectedText.text = "";

	}

	void CheckFalldownReset(){
		if (player.transform.position.y < -20.0f) {
			player.transform.position = new Vector3(0, 5.0f, 0);
			rb.velocity = new Vector3(0, 0, 0);
			StartCoroutine(ShowMessage("-10", 2));
			count -= 10;
			SetCountText();
		}
	}

	public void RestartGame(){
		SceneManager.LoadScene (0);
		textSet = false;
		count = 0;
		SetCountText();
		timerInSeconds = startTime;
		objectsLeft = pickupsRed.Length + pickupsBlue.Length + pickupsYellow.Length + pickupsGreen.Length;
		SetStartText ();
		Time.timeScale = 1.0f;
	}

	IEnumerator ShowMessage (string message, float delay) {
		collectedText.text = message;
		yield return new WaitForSeconds(delay);
		collectedText.text = "";
	}

}
