using Chipmunk.ZipLineSystem;
using UnityEngine;

public interface IZipLineRideable
{
    public GameObject gameObject { get; }
    public Rigidbody2D rigidCompo { get; }
    public ZipLineRope connectingRope { get; set; }
    public void OnRide(ZipLineRope rope);
    public void TryRideRope()
    {
        Debug.Log("TryRideRope");   
        ZipLineRope rope = ZipLineRope.CheckCollisionRope(this);
        if (rope != null)
        {
            if (connectingRope != null)
            {
                connectingRope.Disconnect(this);
            }
            RideRope(rope);
        }
    }
    private void RideRope(ZipLineRope rope)
    {
        rope.ConnectToThis(this);
        OnRide(rope);
    }
}
