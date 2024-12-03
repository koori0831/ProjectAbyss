using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ZipLine : MonoBehaviour
{
    [SerializeField] private Material _lineMaterial;

    private LineRenderer lineRenderer;

    private ZipLineBullet _startLinkBullet = null;
    private ZipLineBullet _endLinkBullet = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = _lineMaterial;
    }
    public void Initialize(Material material)
    {
        lineRenderer.material = material;
    }
    public void Link(ZipLineBullet startLinkBullet, ZipLineBullet endLinkBullet)
    {
        if (_startLinkBullet != null && _endLinkBullet != null)
        {
            PoolManager.Instance.Push(_startLinkBullet);
            PoolManager.Instance.Push(_endLinkBullet);

            _startLinkBullet = null;
            _endLinkBullet = null;
        }

        _startLinkBullet = startLinkBullet;
        _endLinkBullet = endLinkBullet;

        _startLinkBullet.currentLifeTime = 0;
        _endLinkBullet.currentLifeTime = 0;

        startLinkBullet.isLinked = true;
        endLinkBullet.isLinked = true;

        lineRenderer.SetPosition(0, startLinkBullet.transform.position);
        lineRenderer.SetPosition(1, endLinkBullet.transform.position);
    }

    public void ResetLineRenderer()
    {
        lineRenderer.positionCount = 0;
    }
}
