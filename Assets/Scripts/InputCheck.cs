using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputCheck : MonoBehaviour
{
    public Vector3 accelerometer;
    public Vector3 gyroscope;
    public Vector3 gravity;
    public Quaternion attitude;
    public Vector3 linear_acceleration;
    public Vector3 magnetic;
    public float my_light;
    public float pressure;
    public float proximity;
    public float humidity;
    public float ambient_temp;
    public int step_counter;
    private int DEBUG_FRAG=1;


    private Vector3 acceleration;
    private Compass compass;
    private Quaternion gyro;
    private GUIStyle labelStyle;
    private ParticleSystem.MainModule ps_main;

    // Start is called before the first frame update
    void Start()
    {
        //フォント生成

        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 40;
        this.labelStyle.normal.textColor = Color.white;


        if(Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
        if (MagneticFieldSensor.current != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.MagneticFieldSensor.current);
        }
        if (LightSensor.current != null)
        {
            InputSystem.EnableDevice(LightSensor.current);
        }
        if(PressureSensor.current!=null)
        {
          InputSystem.EnableDevice(PressureSensor.current);
        }

        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps_main = ps.main;

        var sp = ps_main.simulationSpeed;
        sp = 10.0f;
        // ps_main.startSize=10.0f;
        // startsize = ;
    }

    // Update is called once per frame
    void Update()
    {
        if(Accelerometer.current != null)
        {
          accelerometer = Accelerometer.current.acceleration.ReadValue();
        }else{
        }
        if (MagneticFieldSensor.current != null)
        {
            magnetic = MagneticFieldSensor.current.magneticField.ReadValue();
        }
        if (LightSensor.current != null)
        {
            my_light = LightSensor.current.lightLevel.ReadValue();
        }
        if (PressureSensor.current != null)
        {
            pressure = PressureSensor.current.atmosphericPressure.ReadValue();
        }


        // ps_main.simulationSpeed = 2f+Mathf.Max(pressure-950,0) / 3;

        ps_main.startSize= 3f+Mathf.Min(Mathf.Abs(magnetic.x + magnetic.y + magnetic.z) / 15,30);
        // ps_main.startSize=Mathf.Min(my_light,10);
        Debug.Log(ps_main.startSize);

        // var st_col = ps_main.startColor;
        // st_col.color =
        // Color startcolor =new Color(1f - Mathf.Min(my_light,100)/255, ps_main.startColor.color.g,ps_main.startColor.color.b, 1f);
        Color startcolor=new Color(Mathf.Min(my_light/80+0.5f,1f),1f,Mathf.Min(my_light/100+0.7f,1f),1f);
        ps_main.startColor=startcolor;
        // ps_main.startSize=5.0f;
    }
    //
    //
    void OnGUI()
    {
        if(DEBUG_FRAG==1){
          float x = Screen.width / 10;
          float y = 0;
          float w = Screen.width * 8 / 10;
          float h = Screen.height / 20;

          for (int i = 0; i < 4; i++)
          {
              y = Screen.height / 10 + h * i;
              string text = string.Empty;

              switch (i)
              {
                  case 0://X
                      if(Accelerometer.current != null){
                        text = string.Format("accelerometer:{0}", accelerometer);
                      }else{
                        text = "accelerometer:null";
                      }
                      break;
                  case 1:
                      if(MagneticFieldSensor.current != null){
                        text = string.Format("magnetic:{0}", magnetic);
                      }else{
                        text = "magnetic:null";
                      }
                      break;
                  case 2:
                      if(LightSensor.current != null){
                        text = string.Format("light:{0}", my_light);
                      }else{
                        text = "light:null";
                      }
                      break;
                  case 3:
                      if(PressureSensor.current != null){
                        text = string.Format("pressure:{0}", pressure);
                      }else{
                        text = "pressure:null";
                      }
                      break;
                  default:
                      throw new System.InvalidOperationException();
              }

              GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);
          }
        }

    }

}
