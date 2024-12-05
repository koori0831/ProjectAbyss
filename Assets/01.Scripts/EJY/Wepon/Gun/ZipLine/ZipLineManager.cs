using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ZipLineManager : MonoSingleton<ZipLineManager>
{
    [SerializeField] private Material _lineMaterial;
    [SerializeField] private LayerMask _whatIsObstacle;

    private LineRenderer lineRenderer;

    private ZipLineBullet _startLinkBullet = null;
    private ZipLineBullet _endLinkBullet = null;

    public ZipLineBullet beforeShootedBullet;

    protected override void Awake()
    {
        base.Awake();
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
        lineRenderer.SetPosition(0, new Vector3(0,0,0));
        lineRenderer.SetPosition(1, new Vector3(0,0,0));
    }

    public void LinkBullet(ZipLineBullet bullet1, ZipLineBullet bullet2)
    {
        bullet1.linkedBullet = bullet2;
        bullet2.linkedBullet = bullet1;
    }

    public void UnlinkBullet(ZipLineBullet bullet1, ZipLineBullet bullet2)
    {
        bullet1.linkedBullet = null;
        bullet2.linkedBullet = null;
    }

    public bool CheckPathBetweenBullets(Vector2 startPos, Vector2 endPos)
    {
        return Physics2D.Raycast(startPos, (endPos - startPos).normalized, (endPos - startPos).magnitude, _whatIsObstacle);
    }

    public void ResetBefore(ZipLineBullet bullet = null)
    {
        beforeShootedBullet = bullet;
    }
}
