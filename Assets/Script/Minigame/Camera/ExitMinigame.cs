using System.Collections;
using UnityEngine;

public class ExitMinigame : MonoBehaviour {

    [SerializeField] private int buildId;
    private bool isUnload = false;

	// Update is called once per frame
	private void Update () {
        if (!isUnload && Input.GetKeyDown(KeyCode.Escape))
        {
            isUnload = true;
            StartCoroutine(UnloadMinigame(GetComponent<VerdictGame>().GetWonGame()));
        }
	}

    private IEnumerator UnloadMinigame(bool wonGame)
    {
        yield return new WaitForSeconds(.10f);
        SceneFade.instance.EndMinigame(buildId,wonGame);
    }
}
