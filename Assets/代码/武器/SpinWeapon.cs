using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    public float RotateSpeed;  

    public Transform Holder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Holder.rotation = Quaternion.Euler(0f, 0f, Holder.rotation.eulerAngles.z + (RotateSpeed* Time.deltaTime));
    }
}
