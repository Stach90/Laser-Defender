﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreKeeper : MonoBehaviour {


	public static int score = 0;
	private Text myText;
	
	void Start(){
		myText = GetComponent<Text>();
	
	}

	public void Score(int points){
		Debug.Log ("Scored points");
		score += points;
		myText.text = score.ToString();
	}
	
	
	public static void Reset(){
	score = 0;
	}
}
