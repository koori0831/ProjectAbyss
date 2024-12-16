using UnityEngine;

namespace Chipmunk.ZipLineSystem
{
    public interface IZiplineRopeLinkable
    {
        public Transform LinkTransform { get; }
        public ZipLineRope Rope { get; set; }
        public bool CanRide { get; }
    }
}
