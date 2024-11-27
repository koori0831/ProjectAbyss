using UnityEngine;
using UnityEngine.Tilemaps;

public class AbyssCreater : MonoBehaviour
{
    [SerializeField] AbyssRoomCreater roomCreater;
    [SerializeField] int abyssWidth = 100;
    [SerializeField] int abyssHoleWidth = 10;
    [SerializeField] TileBase abyssTile;
    [field: SerializeField] Tilemap AbyssTilemap { get; set; }


    private int lastGeneratedY = 0;
    void Awake()
    {
    }
    public void Update()
    {
        CreateAbyss(lastGeneratedY - 1);
    }
    public void CreateAbyss(int yPos)
    {
        if (yPos >= lastGeneratedY)
            return;

        for (int x = -abyssWidth / 2; x < abyssWidth / 2; x++)
        {
            if (IsHole(x))
            {
                continue;
            }
            AbyssTilemap.SetTile(new Vector3Int(x, yPos, 0), abyssTile);
        }
        lastGeneratedY = yPos;
        
        roomCreater.CreateRoom(yPos, abyssHoleWidth);
    }
    public bool IsHole(int x)
    {
        return x < abyssHoleWidth / 2 && x > -abyssHoleWidth / 2;
    }
}
