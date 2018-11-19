using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class InteractTV : MonoBehaviour {

    [SerializeField] private GameObject screen;
	private VideoPlayer videoPlayer;
	private bool isRunning = false;

	void Start(){
		videoPlayer = screen.GetComponent<VideoPlayer> ();
    }

	public void ScreenOn(){
        if (!isRunning)
            StartCoroutine(TurnOn());
	}

    private IEnumerator TurnOn()
    {
        isRunning = true;
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null; ;
        }
        videoPlayer.Play();
    }
		
	public void ScreenOff(){
        if (isRunning)
        {
            videoPlayer.Stop();
            isRunning = false;
        }
	}
}
