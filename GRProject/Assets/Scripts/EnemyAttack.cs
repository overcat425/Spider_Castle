using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { None = -1, Idle = 0, Wander, Chasing, }
public class EnemyAttack : MonoBehaviour
{
    [Header("Chasing")]
    [SerializeField]
    private float targetRecognition = 100;          // 타겟 인식 거리
    [SerializeField]
    private float ChasingLimitRange = 100;          // 추적 제한 거리

    private EnemyState enemyAttack = EnemyState.Chasing;
    private PlayerStatus status;                           // 이동 정보
    private NavMeshAgent navMeshAgent;              // 이동 네비게이션
    private Transform target;                               // 적의 공격 대상 (플레이어)
    private EnemyMemoryPool enemyMemoryPool;
    
    public void Setup(Transform target, EnemyMemoryPool enemyMemoryPool)
    {
        status = GetComponent<PlayerStatus>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        this.target = target;
        this.enemyMemoryPool = enemyMemoryPool;
        navMeshAgent.updateRotation = false;            //nvmh 컴포에서 회전 업뎃 x
    }
    public void Start()
    {
        ChangeState(EnemyState.Chasing);
    }
    public void ChangeState(EnemyState newState)
    {
        if (enemyAttack == newState) return;
        StopCoroutine(enemyAttack.ToString());
        enemyAttack = newState;
        StartCoroutine(enemyAttack.ToString());
    }
    private IEnumerator Pursuit()
    {
        while (true)
        {
            navMeshAgent.speed = status.RunSpeed;
            navMeshAgent.SetDestination(target.position);
            LookRotationToTarget();
            yield return null;
        }
    }
    private void LookRotationToTarget()             // 추적 시 플레이어 바라보기
    {
        Vector3 to = new Vector3(target.position.x, 0, target.position.z);
        Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);

        transform.rotation = Quaternion.LookRotation(to - from);
    }
}
