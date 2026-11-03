using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Transform sprite;

    public float speed;//变换速度

    public float minSize, maxSize;//尺寸大小

    private float activeSize;//变换方向(大->小/小->大)
    // Start is called before the first frame update
    void Start()
    {
        activeSize = maxSize;

        speed = speed * Random.Range(minSize, maxSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite ==null)
        {
            return ;
        }
        sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * activeSize, speed * Time.deltaTime);//敌人模型比例缩放，实现动画
        //MoveTowards函数要求3个参数，分别是起始点，结束点，变化速度
        float scaleDiff = Mathf.Abs(sprite.localScale.x - activeSize);
        if (scaleDiff < 0.01f) // 误差阈值0.01，足够小且避免精度问题
        {
            // 切换目标尺寸（大→小 或 小→大）
            activeSize = (activeSize == maxSize) ? minSize : maxSize;
        }
    }
}
