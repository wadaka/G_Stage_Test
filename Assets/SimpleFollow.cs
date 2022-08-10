using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;
    public GameObject target;
    public float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        diff = target.transform.position - transform.position;
    }

    // Update is called once per frame
    //LateUpdate：ほかのUpdateが終了した後で発生するUpdate
    void LateUpdate()
    {
        //現在地(引数１)と目的地(引数2)に対し、割合分(引数3)の値へGO
        //
       transform.position = Vector3.Lerp(
        transform.position,
        target.transform.position - diff,
        Time.deltaTime*followSpeed
       );
    }
}

