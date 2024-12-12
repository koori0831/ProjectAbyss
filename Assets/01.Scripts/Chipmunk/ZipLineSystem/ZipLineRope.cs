using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.ZipLineSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class ZipLineRope : MonoBehaviour
    {
        public static List<ZipLineRope> ropes = new List<ZipLineRope>();
        [field: SerializeField] public SliderJoint2D sliderJoint2D { get; private set; }
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
        public void ConnectToThis(IZipLineRideable rideable)
        {
            Vector3 betweenVector = linkerB.LinkTransform.position - linkerA.LinkTransform.position;
            Vector3 halfBetweenVector = betweenVector / 2;
            float angle = Mathf.Atan2(betweenVector.y, betweenVector.x) * Mathf.Rad2Deg;

            transform.position = halfBetweenVector + linkerA.LinkTransform.position;

            {
                sliderJoint2D.connectedBody = rideable.rigidCompo;
                sliderJoint2D.angle = angle;

                sliderJoint2D.limits = new JointTranslationLimits2D
                {
                    min = -halfBetweenVector.magnitude,
                    max = halfBetweenVector.magnitude
                };

                sliderJoint2D.enabled = true;
            }

            rideable.connectingRope = this;
        }
        public void Disconnect(IZipLineRideable rideable)
        {
            sliderJoint2D.enabled = false;
            rideable.connectingRope = null;
        }
        public bool CheckCollision(IZipLineRideable rideable)
        {
            if (!linkerA.CanRide || !linkerB.CanRide)
                return false;

            float distance = Vector2.Distance(linkerA.LinkTransform.position, linkerB.LinkTransform.position);
            Vector2 direction = (linkerB.LinkTransform.position - linkerA.LinkTransform.position).normalized;
            RaycastHit2D[] hits = Physics2D.RaycastAll(linkerA.LinkTransform.position, direction, distance);
            Debug.DrawRay(linkerA.LinkTransform.position, direction * distance, Color.red, 1f);
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
                if (rope.CheckCollision(rideable))
                {
                    if (rideable.connectingRope != rope)
                        return rope;
                }
            }
            return null;
        }
    }

}