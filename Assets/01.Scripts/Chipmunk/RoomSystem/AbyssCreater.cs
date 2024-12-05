using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class AbyssCreater : MonoBehaviour
{
    [SerializeField] AbyssRoomCreater roomCreater;
    [SerializeField] TileBase abyssTile;
    [field: SerializeField] public Tilemap AbyssTilemap { get; private set; }
    [field: SerializeField] public Tilemap AbyssPlatformTilemap { get; private set; }
    [field: SerializeField] public AbyssData AbyssData { get; private set; }
    [SerializeField] public UnityEvent onAbyssCreateStart;
    /// <summary>
    /// 생성에 걸린 시간을 반환;
    /// </summary>
    [SerializeField] public UnityEvent<float> onAbyssCreateEnd;

    private int lastGeneratedY = 0;
    void Awake()
    {
        CreateAbyss(AbyssData);
    }

    public void CreateAbyss(AbyssData abyssData, float generateDuration = 3)
    {
        AbyssData = abyssData;
        StartCoroutine(CreateAbyssAsync(generateDuration));
    }
    public IEnumerator CreateAbyssAsync(float duration)
    {
        float calculateTime = duration / AbyssData.abyssHeight;
        Debug.Log(calculateTime * AbyssData.abyssHeight);

        float genTime = 0;

        onAbyssCreateStart.Invoke();
        while (lastGeneratedY > -AbyssData.abyssHeight)
        {
            genTime += Time.deltaTime;
            CreateAbyssFloor(lastGeneratedY - 1);
            yield return null;
        }
        onAbyssCreateEnd.Invoke(genTime);
    }
    private void CreateAbyssFloor(int yPos)
    {
        if (yPos >= lastGeneratedY)
            return;

        for (int x = -AbyssData.abyssWidth / 2; x < AbyssData.abyssWidth / 2; x++)
        {
            if (IsHole(x))
            {
                continue;
            }
            AbyssTilemap.SetTile(new Vector3Int(x, yPos, 0), abyssTile);
        }
        lastGeneratedY = yPos;

        roomCreater.CreateRoom(yPos, AbyssData.abyssHoleWidth);
    }
    public bool IsHole(int x)
    {
        return x < AbyssData.abyssHoleWidth / 2 && x > -AbyssData.abyssHoleWidth / 2;
    }
}
[System.Serializable]
public struct AbyssData
{
    public int abyssWidth;
    public int abyssHoleWidth;

    public int abyssHeight;
}