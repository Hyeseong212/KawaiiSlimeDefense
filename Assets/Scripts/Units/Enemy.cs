using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject vfx;

    private int wayPointCount;  // 이동 경로 갯수
    public Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; // 현재 목표지점 인덱스

    Material outlineSeleted;
    Material outlineClicked;

    NavMeshAgent navMeshAgent;

    Renderer renderers;
    List<Material> materialList = new List<Material>();
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    WaitForSeconds delay = new WaitForSeconds(0.9f);

    public EnemyData thisEnemydata;
    private void OnMouseEnter()
    {

        materialList.Clear();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Add(outlineSeleted);

        renderers.materials = materialList.ToArray();
    }

    private void OnMouseExit()
    {

        materialList.Clear();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Remove(outlineSeleted);
        materialList.Remove(outlineClicked);

        renderers.materials = materialList.ToArray();
    }
    private void OnMouseDown()
    {
        OnMouseExit();

        materialList.Clear();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Add(outlineClicked);

        renderers.materials = materialList.ToArray();
    }

    private void OnMouseUp()
    {

        materialList.Clear();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Remove(outlineSeleted);
        materialList.Remove(outlineClicked);

        renderers.materials = materialList.ToArray();
        OnMouseEnter();
    }

    void Start()
    {
        outlineSeleted = new Material(Shader.Find("Draw/OutlineShaderSelected"));
        outlineClicked = new Material(Shader.Find("Draw/OutlineShaderClicked"));
        renderers = skinnedMeshRenderer.GetComponent<Renderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        wayPointCount = EnemySpawner.i.P1wayPoints.Length;
        wayPoints = new Transform[EnemySpawner.i.P1wayPoints.Length];
        wayPoints = EnemySpawner.i.P1wayPoints; //웨이포인트 저장

        StartCoroutine("Moveto");
    }
    private void Update()
    {
        thisEnemydata = new EnemyData(thisEnemydata.wave, thisEnemydata.index, thisEnemydata.type, thisEnemydata.name, thisEnemydata.speed,
            thisEnemydata.hp, this.gameObject);
    }
    IEnumerator Moveto()
    {
        currentIndex++;
        navMeshAgent.SetDestination(wayPoints[currentIndex].transform.position);
        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].transform.position) < 0.7f) NextMoveTo();
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            currentIndex++;
            navMeshAgent.SetDestination(wayPoints[currentIndex].transform.position);
        }
        else
        {
            currentIndex = 0;
        }
    }
    public void SetActive()
    {
        vfx.SetActive(true);
        StartCoroutine("DelayedSetActiveFalse");
    }
    IEnumerator DelayedSetActiveFalse()
    {
        yield return delay;
        vfx.SetActive(false);
    }
    public void Setup(EnemyData enemydata)
    {
        thisEnemydata = enemydata;
    }
    void ToEnemyController()
    {
        for (int i = 0; i < EnemySpawner.i.enemyInThisWaveList.Count; i++)
        {
            if (thisEnemydata.index == EnemySpawner.i.enemyInThisWaveList[i].index)
            {
                EnemySpawner.i.enemyInThisWaveList[i] = thisEnemydata;
            }
        }
    }
    public void Hit()
    {
        //맞는 오브젝트 풀링 시켜서 처리할까..?
        ToEnemyController();
        if (thisEnemydata.hp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        for (int i = 0; i < EnemySpawner.i.enemyInThisWaveList.Count; i++)
        {
            if (thisEnemydata.index == EnemySpawner.i.enemyInThisWaveList[i].index && thisEnemydata.wave == EnemySpawner.i.enemyInThisWaveList[i].wave)
            {
                EnemySpawner.i.enemyInThisWaveList.Remove(EnemySpawner.i.enemyInThisWaveList[i]);
            }
        }
        Destroy(gameObject);
    }
}
