using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [Header("타워 능력치")]
    [Tooltip("포탑의 공격 범위")]
    public float attackRange = 15f;
    [Tooltip("초당 공격 횟수")]
    public float fireRate = 1f;
    [Tooltip("공격력")]
    public float damage = 25f;

    [Header("Unity 설정")]
    [Tooltip("공격할 대상의 태그")]
    public string enemyTag = "Enemy";

    private List<Transform> enemiesInRange = new List<Transform>();
    private Transform currentTarget;
    private float fireCooldown = 0f;

    void Awake()
    {
        // 공격 범위를 감지하기 위한 SphereCollider 설정
        // 만약 SphereCollider가 이미 존재한다면 그것을 사용하고, 없다면 새로 추가합니다.
        SphereCollider rangeCollider = GetComponent<SphereCollider>();
        if (rangeCollider == null)
        {
            rangeCollider = gameObject.AddComponent<SphereCollider>();
        }
        rangeCollider.isTrigger = true;
        rangeCollider.radius = attackRange;
    }

    void Update()
    {
        // 매 프레임마다 타겟을 찾고 공격을 시도합니다.
        FindTarget();
        AttackTarget();
    }

    void OnTriggerEnter(Collider other)
    {
        // 새로운 적이 범위에 들어오면 목록에 추가합니다.
        if (other.CompareTag(enemyTag))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 적이 범위를 벗어나면 목록에서 제거합니다.
        if (other.CompareTag(enemyTag))
        {
            enemiesInRange.Remove(other.transform);
            // 만약 현재 타겟이 범위를 벗어났다면, 타겟을 초기화합니다.
            if (currentTarget == other.transform)
            {
                currentTarget = null;
            }
        }
    }

    void FindTarget()
    {
        // 목록에 있는 비활성화된(죽은) 적들을 제거하여 리스트를 정리합니다.
        enemiesInRange.RemoveAll(enemy => enemy == null);

        // 현재 타겟이 없거나, 비활성화 되었다면 새로운 타겟을 찾습니다.
        if (currentTarget == null && enemiesInRange.Count > 0)
        {
            // 목록의 첫 번째 적을 현재 타겟으로 설정합니다.
            // TODO: 가장 가까운 적, 체력이 가장 높은 적 등 다양한 타겟팅 로직을 구현할 수 있습니다.
            currentTarget = enemiesInRange.First();
        }
    }

    void AttackTarget()
    {
        // 공격 쿨다운을 계산합니다.
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        // 타겟이 존재하고 공격 쿨다운이 끝났다면 공격합니다.
        if (currentTarget != null && fireCooldown <= 0f)
        {
            // 포탑이 타겟을 바라보도록 합니다. (선택적)
            // TODO: 포탑의 머리(TurretHead) 부분만 타겟을 바라보도록 수정하면 더 자연스럽습니다.
            transform.LookAt(currentTarget);

            // 타겟에게 데미지를 줍니다.
            Enemy enemyComponent = currentTarget.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(damage);
                Debug.Log("공격!");
            }
            
            // TODO: 여기에 총알 발사 이펙트나 사운드를 추가할 수 있습니다.

            // 다음 공격을 위해 쿨다운을 재설정합니다.
            fireCooldown = 1f / fireRate;
        }
    }
}

