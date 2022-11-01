using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalObject : MonoBehaviour
{
    public int _animalHp;
    public int _decreaseHp;
    public GameObject _sword;
    private Animator _animator;
    public Wander _wander;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animalHp = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        death();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            _animalHp = _animalHp-_decreaseHp;
        }
    }

    public void death()
    {
        if(_animalHp <= 0){

            Debug.Log("죽음");
            _animator.SetBool("isWalking", false);

            _wander.WalkSpeed=0;

            _animator.SetBool("isDead", true);
        }
    }
}
