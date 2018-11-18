using UnityEngine;

public class SwitchStage : MonoBehaviour {

    [SerializeField] private Transform mapPlayer;
    [SerializeField] private GameObject mapStage;
    [SerializeField] private GameObject cam;
    private bool newStage = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cam.GetComponent<CameraFocus>().pos = new Vector3(transform.position.x, transform.position.y,100);
            GetComponent<StageManager>().EnableEnemies();
            if (!newStage)
            {
                newStage = true;
                mapStage.SetActive(true);
            }
            mapPlayer.position = mapStage.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetComponent<StageManager>().DisableEnemies();
        }
    }
}
