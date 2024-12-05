using System;
using UnityEngine;

public class AbyssBackground : MonoBehaviour
{
    private int offsetID = Shader.PropertyToID("_Offset");
    public AbyssBackgroundData[] abyssBackgroundDatas;
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
    [SerializeField] private Vector2 offset = new Vector2(0, -9);
    [SerializeField] private float clampYPos;
    // [SerializeField]
    private Transform FollowTarget => Camera.main.transform;
    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        float yPos = FollowTarget.position.y - offset.y > clampYPos ? clampYPos + offset.y : FollowTarget.position.y;
        transform.position = new Vector3(transform.position.x, yPos);
        foreach (var data in abyssBackgroundDatas)
        {
            Vector2 offset = new Vector2(data.speed * yPos, 0);
            // data.spriteRenderer.material.mainTextureOffset += new Vector2(0, data.speed * Time.deltaTime);
            data.spriteRenderer.material.SetVector(offsetID, offset);
            Debug.Log(offset);
        }
    }
}
[System.Serializable]
public struct AbyssBackgroundData
{
    public SpriteRenderer spriteRenderer;
    public float speed;
}