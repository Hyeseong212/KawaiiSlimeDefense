using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SlimeStatus { Move, Hold, Attack, Stop, ForcedAttack, ForcedMove}
public class Shooter : MonoBehaviour
{
    UnitController unitController;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject shootPrefab;
    [SerializeField] public Animator animator;
    [SerializeField] Transform tr;
    public List<EnemyData> enemies;
    float radius;
    float distanceBetweenEnemy;
    SphereCollider sphereCollider;

    public SlimeStatus status;

    public EnemyData targetedEnemy;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        unitController = GetComponentInParent<UnitController>();
        radius = sphereCollider.radius;
        enemies = new List<EnemyData>();
        status = SlimeStatus.Stop;
    }
    private void Update()
    {
        if (targetedEnemy.enemyObject != null)
        {
            if (!targetedEnemy.enemyObject.GetComponent<Enemy>().isDead)
            {
                if (status != SlimeStatus.Hold) //스탑버튼 눌렀을때
                {
                    //distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[0].enemyObject.transform.position);
                    distanceBetweenEnemy = Vector3.Distance(transform.position, targetedEnemy.enemyObject.transform.position);
                    if (distanceBetweenEnemy > radius && status != SlimeStatus.ForcedMove)// 사정거리 밖에 있을때 status가 강제이동이 아닐때
                    {
                        //unitController.MoveTo(enemies[0].enemyObject.transform.position);
                        unitController.MoveTo(targetedEnemy.enemyObject.transform.position);
                    }
                    else if (distanceBetweenEnemy <= radius && status != SlimeStatus.ForcedMove)// 사정거리 안에 있을때 status가 강제이동이 아닐때
                    {
                        unitController.Stop();
                        animator.SetInteger("MoveInt", 2);
                        //this.transform.LookAt(enemies[0].enemyObject.transform);
                        unitController.transform.LookAt(targetedEnemy.enemyObject.transform);
                    }
                }
                if (status == SlimeStatus.Hold) //홀드버튼 눌렀을때
                {
                    //distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[enemies.Count-1].enemyObject.transform.position);
                    distanceBetweenEnemy = Vector3.Distance(transform.position, targetedEnemy.enemyObject.transform.position);
                    if (distanceBetweenEnemy <= radius && status != SlimeStatus.ForcedMove)// 사정거리 안에 있을때 status가 강제이동이 아닐때
                    {
                        unitController.Stop();
                        animator.SetInteger("MoveInt", 2);
                        unitController.transform.LookAt(targetedEnemy.enemyObject.transform);
                    }
                    else if (distanceBetweenEnemy > radius && status != SlimeStatus.ForcedMove) // 사정거리 밖에 있을때 status가 강제이동이 아닐때
                    {
                        animator.SetInteger("MoveInt", 0);
                    }
                }
            }
            else if (status != SlimeStatus.ForcedMove && status != SlimeStatus.ForcedAttack)
            {
                animator.SetInteger("MoveInt", 0);
            }
        }
        else if (status != SlimeStatus.ForcedMove && status != SlimeStatus.ForcedAttack)
        {
            animator.SetInteger("MoveInt", 0);
        }
    }
    public void Shootfunc()//코루틴 트리거
    {
        Shoot();
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!other.gameObject.GetComponent<Enemy>().isDead)
            {
                if (status != SlimeStatus.ForcedAttack)
                {
                    targetedEnemy = other.gameObject.GetComponent<Enemy>().thisEnemydata;
                }
            }
        }
    }
    private void Shoot()
    {
        if (animator.GetInteger("MoveInt") != 1)
        {
            if (enemies.Count != 0 || targetedEnemy.pos != Vector3.zero)
            {
                if (targetedEnemy.enemyObject != null && !targetedEnemy.enemyObject.GetComponent<Enemy>().isDead)
                {
                    if (status != SlimeStatus.Hold)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
                        Instantiate(shootPrefab, this.transform.position, this.transform.rotation);
                        bullet.GetComponent<Bullet>().Setup(targetedEnemy.enemyObject, unitController.slimedata);
                    }
                    else if (status == SlimeStatus.Hold)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
                        GameObject flash = Instantiate(shootPrefab, this.transform.position, this.transform.rotation);
                        bullet.GetComponent<Bullet>().Setup(targetedEnemy.enemyObject, unitController.slimedata);
                    }
                }
            }
        }
    }
}
