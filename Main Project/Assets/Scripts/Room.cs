using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType RoomType = RoomType.Room;

    public int limit;

    public GameObject DoorU;
    public GameObject DoorR;
    public GameObject DoorD;
    public GameObject DoorL;

    public void RotateRandomly()
    {
        int count = Random.Range(0, 4);

        for (int i = 0; i < count; i++)
        {
            transform.Rotate(0, 90, 0);

            GameObject tmp = DoorL;
            DoorL = DoorD;
            DoorD = DoorR;
            DoorR = DoorU;
            DoorU = tmp;
        }
    }
}

public enum RoomType
{
    Room,
    BossRoom,
    ShopRoom
}
