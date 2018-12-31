using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTemplate : MonoBehaviour {

    public static RoomTemplate instance;

    [SerializeField] private Transform canvasObj;
    [SerializeField] private MinigameDifficulty difficulty;

    [System.Serializable]
    public class RoomVariation
    {
        public GameObject[] RoomSides;
        public GameObject EndRoom;
    }

    public RoomVariation[] RoomTypes;

    public GameObject WallRoom;
    public Image wallRoomUI;

    public GameObject FinishRoom;

    public float waitTime;

    public int roomCounter = 0;

    public List<GameObject> enemyTypes;

    private bool hasSpawnEndRoom = false;

    private List<GameObject> rooms;
    private int enemyRoomsRemaining;

    private void Awake()
    {
        instance = this;
        rooms = new List<GameObject>();
        roomCounter = 0;
        enemyRoomsRemaining = difficulty.numEnemyRooms;
    }

    private void Update()
    {
        if(waitTime <= 0)
        {
            if (!hasSpawnEndRoom)
            {
                hasSpawnEndRoom = true;
                Instantiate(FinishRoom, rooms[rooms.Count - 1].transform.position, Quaternion.identity);

                int rand;

                for (int i = 0; i < difficulty.numEnemyRooms; i++)
                {
                    rand = Random.Range(1, rooms.Count - 1);
                    GameObject room = rooms[rand];
                    rooms.Remove(room);
                    room.GetComponent<EnemyManager>().canSpawn = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    public void AddRoom(GameObject room)
    {
        rooms.Add(room);
    }

    public bool CheckRoomCounter()
    {
        return roomCounter < difficulty.maxRooms;
    }

    public Vector2 GetEnemyCountRange()
    {
        return new Vector2(difficulty.minRangeEnemiesPerRoom, difficulty.maxRangeEnemiesPerRoom);
    }

    public void SpawnRoom(int index1, Vector2 pos)
    {
        var roomTypes = RoomTypes[index1].RoomSides;
        int rand = Random.Range(0, roomTypes.Length - 1);

        var room = Instantiate(roomTypes[rand], pos, roomTypes[rand].transform.rotation);
        AddRoom(room);
    }

    public void SpawnEndRoom(int index, Vector2 pos)
    {
        GameObject roomEnd = RoomTypes[index].EndRoom;
        var room = Instantiate(roomEnd, pos, roomEnd.transform.rotation);
        AddRoom(room);
    }

    public void SpawnWallRoom(Vector2 pos)
    {
        Instantiate(WallRoom, pos, Quaternion.identity);
        //var ui = Instantiate(wallRoomUI, canvasObj);
        //ui.transform.localPosition = pos;
    }

    public void SpawnRoomUI(Image stageUI, Vector2 pos)
    {
        var ui = Instantiate(stageUI, canvasObj);
        ui.transform.localPosition = pos;
    }

    public void HasFinishedEnemyRooms()
    {
        if(enemyRoomsRemaining > 0)
            enemyRoomsRemaining--;
    }

    public bool AllEnemyRoomsFinished()
    {
        return enemyRoomsRemaining == 0;
    }
}
