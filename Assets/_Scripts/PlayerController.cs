using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GUIText scoreText;
	public GUIText winText;
	public GUIText countDownText;
	public int score = 0;


	private string scorePreValue;
	private string winPreValue;
	private string countDownPreValue;
	private float startTime;
	private bool stopCountDown = false;
	private bool createCoin = true;

	public GameObject randomCoin;
	private float lastGeneratedCoinTime;


	protected CharacterController controller;

	// Use this for initialization
	void Start () {

		winPreValue = winText.text;
		winText.text = "";
		scorePreValue = scoreText.text;
		countDownPreValue = countDownText.text;
		startTime = Time.time;
		lastGeneratedCoinTime = Time.time;

		CalculateScore();

		/*controller = GetComponent<CharacterController>();
		if(!controller) {
			Debug.LogError("CharacterController bulunamadı.");
		}*/
	
	}
	
	// Update is called once per frame
	void Update () {
		//controller.SimpleMove(new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical")) * speed);
		CountDown();
	}


	void FixedUpdate(){
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
		Vector3 movement = new Vector3(horizontal, 0.0f,vertical);

		rigidbody.AddForce(movement * speed);

	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Coin") {
			other.gameObject.SetActive(false);
			score++;
			CalculateScore();
			if(score >= 6) {
				stopCountDown = true;
				winText.text = winPreValue;
			}
		}
		
	}

	void CalculateScore(){
		scoreText.text = scorePreValue + score.ToString();
	}

	void CountDown(){
		if(!stopCountDown) {
			countDownText.text = countDownPreValue + Mathf.CeilToInt(Time.time-startTime).ToString();
		}

		if( Mathf.CeilToInt(Time.time-lastGeneratedCoinTime) > 3 ) {

			int z = Random.Range(-5,5);
			int x = Random.Range(-5,5);
			Instantiate(randomCoin, new Vector3(x,1.0f,z), Quaternion.identity);

			lastGeneratedCoinTime = Time.time;
			createCoin = false;
		} else {
			createCoin = true;
		}


	}


}
