using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringObject : MonoBehaviour
{
    public int _itemCode;
    public float _delayedTimer;
    public float _movedDelay;
    private PlayerSystem _ps;
    public Inventory _inventory;
    public Item _item;

    // Update is called once per frame

    public void Interaction(float time)
    {
        if(_inventory.CheckInven(_item.FindItem(_itemCode),1))
        {
            Vector3 Direction = (_ps.gameObject.transform.position - this.gameObject.transform.position).normalized;
            _ps._character.forward = Direction;
            StartCoroutine(StopPlayer(time));
        }
    }

    IEnumerator StopPlayer(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gatheringItem();
    }

    private void gatheringItem()
    {
        _inventory.AcquireItem(_item.FindItem(_itemCode),1);
    }
}
