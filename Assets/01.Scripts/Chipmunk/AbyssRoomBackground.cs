using System;
using UnityEngine;

public class AbyssRoomBackground : MonoBehaviour
{
    [SerializeField] private AbyssRoomCreater abyssRoomCreater;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeDistance = 10f;
    [SerializeField] private float clampDistance = 5f;
    [SerializeField] private AbyssBackgroundData[] abyssBackgroundDatas;
    private int offsetID = Shader.PropertyToID("_Offset");
    private float size = 1;
    public float Size
    {
        get => size;
        set
        {
            size = value;
            if (size <= 0)
            {
                size = 0;
            }
            transform.localScale = new Vector3(size, size, 1);
        }
    }
    [SerializeField] private Transform followTarget;
    void Update()
    {
        Follow();
        // Fade();
    }

    private void Fade()
    {
        float distance = Mathf.Abs(followTarget.transform.position.x);
        float fade = Mathf.Clamp01((distance - clampDistance) / fadeDistance);
        Color color = Color.Lerp(fadeColor, defaultColor, fade);
        foreach (var data in abyssBackgroundDatas)
        {
            data.spriteRenderer.color = color;
        }
    }

    private void Follow()
    {
        float xPos = followTarget.position.x;
        AbyssRoom nearRoom = abyssRoomCreater.GetNearRoomByYPos(followTarget.position.y);
        float yPos = nearRoom.Position.y + nearRoom.RoomSO.mapSize.y + 2;

        Size = nearRoom.RoomSO.mapSize.y + 2;

        transform.position = new Vector3(xPos, yPos, 0);

        foreach (var data in abyssBackgroundDatas)
        {
            Vector2 offset = new Vector2(data.speed * xPos, 0);
            data.spriteRenderer.material.SetVector(offsetID, offset);
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        DrawVerticalLine(+fadeDistance, Color.green);
        DrawVerticalLine(-fadeDistance, Color.green);
        DrawVerticalLine(+clampDistance, Color.red);
        DrawVerticalLine(-clampDistance, Color.red);
    }
    private void DrawVerticalLine(float xPos, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(new Vector3(xPos, -1000), new Vector3(xPos, 1000));
    }
#endif
}
