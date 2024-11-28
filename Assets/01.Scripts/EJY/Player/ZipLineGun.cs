using UnityEngine;

public class ZipLineGun : Shooter
{
    private LineRenderer _lineRenderer;

    private Vector2 _startPos = Vector2.zero, _endPos = Vector2.zero;
    private bool _isPair = false;
    private ZipLineBullet beforeShootedBullet;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void FireBullet()
    {
        ZipLineBullet zipLineBullet = PoolManager.Instance.Pop(_bulletPrefab.poolName) as ZipLineBullet;

        zipLineBullet.Fire(_firePosTrm.position, _firePosTrm.right * _shootPoewr);
        if(beforeShootedBullet != null)
        {
            LinkBullet(beforeShootedBullet, zipLineBullet);
            beforeShootedBullet = null;
        }
        else
            beforeShootedBullet = zipLineBullet;
    }
    public void LinkBullet(ZipLineBullet bullet1, ZipLineBullet bullet2)
    {
        bullet1.linkedBullet = bullet2;
        bullet2.linkedBullet = bullet1;
    }
}
