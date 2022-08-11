using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator animator;
    [SerializeField] Transform tr;
    List<Enemy> enemies;
    GameObject enemy;
    float distance;
    public void Shootfunc()
    {
        StopCoroutine("Shoot");
        StartCoroutine("Shoot");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Enemy")
        {
            distance = Vector3.Distance(this.transform.position, other.transform.position);
            //�������� ���⶧�� �����ϰԲ�
            enemy = other.gameObject;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (animator.GetInteger("MoveInt") != 1)
            {
                animator.SetInteger("MoveInt", 2);
            }
        }

    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (animator.GetInteger("MoveInt") != 1)
            {
                if (Vector3.Distance(this.transform.position, enemy.transform.position) < distance)
                {
                    agent.
                    GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().Setup(enemy);
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
