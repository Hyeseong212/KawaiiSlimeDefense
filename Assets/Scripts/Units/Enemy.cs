using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount;  // �̵� ��� ����
    private Transform[] wayPoints; // �̵� ��� ����
    private int currentIndex = 0; // ���� ��ǥ���� �ε���
    private Movement3D movement3D; // ������Ʈ �̵�����
    Transform[] thiswaypoints;
    public void Setup(Transform[] wayPoints)
    {
        thiswaypoints = wayPoints;
        movement3D = GetComponent<Movement3D>();

        // �� �̵� ��� WayPoints ���� ����
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // ���� ��ġ�� ù��° wayPoint ��ġ�� ����
        transform.position = wayPoints[currentIndex].position;

        // �� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ� ����
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true) 
        {
            //// �� ������Ʈ ȸ��
            //transform.Rotate(Vector3.forward * 10);

            // ���� ������ġ�� ��ǥ ��ġ�� �Ÿ��� 0.02 * movement3D.MoveSpeed���� ������ if ���ǹ� ����
            // Tip. movement3D.MoveSpeed�� �����ִ� ������ �ӵ��� ������ �� �����ӿ�  0.02���� ũ�� �����̱� ������
            // if ���ǹ��� �ɸ����ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� �ִ�.
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement3D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }
    private void NextMoveTo()
    {
        // ���� �̵��� wayPoints�� �����ִٸ�
        if (currentIndex < wayPointCount - 1)
        {
            transform.Rotate(new Vector3(0, -90, 0));
            //���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[currentIndex].position;
            // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement3D.MoveTo(direction);
        }
        else
        {
            //���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[0].position;
            currentIndex = 0;
            // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
            Vector3 direction = (wayPoints[0].position - transform.position).normalized;
            movement3D.MoveTo(direction);
        }
    }
}
