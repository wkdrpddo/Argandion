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
    public bool _isremain;

    // Update is called once per frame

    void Start()
    {
        _ps = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
    }

    public void Interaction(float time)
    {
        if(_inventory.CheckInven(_item.FindItem(_itemCode),1))
        // if (true)
        {
            Debug.Log("this is true");
            Debug.Log(_ps.gameObject.transform.position);
            Debug.Log(gameObject.transform.position);
            Vector3 Direction = (gameObject.transform.position - _ps.gameObject.transform.position);
            Direction.y = 0;
            Direction = Direction.normalized;
            _ps._character.forward = Direction;
            _ps._playerAnimator.SetInteger("action", 10);
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
        _ps._playerAnimator.SetInteger("action", 0);
        _inventory.AcquireItem(_item.FindItem(_itemCode),1);
        if (!_isremain)
        {
            Destroy(this.gameObject);
        }
    }
}
