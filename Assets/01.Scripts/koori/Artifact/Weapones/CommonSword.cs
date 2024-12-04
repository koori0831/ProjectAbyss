using UnityEngine;

public class CommonSword : Weapon
{
    public override void Attack()
    {
        base.Attack();

        Debug.Log("³ª´Â Â¯Â¯ ½Ú °ËÀÌÁö·Õ");
    }
}
