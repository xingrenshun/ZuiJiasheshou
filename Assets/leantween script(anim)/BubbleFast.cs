using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BubbleFast : MonoBehaviour
{
	int id1, id2;
	// Use this for initialization
	void OnEnable ()
	{
		bubbleEffect1 ();
	}


	void bubbleEffect1 ()
	{
		id1 = LeanTween.scale (this.gameObject, this.gameObject.GetComponent<RectTransform> ().localScale * 0.98f, 0.2f).setEase (LeanTweenType.easeInOutSine).setOnComplete (bubbleEffect2).setIgnoreTimeScale (true).id;
	}

	void bubbleEffect2 ()
	{
		id2 = LeanTween.scale (this.gameObject, this.gameObject.GetComponent<RectTransform> ().localScale * (10f / 9.8f), 0.2f).setEase (LeanTweenType.easeInOutSine).setOnComplete (bubbleEffect1).setIgnoreTimeScale (true).id;
	}

	void OnDisable ()
	{
		LeanTween.cancel (id1);
		LeanTween.cancel (id2);
	}
}
