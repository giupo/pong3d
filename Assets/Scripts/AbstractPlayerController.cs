using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbstractPlayerController : MonoBehaviour {

	private int score;
	public Text scoreLabel;
	public float xp = 8f;

	protected AudioSource audioSource;

	public virtual void Start () {
		audioSource = GetComponent<AudioSource> ();
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncrementScore() {
		audioSource.PlayOneShot (audioSource.clip);
		++score;
		scoreLabel.text = score.ToString ();
	}
}
