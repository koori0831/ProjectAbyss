using UnityEngine;

public class AbyssRoom
{
    public AbyssRoomSO RoomSO { get; private set; }
    public Vector2 Position { get; private set; }
    public AbyssRoom(AbyssRoomSO room, Vector2 position)
    {
        this.RoomSO = room;
        this.Position = position;
    }
}
