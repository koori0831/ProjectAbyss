using UnityEngine;

public class ZipLineGun : Shooter
{
    private ZipLineBullet beforeShootedBullet;

    protected override void FireBullet()
    {
        ZipLineBullet zipLineBullet = PoolManager.Instance.Pop(_bulletPrefab.poolName) as ZipLineBullet;

        zipLineBullet.Fire(_firePosTrm, _shootPoewr);

        if (beforeShootedBullet != null)
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
