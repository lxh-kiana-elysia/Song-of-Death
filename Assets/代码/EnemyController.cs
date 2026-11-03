using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D theRb;
    public float MoveSpeed = 0f;//速度
    public int damage;//伤害
    private Transform target;//用于代指玩家位置
    public int Health = 3;

    public float hitWaitTime = 1f;//碰撞无敌时间
    private float hitCounter;

    void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("【EnemyController】场景中未找到PlayerController组件！敌人将无法追击玩家", this);
            enabled = false;// 禁用当前脚本，阻止后续Update执行
            return;
        }
        target = player.transform;//获得玩家位置  
    }

    private void Update()
    {
        theRb.velocity = (target.position - transform.position).normalized * MoveSpeed;//追击玩家
        //normalized是用于将向量归一化，即将向量长度缩放为1，方向不变

        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("玩家扣血");
        if (collision.gameObject.tag == "Player" && hitCounter <=0)
        {
            if (PlayerHealthController.Instance == null)
            {
                Debug.LogError("【EnemyController】场景中未找到PlayerController组件！敌人将无法追击玩家", this);
                enabled = false;// 禁用当前脚本，阻止后续Update执行
                return;
            }
            PlayerHealthController.Instance.TakeDamage(damage); 

            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(int damageToTake)
    {
        Health -= damageToTake;

        if (Health <= 0)
        {
            Destroy(gameObject);//摧毁敌人
        }
    }
}
