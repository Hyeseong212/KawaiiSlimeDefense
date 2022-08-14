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
    public List<EnemyData> enemies;
    public EnemyData enemy;
    public float radius;
    public float distanceBetweenEnemy;
    public SphereCollider sphereCollider;

    public SlimeStatus status;

    [SerializeField]
    private LayerMask layerEnemy;

    public EnemyData targetedEnemy;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        unitController = GetComponentInParent<UnitController>();
        radius = sphereCollider.radius*2;
        enemies = new List<EnemyData>();
        status = SlimeStatus.Stop;

    }
    private void Update()
    {
        if (enemies.Count != 0 &&  status == SlimeStatus.Stop || status == SlimeStatus.Move 
            || status == SlimeStatus.Attack || status == SlimeStatus.ForcedAttack) //스탑버튼 눌렀을때
        {
            distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[0].enemyObject.transform.position);
            //distanceBetweenEnemy = Vector3.Distance(transform.position, targetedEnemy.enemyObject.transform.position);
            if (distanceBetweenEnemy > radius && status != SlimeStatus.ForcedMove)// 사정거리 밖에 있을때 status가 강제이동이 아닐때
            {
                unitController.MoveTo(enemies[0].enemyObject.transform.position);
            }
            if(distanceBetweenEnemy <= radius && status != SlimeStatus.ForcedMove)// 사정거리 안에 있을때 status가 강제이동이 아닐때
            {
                unitController.Stop();
                animator.SetInteger("MoveInt", 2);
            }
        } 
        else if(targetedEnemy.enemyObject != null && status == SlimeStatus.Hold) //홀드버튼 눌렀을때
        {
            //distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[enemies.Count-1].enemyObject.transform.position);
            distanceBetweenEnemy = Vector3.Distance(transform.position, targetedEnemy.enemyObject.transform.position);
            if (distanceBetweenEnemy <= radius && status != SlimeStatus.ForcedMove)// 사정거리 안에 있을때 status가 강제이동이 아닐때
            {
                unitController.Stop();
                animator.SetInteger("MoveInt", 2);
            }
            else if(distanceBetweenEnemy > radius && status != SlimeStatus.ForcedMove) // 사정거리 밖에 있을때 status가 강제이동이 아닐때
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
                enemy = other.gameObject.GetComponent<Enemy>().thisEnemydata;
                enemies.Add(enemy);
            }
            else if (status != SlimeStatus.ForcedAttack && status == SlimeStatus.Hold) //홀드시
            {
                enemy = other.gameObject.GetComponent<Enemy>().thisEnemydata;
                enemies.Add(enemy);
                if(targetedEnemy.index != other.gameObject.GetComponent<Enemy>().thisEnemydata.index)
                //들어온적과 타겟된적의 인덱스가 같지않을시
                {
                    targetedEnemy = enemy;
                }
                else if (targetedEnemy.index == other.gameObject.GetComponent<Enemy>().thisEnemydata.index)
                //들어온적과 타겟된적의 인덱스가 같을시
                {
                    targetedEnemy = targetedEnemy;
                }
                else
                {
                    targetedEnemy = enemy;
                }
            }
            //if (status != SlimeStatus.ForcedAttack && status != SlimeStatus.Hold)
            //{
            //    //움직임을 멈출때만 공격하게끔
            //    enemy = other.gameObject.GetComponent<Enemy>().thisEnemydata;
            //    enemies.Insert(0, enemy);
            //}
            //else if(status == SlimeStatus.Hold)
            //{
            //    enemy = other.gameObject.GetComponent<Enemy>().thisEnemydata;
            //    enemies.Add(enemy);
            //}
        }
    }
    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        if (status != SlimeStatus.ForcedAttack || status != SlimeStatus.ForcedMove)
    //        {
    //            if (animator.GetInteger("MoveInt") != 1)
    //            {
    //                if (enemies.Count != 0)
    //                {
    //                    if (distanceBetweenEnemy < radius)
    //                    {
    //                        unitController.gameObject.transform.LookAt(enemies[0].enemyObject.transform);
    //                        animator.SetInteger("MoveInt", 2);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (status != SlimeStatus.ForcedAttack && status != SlimeStatus.Hold)
            {
                if (enemies.Count != 0)
                {
                    enemies.RemoveAt(0);
                }
            }
            else if (status != SlimeStatus.ForcedAttack && status == SlimeStatus.Hold) //홀드시
            {
                if (targetedEnemy.enemyObject != null && targetedEnemy.index == other.gameObject.GetComponent<Enemy>().thisEnemydata.index)
                    //나간적이 공격하고있는적 일시
                {
                    enemies.RemoveAt(0);
                    targetedEnemy = enemies[enemies.Count - 1];
                }
                else if(targetedEnemy.enemyObject != null && targetedEnemy.index != other.gameObject.GetComponent<Enemy>().thisEnemydata.index)
                    //나간적이 공격하고 있는적이 아닐시
                {
                    enemies.RemoveAt(0);
                }
            }
            //if (status != SlimeStatus.ForcedAttack && status != SlimeStatus.Hold)
            //{
            //    if (enemies.Count != 0)
            //    {
            //        enemies.RemoveAt(0);
            //    }
            //}
            //else if(status != SlimeStatus.ForcedAttack && status == SlimeStatus.Hold)
            //{
            //    if (enemies.Count != 0)
            //    {
            //        enemies.RemoveAt(0);
            //        if (enemies.Count - 1 != -1)
            //        {
            //            enemies.Insert(0, enemies[enemies.Count - 1]);
            //            enemies.RemoveAt(enemies.Count - 1);
            //        }
            //    }
            //}
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
                    if (status != SlimeStatus.Hold)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().Setup(enemies[0].enemyObject);
                        yield return new WaitForSeconds(unitController.slimedata.attackspeed);
                    }
                    else if(status == SlimeStatus.Hold)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().Setup(targetedEnemy.enemyObject);
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
