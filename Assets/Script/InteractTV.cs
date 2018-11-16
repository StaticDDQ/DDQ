using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class InteractTV : MonoBehaviour {

    [SerializeField] private GameObject screen;
	private VideoPlayer videoPlayer;
	private bool isRunning = false;

	void Start(){
		videoPlayer = screen.GetComponent<VideoPlayer> ();
        screen.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

	public void ScreenOn(){
        if (!isRunning)
            StartCoroutine(TurnOn());
	}

    private IEnumerator TurnOn()
    {
        isRunning = true;
        WaitForSeconds waitTime = new WaitForSeconds(1);
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return waitTime;
        }
        screen.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        videoPlayer.Play();
    }
		
	public void ScreenOff(){
        if (isRunning)
        {
            videoPlayer.Stop();
            screen.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            isRunning = false;
        }
	}
}
