using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour
{
    [SerializeField] private string animalName; //동물의 이름
    [SerializeField] private int hp;

    [SerializeField] private float walkSpeed; //걷기 속도
    [SerializeField] private float runSpeed; //뛰기 속도

    private Vector3 destination;  //목적지

    //상태 변수
    private bool isAction; //행동중인지 아닌지
    private bool isWalking; //걷는중인지 아닌지
    private bool isRunning; //뛰는중인지 아닌지
    private bool isDead;  //죽었는지 아닌지

    [SerializeField] private float walkTime;  //얼마동안 걸을지
    [SerializeField] private float waitTime;  //대기시간
    [SerializeField] private float runTime;  //대기시간
    private float currentTime;

    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
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
    }

    private void Move()
    {
        if(isWalking || isRunning )
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
            if(currentTime <= 0)
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

        int _random = Random.Range(0,10); //Idle, 풀뜯기, 걷기

        if(_random >= 0 && _random <=1)  // 2/10 확률
            Wait();
        else if(_random >= 2 && _random <=4 )  // 3/10 확률
            Eat();
        else if(_random >= 5 && _random <=9)  // 5/10 확률
            TryWalk();
        
    }

    private void Wait()
    {
        currentTime = waitTime;
        anim.SetTrigger("Idle");
    }

    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");

    }

    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        nav.speed = walkSpeed;

    }

    private void Run(Vector3 _targetPos)
    {
        destination = new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        nav.speed = runSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);

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
            Run(_targetPos);
        }
    }

    private void Dead()
    {
        isWalking = false;
        isRunning = false;

        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);

        //죽는 애니메이션(일단 없음)
        // Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, 0f, -90f), 0.9f);
        // transform.rotation = Quaternion.Euler(_rotation);

        isDead = true;
    }
}
