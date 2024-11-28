using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class ZipLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    private ZipLineBullet linkedBullet1;
    private ZipLineBullet linkedBullet2;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void Initialize(Material material)
    {
        lineRenderer.material = material;
    }
    public void Link(ZipLineBullet linkedBullet1, ZipLineBullet linkedBullet2)
    {
        linkedBullet1.isLinked = true;
        linkedBullet2.isLinked = true;

        lineRenderer.SetPosition(0, linkedBullet1.transform.position);
        lineRenderer.SetPosition(1, linkedBullet2.transform.position);
    }
}
