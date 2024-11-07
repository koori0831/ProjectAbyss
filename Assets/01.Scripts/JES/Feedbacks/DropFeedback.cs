using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFeedback : Feedback
{
    [SerializeField] private DropTableSO _dropTable;
    [SerializeField] private float _dropPower;
    public override void PlayFeedback()
    {
        _dropTable.tables.ForEach(t => DropItem(t));
    }

    public override void StopFeedback()
    {
    }

    private void DropItem(DropInfo info)
    {
        if (info.dropRate > Random.value)
        {
            Collectable item = PoolManager.Instance.Pop(info.item.prefab.poolName) as Collectable;
            item.SetItemData(info.item);

            Vector3 dropDirection = Quaternion.Euler(0, 0, Random.Range(-50f, 50f)) * Vector3.up;
            item.DropIt(transform.position, dropDirection * _dropPower);
        }
    }
  
}
