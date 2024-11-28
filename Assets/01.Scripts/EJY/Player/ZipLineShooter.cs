using UnityEngine;

public class ZipLineShooter : Shooter
{
    private Vector2 _startPos = Vector2.zero, _endPos = Vector2.zero;
    private bool _isPair = false;

    protected override void FireBullet()
    {
        ZipLineBullet zipLineBullet = PoolManager.Instance.Pop(_bulletPrefab.poolName) as ZipLineBullet;
        zipLineBullet.Fire(_firePosTrm.position, _firePosTrm.right * _shootPoewr);
        Debug.Log(zipLineBullet);
    }
}
