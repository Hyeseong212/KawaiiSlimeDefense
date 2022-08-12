using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shooter : MonoBehaviour
{
    UnitController unitController;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator animator;
    [SerializeField] Transform tr;
    public List<GameObject> enemies;
    public GameObject enemy;
    public float radius;
    public float distanceBetweenEnemy;
    public SphereCollider sphereCollider;

    [SerializeField]
    private LayerMask layerEnemy;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        unitController = GetComponentInParent<UnitController>();
        radius = sphereCollider.radius;
        enemies = new List<GameObject>();
    }
    private void Update()
    {
        if (enemies.Count != 0 && !unitController.isMove) 
        {
            distanceBetweenEnemy = Vector3.Distance(transform.position, enemies[0].transform.position);
            if(distanceBetweenEnemy > radius)
            {
                unitController.MoveTo(enemies[0].transform.position);
            }
            else if(distanceBetweenEnemy < radius)
            {
                unitController.Stop();
                animator.SetInteger("MoveInt", 2);
            }
        } 
    }
    public void Shootfunc()//�ڷ�ƾ Ʈ����
    {
        StopCoroutine("Shoot");
        StartCoroutine("Shoot");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Enemy")
        {
            //�������� ���⶧�� �����ϰԲ�
            enemy = other.gameObject;
            enemies.Add(enemy);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (animator.GetInteger("MoveInt") != 1)
            {
                if (Vector3.Distance(this.transform.position, enemies[0].transform.position) < radius)
                {
                    animator.SetInteger("MoveInt", 2);
                }
            }
        }

    }

    private IEnumerator Shoot()//�����ڷ�ƾ
    {
        while (true)
        {
            if (animator.GetInteger("MoveInt") != 1)
            {
                if (Vector3.Distance(this.transform.position, enemies[0].transform.position) < radius)
                {
                    GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().Setup(enemies[0]);
                    //�ش������Ʈ�� ������ ������ ���� ��������
                    yield return new WaitForSeconds(1f);
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
