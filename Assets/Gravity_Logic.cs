using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Logic : MonoBehaviour
{
    //PlayerのTransform
    private Transform myTransform;

    //PlayerのRigidbody
    private Rigidbody rig = null;

    //重力減となる惑星
    private GameObject Planet;

    //「Planet」タグがついているオブジェクトを格納する配列
    private GameObject[] Planets;

    //重力の強さ
    public float Gravity;

    //惑星に対するPlayerの向き
    private Vector3 Direction;

    //Rayが接触した惑星のポリゴンの法線
    private Vector3 Normal_vec = new Vector3(0, 0, 0);

    void Start()
    {
        rig = this.GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeRotation;
        rig.useGravity = false;
        myTransform = transform;
    }

    void Update()
    {
        Attract();
        RayTest();
    }

    public void Attract()
    {
        Vector3 gravityUp = Normal_vec;

        Vector3 bodyUp = myTransform.up;

        myTransform.GetComponent<Rigidbody>().AddForce(gravityUp * Gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * myTransform.rotation;

        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRotation, 120 * Time.deltaTime);

    }

    GameObject Choose_Planet()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planet");

        double[] Planet_distance = new double[Planets.Length];

        for (int i = 0; i < Planets.Length; i++)
        {
            Planet_distance[i] = Vector3.Distance(this.transform.position, Planets[i].transform.position);
        }

        int min_index = 0;
        double min_distance = Mathf.Infinity;

        for (int j = 0; j < Planets.Length; j++)
        {
            if (Planet_distance[j] < min_distance)
            {
                min_distance = Planet_distance[j];
                min_index = j;
            }
        }

        return Planets[min_index];
    }

    void RayTest()
    {
        Planet = Choose_Planet();

        Direction = Planet.transform.position - this.transform.position;

        Ray ray = new Ray(this.transform.position, Direction);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //もしRayにオブジェクトが衝突したら
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Rayが当たったオブジェクトのtagがPlanetだったら
            if (hit.collider.tag == "Planet")
            {
                Normal_vec = hit.normal;
            }
        }
    }
}