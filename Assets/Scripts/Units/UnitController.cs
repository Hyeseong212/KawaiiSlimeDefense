using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
	[SerializeField]
	private	GameObject		unitMarker;
	private	NavMeshAgent	navMeshAgent;
    public Face faces;
    Transform slimeVector;
    [SerializeField] Animator animator;
    public GameObject mainSlime;
    private Material faceMaterial;

    float distance;
    float remainTime;

    public SlimeData slimedata;

    public Shooter shooter;
    GameObject enemy;

    public bool isWaitForCommand;

    GraphicRaycaster m_gr;
    PointerEventData m_ped;
    Canvas m_canvas;

    int m_index;
    //public Button idleBut, walkBut, jumpBut, attackBut, damageBut0, damageBut1, damageBut2;
    private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

    private void Start()
    {
        slimeVector = GetComponent<Transform>();
        shooter = GetComponentInChildren<Shooter>();
        for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
        {
            if (CraftManager.i.currentSceneSlimeData[i].Slime == this.gameObject)
            {
                slimedata = CraftManager.i.currentSceneSlimeData[i];
            }
        }
        SetupData(slimedata);
        m_canvas = GameObject.Find("PCUI(Canvas)").GetComponent<Canvas>();
        m_gr = m_canvas.GetComponent<GraphicRaycaster>();
        m_ped = new PointerEventData(null);
        //idleBut.onClick.AddListener(delegate { Idle(); });
        //walkBut.onClick.AddListener(delegate { ChangeStateTo(SlimeAnimationState.Walk); });
        //jumpBut.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Jump); });
        //attackBut.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Attack); });
        //damageBut0.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Damage); mainSlime.GetComponent<EnemyAi>().damType = 0; });
        //damageBut1.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Damage); mainSlime.GetComponent<EnemyAi>().damType = 1; });
        //damageBut2.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Damage); mainSlime.GetComponent<EnemyAi>().damType = 2; });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            shooter.status = SlimeStatus.Hold;
            UnitControllerPanel.i.UnitStatusInPanel();
            Stop();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            shooter.status = SlimeStatus.Stop;
            UnitControllerPanel.i.UnitStatusInPanel();
            Stop();
        }
        if (isWaitForCommand)
        {
            if (shooter.status == SlimeStatus.ForcedMove || shooter.status == SlimeStatus.ForcedAttack)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    m_ped.position = Input.mousePosition;
                    List<RaycastResult> results = new List<RaycastResult>();//리팩토링때 건드려야할 코드
                    m_gr.Raycast(m_ped, results);
                    // 유닛 오브젝트(layerUnit)를 클릭했을 때
                    if (results.Count == 1)
                    {
                        if (results[0].gameObject.name == "map")
                        {
                            return;
                        }
                    }
                    else if (results.Count == 0)
                    {
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Ground")))
                        {
                            RTSUnitController.i.MoveSelectedUnits(hit.point);
                        }
                    }
                }
            }
        }
    }
    void StopToWall()
    {

    }
    
    public void SelectUnit()
	{
		unitMarker.SetActive(true);
	}

	public void DeselectUnit()
	{
		unitMarker.SetActive(false);
	}

	public void MoveTo(Vector3 end)
	{
        UnitControllerPanel.i.UnitStatusInPanel();
        StopCoroutine("StopCheck");
        navMeshAgent.isStopped = false;
        animator.SetInteger("MoveInt",1);
        navMeshAgent.SetDestination(end);
        distance  = Vector3.Distance(slimeVector.position ,end);
        remainTime = distance/ navMeshAgent.speed;
        StartCoroutine("StopCheck");
    }
    IEnumerator StopCheck()
    {
        yield return new WaitForSeconds(remainTime);
        isWaitForCommand = false;
        animator.SetInteger("MoveInt", 0);
        navMeshAgent.isStopped = true;
        if (shooter.status == SlimeStatus.Hold) 
        {
            shooter.status = SlimeStatus.Hold;
            UnitControllerPanel.i.UnitStatusInPanel();
        }
        else if(shooter.status == SlimeStatus.ForcedAttack)
        {
            shooter.status = SlimeStatus.ForcedAttack;
            UnitControllerPanel.i.UnitStatusInPanel();
        }
        else
        {
            shooter.status = SlimeStatus.Stop;
            UnitControllerPanel.i.UnitStatusInPanel();
        }
    }

    public void Stop()
    {
        animator.SetInteger("MoveInt", 0);
        navMeshAgent.isStopped = true;
    }
    public void SetupData(SlimeData slimedata)
    {
        shooter.animator.SetFloat("attackspeed",slimedata.attackspeed);
        this.slimedata = slimedata;
    }

    public void DestroyThisSlime(GameObject thisObject)
    {
        for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++) 
        {
            if (thisObject == CraftManager.i.currentSceneSlimeData[i].Slime) 
            {
                CraftManager.i.currentSceneSlimeData.RemoveAt(i);
            }
        }
    }
}

