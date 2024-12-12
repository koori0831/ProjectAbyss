using UnityEngine;

namespace Chipmunk.ZipLineSystem
{
    [CreateAssetMenu(menuName = "ZipLine/ZipLineSettingSO")]
    public class ZipLineSettingSO : ScriptableObject
    {
        [Header("Gun")]
        [SerializeField] public int maxAmmo = 5;
        [SerializeField] public float reloadTime = 3f;
        [Header("Bullet")]
        [SerializeField] public ZipLineBullet bulletPrefab;
        [SerializeField] public float bulletSpeed = 4;
        [Header("Rope")]
        [SerializeField] public ZipLineRope ropePrefab;
        [SerializeField] public float ropeMaxDistance = 8;

    }
}