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
            if (status != SlimeStatus.Hold) //��ž��ư ��������
            {
                //distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[0].enemyObject.transform.position);
                distanceBetweenEnemy = Vector3.Distance(transform.position, targetedEnemy.enemyObject.transform.position);
                if (distanceBetweenEnemy > radius && status != SlimeStatus.ForcedMove)// �����Ÿ� �ۿ� ������ status�� �����̵��� �ƴҶ�
                {
                    //unitController.MoveTo(enemies[0].enemyObject.transform.position);
                    unitController.MoveTo(targetedEnemy.enemyObject.transform.position);
                }
                if (distanceBetweenEnemy <= radius && status != SlimeStatus.ForcedMove)// �����Ÿ� �ȿ� ������ status�� �����̵��� �ƴҶ�
                {
                    unitController.Stop();
                    animator.SetInteger("MoveInt", 2);
                    //this.transform.LookAt(enemies[0].enemyObject.transform);
                    unitController.transform.LookAt(targetedEnemy.enemyObject.transform);
                }
            }
            else if (targetedEnemy.enemyObject != null && status == SlimeStatus.Hold) //Ȧ���ư ��������
            {
                //distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[enemies.Count-1].enemyObject.transform.position);
                distanceBetweenEnemy = Vector3.Distance(transform.position, targetedEnemy.enemyObject.transform.position);
                if (distanceBetweenEnemy <= radius && status != SlimeStatus.ForcedMove)// �����Ÿ� �ȿ� ������ status�� �����̵��� �ƴҶ�
                {
                    unitController.Stop();
                    animator.SetInteger("MoveInt", 2);
                    unitController.transform.LookAt(targetedEnemy.enemyObject.transform);
                }
                else if (distanceBetweenEnemy > radius && status != SlimeStatus.ForcedMove) // �����Ÿ� �ۿ� ������ status�� �����̵��� �ƴҶ�
                {
                    animator.SetInteger("MoveInt", 0);
                }
            }
        }
        else
        {
            animator.SetInteger("MoveInt", 0);
        }
        Debug.Log(status);
    }
    public void Shootfunc()//�ڷ�ƾ Ʈ����
    {
        Shoot();
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (status != SlimeStatus.ForcedAttack)
            {
                targetedEnemy = other.gameObject.GetComponent<Enemy>().thisEnemydata;
            }
        }
    }
    private void Shoot()
    {
        if (animator.GetInteger("MoveInt") != 1)
        {
            if (enemies.Count != 0 || targetedEnemy.pos != Vector3.zero)
            {
                if (targetedEnemy.enemyObject != null)
                {
                    if (status != SlimeStatus.Hold)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.LookRotation(targetedEnemy.pos));
                        Instantiate(shootPrefab, this.transform.position, Quaternion.LookRotation(targetedEnemy.pos));
                        bullet.GetComponent<Bullet>().Setup(targetedEnemy.enemyObject, unitController.slimedata);
                    }
                    else if (status == SlimeStatus.Hold)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.LookRotation(targetedEnemy.pos));
                        Instantiate(shootPrefab, this.transform.position, Quaternion.LookRotation(targetedEnemy.pos));
                        bullet.GetComponent<Bullet>().Setup(targetedEnemy.enemyObject, unitController.slimedata);
                    }
                }
            }
        }
        
    }
}