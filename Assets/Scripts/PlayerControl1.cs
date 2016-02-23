using UnityEngine;
using System;
// required to use "text" object and other UI elements
using UnityEngine.UI;

public class PlayerControl1 : MonoBehaviour 
{
	//public so that we can change the value in the inspector
	public float speed = 900.0f;
	public float speedAc = 17;
	//"scoreText will store our UI Text Object"
	public Text scoreText;
	//"count" wil keep track of how many cubes we picked up
	private int count =18;
	public Text WinText;
	//accelerometer
	private Vector3 curAc;
	private Vector3 zeroAc;
	private float smooth = 0.9f;
	private float sensV =30;
	private float sensH =30;
	private float GetAxisH =0;
	private float GetAxisV =0;

	//infinite loop while we are in playmode
	void FixedUpdate()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			//left- right input stored as a float between -1.0 and 1.0
			float moveHorizontal = Input.GetAxis ("Horizontal");
			//down-  up input stores as a float between -1.0 and 1.0
			float moveVertical = Input.GetAxis ("Vertical");
			// direction of the sum of both the input values
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			// applies force in the desired direciton
			GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
		
		} else {
			curAc = Vector3.Lerp(curAc,Input.acceleration,Time.deltaTime/smooth);
			GetAxisV=Mathf.Clamp(curAc.y * sensV,-1,1);
			GetAxisH=Mathf.Clamp(curAc.x * sensH,-1,1);
			//Use getAxisV and GetAxisH instead of Input.GetAxis for verti and horizontal
			//If H and V are swapped, swap curAc.y and curAc.x.
			//If any axis is going in the wrong direction, use -curAc.y and -curAc.x
			Vector3 movement = new Vector3 (GetAxisH, 0.0f, GetAxisV);
			GetComponent<Rigidbody>().AddForce(movement * speedAc);
		
		
		}	



	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickup") {
			other.gameObject.SetActive (false);
			//Adds 1 to count each time we pick up a cube
			count = count - 1;
			//Updates the text property of scoreText
			scoreText.text = "Cubes Remaining :" + count;
		}
		if (count == 0) {
			WinText.text ="All cubes collected. GOOD WORK!!";

		}

	}
	
}