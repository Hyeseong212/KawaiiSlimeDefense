using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SlimeStatus { Move, Hold, Attack, Stop, ForcedAttack, ForcedMove}
public class Shooter : MonoBehaviour
{
    UnitController unitController;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Animator animator;
    [SerializeField] Transform tr;
    public List<GameObject> enemies;
    public GameObject enemy;
    public float radius;
    public float distanceBetweenEnemy;
    public SphereCollider sphereCollider;

    public SlimeStatus status;

    [SerializeField]
    private LayerMask layerEnemy;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        unitController = GetComponentInParent<UnitController>();
        radius = sphereCollider.radius*2;
        enemies = new List<GameObject>();
        status = SlimeStatus.Stop;

    }
    private void Update()
    {
        if (enemies.Count != 0 &&  status == SlimeStatus.Stop || status == SlimeStatus.Move 
            || status == SlimeStatus.Attack || status == SlimeStatus.ForcedAttack) //스탑버튼 눌렀을때
        {
            distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[0].transform.position);
            if(distanceBetweenEnemy > radius && status != SlimeStatus.ForcedMove)// 사정거리 밖에 있을때 status가 강제이동이 아닐때
            {
                unitController.MoveTo(enemies[0].transform.position);
            }
            if(distanceBetweenEnemy <= radius && status != SlimeStatus.ForcedMove)// 사정거리 안에 있을때 status가 강제이동이 아닐때
            {
                unitController.Stop();
                animator.SetInteger("MoveInt", 2);
            }
        } 
        else if(enemies.Count != 0 && status == SlimeStatus.Hold) //홀드버튼 눌렀을때
        {
            distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[0].transform.position);
            if (distanceBetweenEnemy < radius && status != SlimeStatus.ForcedMove)// 사정거리 안에 있을때 status가 강제이동이 아닐때
            {
                unitController.Stop();
                animator.SetInteger("MoveInt", 2);
            }
            else if(distanceBetweenEnemy >= radius && status != SlimeStatus.ForcedMove) // 사정거리 밖에 있을때 status가 강제이동이 아닐때
            {
                animator.SetInteger("MoveInt", 0);
            }
        }
    }
    public void Shootfunc()//코루틴 트리거
    {
        StopCoroutine("Shoot");
        StartCoroutine("Shoot");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (status != SlimeStatus.ForcedAttack && status != SlimeStatus.Hold)
            {
                //움직임을 멈출때만 공격하게끔
                enemy = other.gameObject;
                enemies.Insert(0, enemy);
            }
            else if(status == SlimeStatus.Hold)
            {
                enemy = other.gameObject;
                enemies.Add(enemy);
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (status != SlimeStatus.ForcedAttack || status != SlimeStatus.ForcedMove)
            {
                if (animator.GetInteger("MoveInt") != 1)
                {
                    if (enemies.Count != 0)
                    {
                        if (Vector3.Distance(this.transform.position, enemies[0].transform.position) < radius)
                        {
                            unitController.gameObject.transform.LookAt(enemies[0].transform);
                            animator.SetInteger("MoveInt", 2);
                        }
                    }
                }
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (status != SlimeStatus.ForcedAttack)
            {
                if (enemies.Count != 0)
                {
                    enemies.RemoveAt(0);
                }
            }
        }
    }
    private IEnumerator Shoot()//공격코루틴
    {
        while (true)
        {
            if (animator.GetInteger("MoveInt") != 1)
            {
                if (enemies.Count != 0)
                {
                    if (Vector3.Distance(this.transform.position, enemies[0].transform.position) < radius)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().Setup(enemies[0]);
                        //해당오브젝트의 슬라임 명으로 공속 가져오셈
                        Debug.Log(unitController.slimedata.attackspeed);
                        yield return new WaitForSeconds(unitController.slimedata.attackspeed);
                    }
                    else
                    {
                        yield return null;
                    }
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
