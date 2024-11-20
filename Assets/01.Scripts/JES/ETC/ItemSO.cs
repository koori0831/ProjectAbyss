using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Coin,Ammo
}
[CreateAssetMenu(menuName ="SO/Item/Data")]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;
    public Sprite itemSprite;

    public int minAmount, maxAmount;
    public PoolItemSO prefab;

    public int GetRandomAmount()=>Random.Range(minAmount,maxAmount+1);

}
