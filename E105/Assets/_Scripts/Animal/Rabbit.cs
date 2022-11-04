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

    [SerializeField] private float waitTime;  //대기시간
    [SerializeField] private float idleTime;
    [SerializeField] private float eatTime;
    [SerializeField] private float walkTime;  //얼마동안 걸을지
    [SerializeField] private float runTime;
    private float currentTime;

    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;
    private NavMeshAgent nav;


    [SerializeField] private GameObject items;  //동물의 가죽
    //Item
    [SerializeField] private GameObject item20;  //동물의 가죽
    [SerializeField] private GameObject item21;   //부드러운 가죽
    [SerializeField] private GameObject item103;   //작은 고기
    [SerializeField] private GameObject item104;   //고기

     


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
        if (!isDead)
        {
            Move();
            ElapseTime();
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
        {
            nav.SetDestination(this.transform.position + destination * 5f);
        }
    }


    //시간 경과 함수
    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
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
        destination = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)).normalized;
        RandomAction();
    }

    private void RandomAction()
    {
        isAction = true;

        int _random = Random.Range(0, 10); //Idle, eat, walk

        if (_random >= 0 && _random <= 1)  // 2/10 확률
            Idle();
        else if (_random >= 2 && _random <= 4)  // 3/10 확률
            Eat();
        else if (_random >= 5 && _random <= 9)  // 5/10 확률
            TryWalk();

    }

    private void Idle()
    {
        currentTime = idleTime;
        anim.SetTrigger("Idle");
    }

    private void Eat()
    {
        currentTime = eatTime;
        anim.SetTrigger("Eat");

    }

    private void TryWalk()
    {
        nav.speed = walkSpeed;
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
    }

    private void Run(Vector3 _targetPos)
    {
        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        nav.speed = runSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);

        destination = new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized;
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;
            if (hp <= 0)
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

        isDead = true;

        Destroy(this.gameObject, 1f);
        Item();
    }

    private void Item()
    {
        Instantiate(item20, this.transform.position + new Vector3(Random.Range(-2,2),0f,Random.Range(-2,2)), Quaternion.identity).transform.parent = items.transform;
        Instantiate(item21, this.transform.position + new Vector3(Random.Range(-2,2),0f,Random.Range(-2,2)), Quaternion.identity).transform.parent = items.transform;
        Instantiate(item103, this.transform.position + new Vector3(Random.Range(-2,2),0f,Random.Range(-2,2)), Quaternion.identity).transform.parent = items.transform;
        Instantiate(item104, this.transform.position + new Vector3(Random.Range(-2,2),0f,Random.Range(-2,2)), Quaternion.identity).transform.parent = items.transform;
    }
}
