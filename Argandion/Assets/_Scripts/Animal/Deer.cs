using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer : MonoBehaviour
{
    [SerializeField] private string animalName; //동물의 이름
    [SerializeField] private int hp;

    [SerializeField] private float walkSpeed; //걷기 속도
    [SerializeField] private float runSpeed; //뛰기 속도

    private Vector3 destination;  //목적지

    //상태 변수
    private bool isHurt = false;
    private bool isAction; //행동 중인지 아닌지
    private bool isWalking; //걷는 중인지 아닌지
    private bool isRunning; //뛰는 중인지 아닌지
    private bool isDead;  //죽었는지 아닌지

    [SerializeField] private float waitTime;  //대기 시간 
    [SerializeField] private float eatTime;   //먹는 시간
    [SerializeField] private float walkTime;  //얼마 동안 걸을지
    [SerializeField] private float runTime;  //뛰는 시간
    private float currentTime;


    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider boxCol;
    private NavMeshAgent nav;
    public AudioSource _sound;
    public AudioClip attackedSound;

    //Item
    [SerializeField] private GameObject item20;  //동물의 가죽
    [SerializeField] private GameObject item22;   //거친 가죽
    [SerializeField] private GameObject item104;   //고기
    [SerializeField] private GameObject item105;   //두툼고기

    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentTime = waitTime;
        isAction = true;
    }

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
        int _random = Random.Range(0, 10); // eat, walk
        if (_random >= 0 && _random <= 1)   // 2/10 활률 
            Eat();
        else if (_random >= 2 && _random <= 9)  // 8/10 확률
            TryWalk();

    }

    private void Eat()
    {
        currentTime = eatTime;
        anim.SetTrigger("Eat");
    }

    private void TryWalk()
    {
        nav.speed = walkSpeed;
        currentTime = walkTime;
        isWalking = true;
        anim.SetBool("Walking", isWalking);
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
        if (!isDead && !isHurt)
        {
            _sound.clip = attackedSound;
            _sound.Play();
            isHurt = true;
            Invoke("NotHurt", 0.5f);
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }

            Run(_targetPos);
        }
    }

    private void NotHurt()
    {
        isHurt = false;
    }

    private void Dead()
    {
        isWalking = false;
        isRunning = false;

        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        anim.SetTrigger("Death");

        isDead = true;

        Destroy(this.gameObject, 1f);
        Item();
    }

    private void Item()
    {
        int random_index = Random.Range(1, 4);
        for (int i = 0; i < random_index; i++)  //동물의 가죽
        {
            Instantiate(item20, this.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity).transform.parent = GameObject.Find("Items").transform;
        }

        random_index = Random.Range(1, 3);
        for (int i = 0; i < random_index; i++)  //거친 가죽
        {
            Instantiate(item22, this.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity).transform.parent = GameObject.Find("Items").transform;
        }

        random_index = Random.Range(1, 3);
        for (int i = 0; i < random_index; i++)  // 고기
        {
            Instantiate(item104, this.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity).transform.parent = GameObject.Find("Items").transform;
        }

        random_index = Random.Range(1, 3);
        for (int i = 0; i < random_index; i++)  // 고기
        {
            Instantiate(item105, this.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity).transform.parent = GameObject.Find("Items").transform;
        }

    }

}
