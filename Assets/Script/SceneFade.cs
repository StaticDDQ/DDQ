using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFade: MonoBehaviour {

    public static SceneFade instance;

	[SerializeField] private Texture2D fadeOutText;
    [SerializeField] private float fadeSpeed = 0.35f;

	private float alpha = 1f;
	private int fadeDir = -1;
    private bool waitLoad = false;

    // There is only one instance of this and will appear in every scene
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartMinigame(int m)
    {
        if (!SceneManager.GetSceneByBuildIndex(m).isLoaded)
        {
            StartCoroutine(LoadLevel(m, true)); 
        }
    }

    public void EndMinigame(int m)
    {
        if (SceneManager.GetSceneByBuildIndex(m).isLoaded)
        {
            SceneManager.UnloadSceneAsync(m);
            Camera.main.enabled = true;
        }
    }

    // when the player wants to load a level
    public void StartLevel(int level)
    {
        if (!waitLoad)
        {
            StartCoroutine(LoadLevel(level,false));
        }
    }

    // it will fade in first and wait till the level is ready to be loaded, afterwards it will fade out
    private IEnumerator LoadLevel(int index, bool isMinigame)
    {
        waitLoad = true;
        fadeDir = 1;
        yield return new WaitForSeconds(2);
        AsyncOperation operation;
        if (!isMinigame)
            operation = SceneManager.LoadSceneAsync(index);
        else 
            operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }

        if (isMinigame)
        {
            Camera.main.enabled = false;
        }
        fadeDir = -1;
        yield return new WaitForSeconds(1);
        waitLoad = false;
    }

    // assign alpha to either render or unrender the black texture
    private void OnGUI()
	{
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // Restrict values from 0 to 1 only
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutText);
	}

    // used if the player wants to reset the level
    public void ResetLevel()
    {
        instance.StartLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
