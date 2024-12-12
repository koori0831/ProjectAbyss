using Chipmunk.ZipLineSystem;
using UnityEngine;

public interface IZipLineRideable
{
    public GameObject gameObject { get; }
    public Rigidbody2D rigidCompo { get; }
    public void OnRide(ZipLineRope rope);
    public void TryRideRope()
    {
        ZipLineRope rope = ZipLineRope.CheckCollisionRope(this);
        if (rope != null)
        {
            RideRope(rope);
        }
    }
    private void RideRope(ZipLineRope rope)
    {
        rope.sliderJoint2D.connectedBody = rigidCompo;
        OnRide(rope);
    }
}
