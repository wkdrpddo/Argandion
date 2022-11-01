using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField] private string animalName; //동물의 이름
    [SerializeField] private int hp;

    [SerializeField] private float walkSpeed; //걷기 속도
    [SerializeField] private float runSpeed; //뛰기 속도
    private float applySpeed;

    private Vector3 direction;  //방향


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

    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            Move();
            Rotation();
            ElapseTime();  
        }
    }

    private void Move()
    {
        if(isWalking || isRunning )
            rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
    }

    private void Rotation()
    {
        if(isWalking || isRunning )
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            // Debug.Log(_rotation);
            transform.rotation = Quaternion.Euler(_rotation);
            // rigid.MoveRotation(Quaternion.Euler(_rotation));
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
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0f,Random.Range(0f,360f),0f);
        Debug.Log(direction);
        RandomAction();
    }

    private void RandomAction()
    {
        isAction = true;

        int _random = Random.Range(2,3); //대기, 풀뜯기, 걷기

        if(_random == 0)
            Wait();
        else if(_random == 1)
            Eat();
        else if(_random == 2)
            TryWalk();
        
    }

    private void Wait()
    {
        currentTime = waitTime;
        anim.SetTrigger("Idle");
        Debug.Log("대기");
    }

    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("풀뜯기");
    }

    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        applySpeed = walkSpeed;
        Debug.Log("걷기");
    }

    private void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
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
                Debug.Log("죽음");
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


        //죽는 애니메이션(없어서 회전시키기)
        Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, 0f, -90f), 0.9f);

        transform.rotation = Quaternion.Euler(_rotation);

        isDead = true;
    }

}
