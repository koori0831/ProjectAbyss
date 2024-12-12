using System;
using UnityEngine;

namespace Chipmunk.ZipLineSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class ZipLineBullet : MonoBehaviour, IZiplineRopeLinkable
    {
        [SerializeField] LayerMask groundLayer;
        private ZipLineBullet linkedBullet;
        public bool IsLinked => linkedBullet != null;
        private bool isPlaced = false;
        public bool IsPlaced => isPlaced;
        public bool CanRide => IsPlaced;


        private ZipLineSettingSO zipLineSettingSO;

        #region Components
        public Rigidbody2D rigidCompo { get; private set; }
        public Collider2D colliderCompo { get; private set; }

        public Transform LinkTransform => transform;

        public ZipLineRope Rope { get; set; }

        #endregion
        void Awake()
        {
            rigidCompo = GetComponent<Rigidbody2D>();
            colliderCompo = GetComponent<Collider2D>();
        }
        void FixedUpdate()
        {
            if (!isPlaced)
            {
                if (Rope != null)
                {
                    Rope.UpdateRope();
                }
            }
        }
        public void Initialize(ZipLineSettingSO zipLineSettingSO)
        {
            this.zipLineSettingSO = zipLineSettingSO;
        }
        public void LinkBullet(ZipLineBullet bullet)
        {
            linkedBullet = bullet;
        }
        public void Shoot(Vector2 direction)
        {
            float speed = zipLineSettingSO.bulletSpeed;
            rigidCompo.linearVelocity = direction * speed;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((groundLayer.value & (1 << other.gameObject.layer)) != 0)
            {
                PlaceZipLine();
            }
        }

        private void PlaceZipLine()
        {
            isPlaced = true;
            rigidCompo.bodyType = RigidbodyType2D.Static;
        }
    }
}