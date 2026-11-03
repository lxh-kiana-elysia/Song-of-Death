using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;//实例  

    public GameObject Life1, Life2, Life3, Life4, Life5;
    public GameObject Enemy;

    private EnemySpawner enemySpawner;
    private void Awake()
    {
        Instance = this;//生成实例
    }

    public int CurrentHealth , MaxHealth = 5, Initial = 3;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = Initial;//初始化血量

        enemySpawner = FindObjectOfType<EnemySpawner>();    
    }

    // Update is called once per frame
    void Update()
    {
        Life();
    }

    public void TakeDamage(int DamageToTake)
    {
        CurrentHealth -= DamageToTake;//扣血
        Life();
        if (CurrentHealth <= 0)
        {
            //SceneManager.LoadScene("Die");
            FindObjectOfType<GameController>().game();
            if (enemySpawner != null)
            {
                enemySpawner.ClearAllSpawnedEnemies();
            }
            FindObjectOfType<EnemySpawner>().Die();
            gameObject.transform.localScale = new Vector3(0, 0, 0);//停止玩家活动
        }
    }

    public void Life()
    {
        if (CurrentHealth == 5)
        {
            Life1.SetActive(true);
            Life2.SetActive(true);
            Life3.SetActive(true);
            Life4.SetActive(true);
            Life5.SetActive(true);
        }
        if (CurrentHealth == 4)
        {
            Life1.SetActive(true);
            Life2.SetActive(true);
            Life3.SetActive(true);
            Life4.SetActive(true);
            Life5.SetActive(false);
        }
        if (CurrentHealth == 3)
        {
            Life1.SetActive(true);
            Life2.SetActive(true);
            Life3.SetActive(true);
            Life4.SetActive(false);
            Life5.SetActive(false);
        }
        if (CurrentHealth == 2)
        {
            Life1.SetActive(true);
            Life2.SetActive(true);
            Life3.SetActive(false);
            Life4.SetActive(false);
            Life5.SetActive(false);
        }
        if (CurrentHealth == 1)
        {
            Life1.SetActive(true);
            Life2.SetActive(false);
            Life3.SetActive(false);
            Life4.SetActive(false);
            Life5.SetActive(false);
        }
        if (CurrentHealth < 1)
        {
            Life1.SetActive(false);
            Life2.SetActive(false);
            Life3.SetActive(false);
            Life4.SetActive(false);
            Life5.SetActive(false);
        }
    }
}
