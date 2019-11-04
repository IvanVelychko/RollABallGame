using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float JumpForce;
	public Text winText;
	public Text countText;

	private bool OnGround;
	private Rigidbody rb;
	private int count;
	private int nextSceneToLoad;

	void Start (){
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
		OnGround = false;
		nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

//    void FixedUpdate (){
	void FixedUpdate (){
		if(OnGround == true) {
			if (Input.GetKeyDown(KeyCode.Space)) {
			this.gameObject.GetComponent <Rigidbody> ().AddForce (Vector3.up * JumpForce, ForceMode.Impulse); 
            OnGround = false;
            }
		}
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		

		

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		
        
		rb.AddForce (movement * speed);
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}
	

	void OnCollisionEnter(Collision c){
		if(c.gameObject.CompareTag ("Ground")) {
			OnGround = true;
		}
	}
	
	void SetCountText () {
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "You Win!";
//			sleep(500);
			SceneManager.LoadScene(nextSceneToLoad);
		}
	}
}
