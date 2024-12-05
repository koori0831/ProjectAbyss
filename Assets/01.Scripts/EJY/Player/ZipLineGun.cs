using UnityEngine;

public class ZipLineGun : Shooter
{

    protected override void Attack()
    {
        ZipLineBullet zipLineBullet = PoolManager.Instance.Pop(_bulletPrefab.poolName) as ZipLineBullet;

        zipLineBullet.Fire(_firePosTrm, _shootPoewr);
    }
}
