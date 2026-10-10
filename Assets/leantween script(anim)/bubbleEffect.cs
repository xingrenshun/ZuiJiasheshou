using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bubbleEffect : MonoBehaviour {
	// Use this for initialization
	void Start () {
		bubbleEffect1 ();
	}


	void bubbleEffect1(){
		LeanTween.scale (this.gameObject, this.gameObject.GetComponent<RectTransform>().localScale*0.90f, 0.6f).setEase(LeanTweenType.easeInOutSine).setOnComplete(bubbleEffect2).setIgnoreTimeScale(true);
	}

	void bubbleEffect2(){
		LeanTween.scale (this.gameObject, this.gameObject.GetComponent<RectTransform>().localScale*(10f/9.0f), 0.6f).setEase(LeanTweenType.easeInOutSine).setOnComplete(bubbleEffect1).setIgnoreTimeScale(true);
	}
}
