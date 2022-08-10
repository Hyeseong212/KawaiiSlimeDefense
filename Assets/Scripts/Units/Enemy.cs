using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount;  // 이동 경로 갯수
    private Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; // 현재 목표지점 인덱스
    private Movement3D movement3D; // 오브젝트 이동제어
    Transform[] thiswaypoints;
    public void Setup(Transform[] wayPoints)
    {
        thiswaypoints = wayPoints;
        movement3D = GetComponent<Movement3D>();

        // 적 이동 경로 WayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // 적의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;

        // 적 이동/목표지점 설정 코로틴 함수 시작
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true) 
        {
            //// 적 오브젝트 회전
            //transform.Rotate(Vector3.forward * 10);

            // 적의 현재위치와 목표 위치의 거리가 0.02 * movement3D.MoveSpeed보다 작을때 if 조건문 실행
            // Tip. movement3D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에  0.02보다 크게 움직이기 때문에
            // if 조건문에 걸리지않고 경로를 탈주하는 오브젝트가 발생할 수 있다.
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement3D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }
    private void NextMoveTo()
    {
        // 아직 이동할 wayPoints가 남아있다면
        if (currentIndex < wayPointCount - 1)
        {
            transform.Rotate(new Vector3(0, -90, 0));
            //적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;
            // 이동 방향 설정 => 다음 목표지점(wayPoints)
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement3D.MoveTo(direction);
        }
        else
        {
            //적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[0].position;
            currentIndex = 0;
            // 이동 방향 설정 => 다음 목표지점(wayPoints)
            Vector3 direction = (wayPoints[0].position - transform.position).normalized;
            movement3D.MoveTo(direction);
        }
    }
}
