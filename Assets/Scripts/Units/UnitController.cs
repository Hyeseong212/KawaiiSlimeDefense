using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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

    Shooter shooter;
    GameObject enemy;
    //public Button idleBut, walkBut, jumpBut, attackBut, damageBut0, damageBut1, damageBut2;
    private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

    private void Start()
    {
        slimeVector = GetComponent<Transform>();
        faceMaterial = mainSlime.GetComponent<Renderer>().materials[1];
        shooter = GetComponentInChildren<Shooter>();
        for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
        {
            if (CraftManager.i.currentSceneSlimeData[i].Slime == this.gameObject)
            {
                slimedata = CraftManager.i.currentSceneSlimeData[i];
            }
        }
        SetupData(slimedata);
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
            Stop();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            shooter.status = SlimeStatus.Stop;
            Stop();
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
        StopCoroutine("StopCheck");
        navMeshAgent.isStopped = false;
        animator.SetInteger("MoveInt",1);
        SetFace(faces.WalkFace);
        navMeshAgent.SetDestination(end);
        distance  = Vector3.Distance(slimeVector.position ,end);
        remainTime = distance/ navMeshAgent.speed;
        StartCoroutine("StopCheck");
    }
    IEnumerator StopCheck()
    {
        yield return new WaitForSeconds(remainTime);
        animator.SetInteger("MoveInt", 0);
        navMeshAgent.isStopped = true;
        if (shooter.status == SlimeStatus.Hold) 
        {
            shooter.status = SlimeStatus.Hold;
        }
        else if(shooter.status == SlimeStatus.ForcedAttack)
        {
            shooter.status = SlimeStatus.ForcedAttack;
        }
        else
        {
            shooter.status = SlimeStatus.Stop;
        }
    }
    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
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
}

