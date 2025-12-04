using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Data", menuName = "Tower Defense/Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("타워 정보")]
    public string towerName;
    public GameObject towerPrefab;
    public int cost;

    [Header("타워 능력치")]
    [Tooltip("포탑의 공격 범위")]
    public float attackRange = 15f;

    [Tooltip("초당 공격 횟수")]
    public float fireRate = 1f;

    [Tooltip("공격력")]
    public float damage = 25f;
}

