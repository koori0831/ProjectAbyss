using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Chipmunk.ZipLineSystem
{
    public class ZipLineShooter : MonoBehaviour, IZiplineRopeLinkable
    {
        [SerializeField] private ZipLineSettingSO settingSO;
        [SerializeField] private Transform firePoint;
        private ZipLineRope currentRope;
        #region Events
        public UnityEvent<float> OnReloadEvent;
        public UnityEvent OnReloadEndEvent;
        #endregion
        private int ammo = 0;
        [field: SerializeField] private bool AutoReload { get; set; } = true;

        public Transform LinkTransform => firePoint;

        public bool CanRide => false;

        public ZipLineRope Rope {get; set;}

        void Update()
        {
            Debug.Log(Rope);
            if (Rope != null)
            {
                Rope.UpdateRope();
            }
        }

        public void TryShoot(Vector2 direction)
        {
            if (ammo > 0)
            {
                Shoot(direction);
                ammo--;
            }

            if (ammo == 0 && AutoReload)
            {
                Reload();
            }
        }
        public void Reload()
        {
            StartCoroutine(ReloadCoroutine());
        }
        private IEnumerator ReloadCoroutine()
        {
            OnReloadEvent.Invoke(settingSO.reloadTime);

            yield return new WaitForSeconds(settingSO.reloadTime);

            ammo = settingSO.maxAmmo;
            OnReloadEndEvent.Invoke();
        }
        public void Shoot(Vector2 direction)
        {
            ZipLineBullet bullet = Instantiate(settingSO.bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.Initialize(settingSO);
            bullet.Shoot(direction);

            if(currentRope == null)
            {
                currentRope = Instantiate(settingSO.ropePrefab);
                currentRope.Initialize(bullet, this);
            }
            else
            {
                currentRope.Initialize(currentRope.linkerA, bullet);
                currentRope.UpdateRope();
                currentRope = null;
            }
        }
    }
}