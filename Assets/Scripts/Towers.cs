using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    /// <summary>
    /// 다른 스크립트에서 타워 데이터 목록에 쉽게 접근할 수 있도록 하는 싱글톤 인스턴스
    /// </summary>
    public static Towers instance;

    /// <summary>
    /// 게임에 존재하는 모든 타워의 데이터 목록
    /// </summary>
    public List<TowerData> towerDatas;

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 필요에 따라 타워 이름으로 특정 타워 데이터를 찾는 함수
    /// </summary>
    public TowerData GetTowerData(string towerName)
    {
        return towerDatas.Find(data => data.towerName == towerName);
    }
}

