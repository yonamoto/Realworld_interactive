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
    // private int DEBUG_FRAG=1;
    GameObject infoButton;
    DataInfo datainfo;

    private Vector3 acceleration;
    private Compass compass;
    private Quaternion gyro;
    private GUIStyle labelStyle;
    private ParticleSystem.MainModule ps_main;

    // Start is called before the first frame update
    void Start()
    {
        //フォント生成
        infoButton=GameObject.Find("infoButton");
        datainfo=infoButton.GetComponent<DataInfo>();
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
        // sp = 10.0f;
        // ps_main.startSize=10.0f;
        // startsize = ;
    }

    // Update is called once per frame
    void Update()
    {
        if(Accelerometer.current != null)
        {
          InputSystem.EnableDevice(Accelerometer.current);
          accelerometer = Accelerometer.current.acceleration.ReadValue();
        }
        if (MagneticFieldSensor.current != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.MagneticFieldSensor.current);
            magnetic = MagneticFieldSensor.current.magneticField.ReadValue();
        }else{
            magnetic=new Vector3(-5f,20f,-5f);
        }
        if (LightSensor.current != null)
        {
            InputSystem.EnableDevice(LightSensor.current);
            my_light = LightSensor.current.lightLevel.ReadValue();
        }else{
            my_light = 8f;
        }
        if (PressureSensor.current != null)
        {
            InputSystem.EnableDevice(PressureSensor.current);
            pressure = PressureSensor.current.atmosphericPressure.ReadValue();
        }else{
            pressure = 1024f;
        }


        ps_main.simulationSpeed = 0.55f+Mathf.Max(pressure-950,0) / 110;

        ps_main.startSize= 1.5f+Mathf.Min(Mathf.Sqrt(Mathf.Abs(magnetic.x)+Mathf.Abs(magnetic.y) + Mathf.Abs(magnetic.z)) / 6 ,5);
        // ps_main.startSize=Mathf.Min(my_light,10);
        Debug.Log(ps_main.startSize);

        // var st_col = ps_main.startColor;
        // st_col.color =
        // Color startcolor =new Color(1f - Mathf.Min(my_light,100)/255, ps_main.startColor.color.g,ps_main.startColor.color.b, 1f);
        Color startcolor=new Color(Mathf.Min(my_light/100+0.4f,1f),Mathf.Min(my_light/500+0.90f,1f),Mathf.Min(Mathf.Sqrt(my_light)/40+0.65f,1f),Mathf.Min(my_light/130+0.85f,1f));
        ps_main.startColor=startcolor;
        // ps_main.startSize=5.0f;
    }
    //
    //
    void OnGUI()
    {
        if(datainfo.GetDebugFrag()==1){
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
