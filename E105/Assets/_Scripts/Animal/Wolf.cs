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
    private bool isAttacking; //공격중
    private bool isDead;  //죽었는지 아닌지

    [SerializeField] private float waitTime;  //대기시간 
    [SerializeField] private float idleTime;
    [SerializeField] private float howlTime;
    [SerializeField] private float walkTime;  //얼마동안 걸을지
    [SerializeField] private float chaseTime;  //추격시간
    private float currentTime;

    [SerializeField] private float attackDamage;
    [SerializeField] private float attackDelay;
    [SerializeField] private LayerMask targetMask;



    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider boxCol;
    private NavMeshAgent nav;
    [SerializeField] private Transform playerPos;
    [SerializeField] private PlayerSystem _playerSystem;

    //Item
    [SerializeField] private GameObject item20;  //동물의 가죽
    [SerializeField] private GameObject item21;   //부드러운 가죽
    [SerializeField] private GameObject item103;   //작은 고기
    [SerializeField] private GameObject item104;   //고기

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerPos = GameObject.Find("PlayerObject").transform;
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
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

        if (isWalking)
        {
            nav.SetDestination(this.transform.position + destination * 5f);  //이동할땐 랜덤지역으로 이동
        }
        if (isChasing)
        {
            nav.SetDestination(playerPos.position);   //쫒을때는 플레이어를 목적지로 설정
        }

    }


    //시간 경과 함수
    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isAttacking)
            {
                //다음 랜덤 행동 개시
                ReSet();
                StopCoroutine(AttackCoroutine());
            }
        }
    }

    private void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isChasing = false;
        isAttacking = false;
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

        int _random = Random.Range(0, 10); //대기, idle, 하울링, 걷기

        if (_random == 0)  // 1/10 확률
            Idle();
        else if (_random >= 1 && _random <= 2) // 2/10 확률
            Howling();
        else if (_random >= 3 && _random <= 9) // 7/10 확률
            TryWalk();

    }

    private void Idle()
    {
        currentTime = idleTime;
        anim.SetTrigger("Idle");
    }

    private void Howling()
    {
        currentTime = howlTime;
        anim.SetTrigger("Howl");
    }

    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        nav.speed = walkSpeed;
    }

    public void Damage(int _dmg)
    {
        if (!isDead)
        {
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }

            Chase();
        }
    }

    private void Chase()
    {
        if (!isDead)
        {
            currentTime = chaseTime;
            isChasing = true;
            isWalking = false;
            isRunning = true;
            nav.speed = runSpeed;
            anim.SetBool("Walking", isWalking);
            anim.SetBool("Running", isRunning);

            if (!isDead && Vector3.Distance(this.transform.position, playerPos.position) <= 3f)
            {
                Debug.Log("늑대가 플레이어 공격 시도");
                if (!isAttacking)
                {
                    StartCoroutine(AttackCoroutine());
                }
            }
        }

    }

    IEnumerator AttackCoroutine()
    {


        Debug.Log("AttackCoroutine 호출");

        isAttacking = true;
        //nav.ResetPath();
        isChasing = false;
        anim.SetBool("Running", isChasing);

        // yield return new WaitForSeconds(0.5f);
        transform.LookAt(playerPos);
        anim.SetTrigger("Attack");
        // yield return new WaitForSeconds(0.1f);

        RaycastHit _hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, 3, targetMask))
        {
            Debug.Log("플레이어 적중!");
            _playerSystem.changeHealth(5);

        }
        else
        {
            Debug.Log("플레이어 빗나감");
        }


        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;

        Chase();

    }


    private void Dead()
    {
        isAction = false;
        isDead = true;
        isWalking = false;
        isRunning = false;
        isChasing = false;
        isAttacking = false;

        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);

        anim.SetTrigger("Death");

        Destroy(this.gameObject, 1f);
        Item();
    }

    private void Item()
    {
        int random_index = Random.Range(1, 3);
        for (int i = 0; i < random_index; i++)  //동물의 가죽
        {
            Instantiate(item20, this.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity).transform.parent = GameObject.Find("Items").transform;
        }

        random_index = Random.Range(1, 4);
        for (int i = 0; i < random_index; i++)  //부드러운 가죽
        {
            Instantiate(item21, this.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity).transform.parent = GameObject.Find("Items").transform;
        }


        random_index = Random.Range(1, 3);
        for (int i = 0; i < random_index; i++)  // 작은고기
        {
            Instantiate(item103, this.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity).transform.parent = GameObject.Find("Items").transform;
        }

        //고기
        Instantiate(item104, this.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity).transform.parent = GameObject.Find("Items").transform;

    }

}
