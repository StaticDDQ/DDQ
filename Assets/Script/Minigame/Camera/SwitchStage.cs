using UnityEngine;
using UnityEngine.UI;

public class SwitchStage : MonoBehaviour {

    private Camera cam;
    private EnemyManager em;
    private bool firstTimeEnter = true;
    private bool hasSpawned = false;
    [SerializeField] private Image stageImg;

    private void Start()
    {
        cam = Camera.main;
        em = GetComponent<EnemyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cam.GetComponent<CameraFocus>().pos = new Vector3(transform.position.x, transform.position.y,100);
            if (firstTimeEnter)
            {
                firstTimeEnter = false;
                RoomTemplate.instance.SpawnRoomUI(stageImg, transform.position * 5);

                if (em != null && !hasSpawned)
                {
                    hasSpawned = true;
                    em.StartSpawning();
                }
            }
        }
    }
}
