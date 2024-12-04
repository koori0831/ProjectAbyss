using UnityEngine;

public class ZipLineGun : Shooter
{
    public ZipLineBullet beforeShootedBullet;

    protected override void FireBullet()
    {
        ZipLineBullet zipLineBullet = PoolManager.Instance.Pop(_bulletPrefab.poolName) as ZipLineBullet;

        zipLineBullet.Fire(_firePosTrm, _shootPoewr);
    }

    public void ResetBefore(ZipLineBullet bullet = null)
    {
        beforeShootedBullet = bullet;
    }
}
