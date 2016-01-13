using UnityEngine;
using System;

public class CameraController1 : MonoBehaviour 
{
	//public  so we can assign the player to this variable in the inspector
	public GameObject player;
	//empty vector3 to store the strating position of the camera
	Vector3 offset;
	
	void Start () 
	{
		//store camera's starting position so tthat we can keep the same offset as the ball moves
		offset = transform.position;
	}
	
	void LateUpdate () 
	{
		//updatess the camera postion  as the ball moves
		transform.position = player.transform.position + offset;
	}
}