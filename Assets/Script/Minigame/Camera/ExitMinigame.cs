using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMinigame : MonoBehaviour {

    [SerializeField] private int buildId;
    private bool isUnload = false;

	// Update is called once per frame
	void Update () {
        if (!isUnload && Input.GetKeyDown(KeyCode.Escape))
        {
            isUnload = true;
            StartCoroutine(UnloadMinigame());
        }
	}

    private IEnumerator UnloadMinigame()
    {
        yield return new WaitForSeconds(.10f);
        SceneFade.instance.EndMinigame(SceneManager.GetActiveScene().buildIndex);
    }
}
