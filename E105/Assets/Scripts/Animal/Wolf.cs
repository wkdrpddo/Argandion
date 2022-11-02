using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviour
{
    [SerializeField] private string animalName; //동물의 이름
    [SerializeField] private int hp;

    [SerializeField] private float walkSpeed; //걷기 속도
    [SerializeField] private float runSpeed;

    private Vector3 destination;  //목적지

    //상태 변수
    private bool isAction; //행동중인지 아닌지
    private bool isWalking; //걷는중인지 아닌지
    private bool isRunning;  //뛰는중인지 아닌지
    private bool isChasing; //추격중인지 아닌지
    private bool isDead;  //죽었는지 아닌지

    [SerializeField] private float walkTime;  //얼마동안 걸을지
    [SerializeField] private float waitTime;  //대기시간 and 하울링시간
    [SerializeField] private float idleTime;  //idle 시간
    [SerializeField] private float chaseTime;  //추격시간
    private float currentTime;

    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;
    private NavMeshAgent nav;
    [SerializeField] private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            Move();
            ElapseTime();  
        }
        if(isChasing)
        {
            nav.SetDestination(playerPos.position);
            
        }
    }

    private void Move()
    {
        if(isWalking)
        {
            nav.SetDestination(this.transform.position + destination * 5f);
        }
    }


    //시간 경과 함수
    private void ElapseTime()
    {
        if(isAction)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0 && !isChasing)
            {
                //다음 랜덤 행동 개시
                ReSet();
            }
        }

    }

    private void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;
        nav.speed = walkSpeed;
        nav.ResetPath();
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination = new Vector3(Random.Range(-10f,10f), 0f, Random.Range(-10f,10f)).normalized;
        RandomAction();
    }

    private void RandomAction()
    {
        isAction = true;

        int _random = Random.Range(0,10); //대기, idle, 하울링, 걷기

        if(_random == 0)  // 1/10 확률
            Wait();
        else if(_random >= 1 && _random <= 2) // 2/10 확률
            Idle();
        else if(_random >= 3 && _random <= 4) // 2/10 확률
            Howling();
        else if(_random >= 5 && _random <= 9) // 5/10 확률
            TryWalk();
        
    }

    private void Wait()
    {
        currentTime = waitTime;
    }

    private void Idle()
    {
        currentTime = idleTime;
        anim.SetTrigger("Idle");
    }

    private void Howling()
    {
        currentTime = waitTime;
        anim.SetTrigger("Howl");
    }

    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        nav.speed = walkSpeed;
    }

    private void Chase(Vector3 _targetPos)
    {
        // isWalking = false;
        isChasing = true;
        // currentTime = chaseTime;
        //destination = _targetPos;
        nav.speed = runSpeed;
        isRunning = true;
        anim.SetBool("Running", isRunning);
        //nav.SetDestination(destination);

    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if(!isDead)
        {
            hp -= _dmg;

            if(hp<=0)
            {
                Dead();
                return;
            }

            Chase(_targetPos);
        }     
    }

    private void Dead()
    {
        isWalking = false;
        isRunning = false;

        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        anim.SetTrigger("Death");

        isDead = true;
    }

}
