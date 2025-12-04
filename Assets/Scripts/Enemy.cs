using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 적의 최대 체력
    /// </summary>
    public float maxHealth = 100f;

    /// <summary>
    /// 현재 체력
    /// </summary>
    private float currentHealth;

    void Awake()
    {
        // 시작 시 현재 체력을 최대 체력으로 설정합니다.
        currentHealth = maxHealth;
    }

    /// <summary>
    /// 외부(포탑 등)에서 호출하여 적에게 데미지를 줍니다.
    /// </summary>
    /// <param name="damage">입힐 데미지 양</param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // 체력이 0 이하가 되면 사망 처리
        if (currentHealth <= 0)
        {
            // TODO: 여기에 골드 획득, 파괴 이펙트 생성 등의 코드를 추가할 수 있습니다.
            Destroy(gameObject);
        }
    }
}

