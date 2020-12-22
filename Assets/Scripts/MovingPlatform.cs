using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour { 

    public Transform movingPlatform;
    public Transform position1;
    public Transform position2;
    public Vector3 newPostion;
    public string currentState;
    public float smooth;
    public float resetTime;

    void Start() 
	{
        ChangeTarget ();		
    }

    void FixedUpdate () 
	{
        movingPlatform.position = Vector3.Lerp (movingPlatform.position, newPostion, smooth * Time.deltaTime);
    }

    void ChangeTarget () 
	{
		
		if (currentState == "Moving To Position 1") 
		{
            currentState = "Moving To Position 2";
            newPostion = position2.position;
        }
        else if (currentState == "Moving To Position 2") 
		{
            currentState = "Moving To Position 1";
            newPostion = position1.position;
        }
		else if (currentState == "") 
		{
            currentState = "Moving To Position 2";
            newPostion = position2.position;
        }
        Invoke ("ChangeTarget", resetTime) ;
    }
}


		