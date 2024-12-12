using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.ZipLineSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class ZipLineRope : MonoBehaviour
    {
        public static List<ZipLineRope> ropes = new List<ZipLineRope>();
        [SerializeField] public SliderJoint2D sliderJoint2D { get; private set; }
        [SerializeField] public IZiplineRopeLinkable linkerA { get; private set; }
        [SerializeField] public IZiplineRopeLinkable linkerB { get; private set; }
        private LineRenderer lineRenderer;
        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        public void Initialize(IZiplineRopeLinkable linkerA, IZiplineRopeLinkable linkerB)
        {
            this.linkerA = linkerA;
            linkerA.Rope = this;
            this.linkerB = linkerB;
            linkerB.Rope = this;

            if (ropes.Contains(this) == false)
            {
                ropes.Add(this);
            }
        }
        void OnDestroy()
        {
            if (ropes.Contains(this))
            {
                ropes.Remove(this);
            }
        }
        public void UpdateRope()
        {
            lineRenderer.SetPosition(0, linkerA.LinkTransform.position);
            lineRenderer.SetPosition(1, linkerB.LinkTransform.position);
        }
        public bool CheckCollision(IZipLineRideable rideable)
        {
            float distance = Vector2.Distance(linkerA.LinkTransform.position, linkerB.LinkTransform.position);
            Vector2 direction = (linkerB.LinkTransform.position - linkerA.LinkTransform.position).normalized;
            RaycastHit2D[] hits = Physics2D.RaycastAll(linkerA.LinkTransform.position, direction, distance);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject == rideable.gameObject)
                {
                    return true;
                }
            }
            return false;
        }
        public static ZipLineRope CheckCollisionRope(IZipLineRideable rideable)
        {
            foreach (var rope in ropes)
            {
                if(rope.CheckCollision(rideable))
                {
                    return rope;
                }
            }
            return null;
        }
    }

}