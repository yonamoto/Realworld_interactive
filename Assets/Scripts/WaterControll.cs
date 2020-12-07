using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaterControll : MonoBehaviour
{
    private Vector3 acceleration;
    private Compass compass;
    private Quaternion gyro;

    // Use this for initialization
    void Start()
    {
        Input.compass.enabled = true;
        Input.gyro.enabled = true;
        Debug.Log("start! waterfall");
    }

    // Update is called once per frame
    void Update()
    {
        this.acceleration = Input.acceleration;
        this.compass = Input.compass;
        this.gyro = Input.gyro.attitude;

        //// 加速度センサを利用してCubeを移動
        /*
        float speed = 5.0f;

        var dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;

        if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }

        dir *= Time.deltaTime;

        transform.Translate(dir * speed);
        */

        //地磁気センサーから値を取得
        transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);

        // ジャイロセンサの値を取得し、Unity内のカメラと同期
        this.transform.localRotation = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));

        // Cubeの位置を任意の位置に変更
        //Vector3 pos = transform.position;
        //pos.x = 0.5f;
        //transform.position = pos;
        //Debug.Log(transform.position);

        //// OK
        //transform.position = new Vector3(
        //    -4,
        //    -1,
        //    5);
    }
}
