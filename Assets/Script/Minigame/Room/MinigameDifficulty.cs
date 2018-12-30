using UnityEngine;

[CreateAssetMenu(fileName = "MinigameDifficulty", menuName = "Minigame")]
public class MinigameDifficulty : ScriptableObject {

    public int numEnemyRooms;
    public int minRangeEnemiesPerRoom;
    public int maxRangeEnemiesPerRoom;

    public int maxRooms;
}
