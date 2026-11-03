 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("要生成的敌人预制体")]
    public GameObject EnemyToSpawn;//敌人种类

    [Tooltip("刷怪间隔s")]
    public float TimeToSpawn;//计数器 
    private float SpawnCounter;//刷怪频率

    public Transform MaxSpawn, MinSpawn;//最大生成点，最小生成点

    private float DespawnDistance;//敌人离玩家距离

    public List<GameObject> SpawnedEnemies = new List<GameObject>();//怪物列表

    public int CheckPerFrme = 5;//每帧检查
    private int EnemyToCheck;//每帧敌人数量

    private Transform playerTarget;

    // Start is called before the first frame update
    void Start()
    {

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("【EnemySpawner】场景中未找到PlayerController组件！", this);
            enabled = false;
            return;
        }
        playerTarget = player.transform;

        SpawnCounter = TimeToSpawn;

        
        DespawnDistance = Vector3.Distance(playerTarget.position, MaxSpawn.position) + 5f;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCounter -= Time.deltaTime;
        if (SpawnCounter <= 0)//生成敌人
        {

            SpawnCounter = TimeToSpawn;//重置刷怪时间
                                       // Vector3 rad = new Vector3(Random.Range(0, 15), Random.Range(0, 15));
            GameObject newObject = Instantiate(EnemyToSpawn, SelectSpawnPoint(), transform.rotation);

            SpawnedEnemies.Add(newObject);
        }

        int CheckTarget = EnemyToCheck + CheckPerFrme;//检查每帧敌人数量

        while (EnemyToCheck < CheckTarget)//判断是否开启检查
        {
            if (EnemyToCheck < SpawnedEnemies.Count)//判断检查是否开始
            {
                if (SpawnedEnemies[EnemyToCheck] != null)//检查是否为空
                {
                    if (Vector3.Distance(playerTarget.position, SpawnedEnemies[EnemyToCheck].transform.position) > DespawnDistance)//判断敌人与玩家间的距离
                    {
                        Destroy(SpawnedEnemies[EnemyToCheck]);//摧毁该敌人

                        SpawnedEnemies.RemoveAt(EnemyToCheck);//移除列表中的敌人
                        CheckTarget--;//减少敌人数量
                    }
                    else
                    {
                        EnemyToCheck++;//恢复该敌人的数量
                    }
                }
                else
                {
                    SpawnedEnemies.RemoveAt(EnemyToCheck);
                    CheckTarget--;
                }
            }
            else
            {
                EnemyToCheck = 0;//重置检查数
                CheckTarget = 0;//重置待检查数
            }
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 SpawnPoint = Vector3.zero;

        bool SpawnVerticalEdge = Random.Range(0f, 1f) > .5f;

        if (SpawnVerticalEdge)
        {
            SpawnPoint.y = Random.Range(MinSpawn.position.y, MaxSpawn.position.y);

            if (Random.Range(0f, 1f) > .5f)
            {
                SpawnPoint.x = MaxSpawn.position.x;
            }
            else
            {
                SpawnPoint.x = MinSpawn.position.x;
            }
        }
        else
        {
            SpawnPoint.x = Random.Range(MinSpawn.position.x, MaxSpawn.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                SpawnPoint.y = MaxSpawn.position.y;
            }
            else
            {
                SpawnPoint.y = MinSpawn.position.y;
            }
        }


        return SpawnPoint;
    }

    public void ClearAllSpawnedEnemies()
    {
        // 倒序遍历销毁，避免正序删除导致的索引越界
        for (int i = SpawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (SpawnedEnemies[i] != null)
            {
                Destroy(SpawnedEnemies[i]);
            }
        }
        // 清空敌人列表，避免残留空引用
        SpawnedEnemies.Clear();
        // 重置检查计数器，防止后续逻辑异常
        EnemyToCheck = 0;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
