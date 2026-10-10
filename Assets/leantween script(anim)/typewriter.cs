using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class typewriter : MonoBehaviour
{

	Text txt;
	string story;
	public float delay = 0.3f;

	public void OnEnable ()
	{
		txt = GetComponent<Text> ();
		txt.enabled = false;

		// add optional delay when to start
		StartCoroutine ("PlayText");
	}

	IEnumerator PlayText ()
	{
		
		yield return new WaitForSeconds (delay);

		story = txt.text;
		txt.text = "";
		txt.enabled = true;
		foreach (char c in story) {
			txt.text += c;
			yield return new WaitForSeconds (0.1f);
		}
	}
}
