using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl2 : MonoBehaviour {


	string currentAnimation="";

	public void SetAnimation(string animationName){
		
		if (currentAnimation != "") {
			this.GetComponent<Animator> ().SetBool (currentAnimation, false);
		}
		this.GetComponent<Animator> ().SetBool (animationName, true);
		currentAnimation = animationName;
	}

	public void SetAnimationIdle(){
		if (currentAnimation != "") {
			this.GetComponent<Animator> ().SetBool (currentAnimation, false);
		}
	}
	
	public void SetDeathAnimation(int numOfClips){

		int clipIndex = Random.Range(0, numOfClips);
		string animationName = "Death";
		this.GetComponent<Animator> ().SetInteger (animationName, clipIndex);
	}















}
