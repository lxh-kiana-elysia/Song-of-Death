using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;//移动速度
    public Transform sprite;
    private float faceDirection = 1f;

    public Animator anim;
    void Start()
    {

    }

    void Update()
    {
        
        UnityEngine.Vector3 MoveInput = new UnityEngine.Vector3(0f, 0f, 0f);
        MoveInput.x = Input.GetAxis("Horizontal");//水平移动
        MoveInput.y = Input.GetAxis("Vertical");//垂直移动

        MoveInput.Normalize();//确认方向

        if (Input.GetKeyDown(KeyCode.LeftShift))//冲刺输入
        {
            Debug.Log("冲刺");
            MoveSpeed *= 1.5f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            MoveSpeed /=1.5f ;
        }

        

        //Debug.Log("输入测试: " + MoneInput);
        transform.position += MoveInput * MoveSpeed * Time.deltaTime;//玩家移动
        if (MoveInput != Vector3.zero)
        {
            anim.SetBool("IsMoving",true);
        }
        else
        {
            anim.SetBool("IsMoving",false); 
        }

        // 5. 更新朝向（只有有移动输入时才更新）
        if (MoveInput.x != 0)
        {
            faceDirection = Mathf.Sign(MoveInput.x); // 取x轴输入的符号：1（右）或-1（左）
        }
    }

    void Filp()//翻转
    {
        Debug.Log("111");
        sprite.localScale = new Vector3(faceDirection, 1f, 1f);

    }

    private void FixedUpdate()
    {
        
        Filp();
    }
}
