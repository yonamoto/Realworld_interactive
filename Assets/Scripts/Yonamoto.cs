using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Yonamoto : MonoBehaviour
{
    //public Vector3 accelerometer;
    public Vector3 gyroscope;
    public Vector3 gravity;
    public Quaternion attitude;
    public Vector3 linear_acceleration;
    public Vector3 magnetic;
    public float ligth;
    public float pressure;
    public float proximity;
    public float humidity;
    public float ambient_temp;
    public int step_counter;


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


        Input.compass.enabled = true;

        if (UnityEngine.InputSystem.Gyroscope.current != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
        if (GravitySensor.current != null)
        {
            InputSystem.EnableDevice(GravitySensor.current);
        }
        if (AttitudeSensor.current != null)
        {
            InputSystem.EnableDevice(AttitudeSensor.current);
        }
        if (LinearAccelerationSensor.current != null)
        {
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
        }
        if (MagneticFieldSensor.current != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.MagneticFieldSensor.current);
        }
        if (LightSensor.current != null)
        {
            InputSystem.EnableDevice(LightSensor.current);
        }
        if (PressureSensor.current != null)
        {
            InputSystem.EnableDevice(PressureSensor.current);
        }
        if (ProximitySensor.current != null)
        {
            InputSystem.EnableDevice(ProximitySensor.current);
        }
        if (HumiditySensor.current != null)
        {
            InputSystem.EnableDevice(HumiditySensor.current);
        }
        if (AmbientTemperatureSensor.current != null)
        {
            InputSystem.EnableDevice(AmbientTemperatureSensor.current);
        }
        if (StepCounter.current != null)
        {
            InputSystem.EnableDevice(StepCounter.current);
        }


        //Debug.Log(string.Format("<b>精度</b>：{0}", Input.compass.headingAccuracy));
        //Debug.Log(string.Format("<b>タイムスタンプ</b>：{0}", Input.compass.timestamp));

        //Input.gyro.enabled = true;

        //ParticleSystem ps = GetComponentInParent<ParticleSystem>();
        //ps = GetComponent<ParticleSystem>();
        ParticleSystem ps = this.gameObject.GetComponent<ParticleSystem>();
        ps_main = ps.main;

        var sp = ps_main.simulationSpeed;
        sp = 10.0f;
        var startsize = ps_main.startSize;
        startsize = 10.0f;
    }

    // Update is called once per frame
    //void update yonamoto
    void Update()
    {
        /*
        if (Accelerometer.current != null)
        {
            accelerometer = Accelerometer.current.acceleration.ReadValue();
        }
        */
        if (GravitySensor.current != null)
        {
            gravity = GravitySensor.current.gravity.ReadValue();
        }
        if (AttitudeSensor.current != null)
        {
            attitude = AttitudeSensor.current.attitude.ReadValue();
        }
        if (LinearAccelerationSensor.current != null)
        {
            linear_acceleration = LinearAccelerationSensor.current.acceleration.ReadValue();
        }
        if (MagneticFieldSensor.current != null)
        {
            magnetic = MagneticFieldSensor.current.magneticField.ReadValue();
        }
        if (LightSensor.current != null)
        {
            ligth = LightSensor.current.lightLevel.ReadValue();
        }
        if (PressureSensor.current != null)
        {
            pressure = PressureSensor.current.atmosphericPressure.ReadValue();
        }
        if (ProximitySensor.current != null)
        {
            proximity = ProximitySensor.current.distance.ReadValue();
        }
        if (HumiditySensor.current != null)
        {
            humidity = HumiditySensor.current.relativeHumidity.ReadValue();
        }
        if (AmbientTemperatureSensor.current != null)
        {
            ambient_temp = AmbientTemperatureSensor.current.ambientTemperature.ReadValue();
        }
        if (StepCounter.current != null)
        {
            step_counter = StepCounter.current.stepCounter.ReadValue();
        }
        

        this.acceleration = Input.acceleration;
        this.compass = Input.compass;
        this.gyro = Input.gyro.attitude;


        //ps_main.simulationSpeed = Math.Abs(accelerometer.x + accelerometer.y + accelerometer.z) *10;
        //ps_main.simulationSpeed = Math.Abs(acceleration.x + acceleration.y + acceleration.z) *10;


        //カンタの
        //ps_main.simulationSpeed = 2f + Mathf.Abs(accelerometer.x + accelerometer.y + accelerometer.z) / 3;

        ps_main.startSize = 2f + Mathf.Min(Mathf.Abs(magnetic.x + magnetic.y + magnetic.z) / 7, 40);
        Debug.Log(ps_main.startSize);
        Color startcolor = new Color(Mathf.Min(ligth / 80 + 0.5f, 1f), 1f, Mathf.Min(ligth / 100 + 0.7f, 1f), 1f);
        ps_main.startColor = startcolor;
        // ps_main.startSize=5.0f;
        /*
        ps_main.simulationSpeed = Math.Abs(gravity.x + gravity.y + gravity.z);
        ps_main.startSize = Math.Abs(magnetic.x + magnetic.y + magnetic.z) / 5;
        ParticleSystem.MinMaxGradient st_col = ps_main.startColor;
        st_col.color = new Color32((byte)(255 - ligth / 3), (byte)(ps_main.startColor.color.g), (byte)(ps_main.startColor.color.b), (byte)1);
        */

        /*
        var sp = ps_main.simulationSpeed;
        sp = Math.Abs(accelerometer.x + accelerometer.y + accelerometer.z) / 3;
        var startsize = ps_main.startSize;
        startsize = Math.Abs(magnetic.x + magnetic.y + magnetic.z) / 10;
        Debug.Log(ligth);
        var st_col = ps_main.startColor;
        st_col.color = new Color32((byte)(255 - ligth / 10), (byte)(ps_main.startColor.color.g), (byte)(ps_main.startColor.color.b), (byte)1);
        */
    }



    void OnGUI()
    {
        if (acceleration != null)
        {
            float x = Screen.width / 10;
            float y = 0;
            float w = Screen.width * 8 / 10;
            float h = Screen.height / 20;

            for (int i = 0; i < 12; i++)
            {
                y = Screen.height / 10 + h * i;
                string text = string.Empty;


                switch (i)
                {
                    case 0://X
                        //text = string.Format("accel-X:{0}", accelerometer);
                        text = string.Format("accel-X:{0}", acceleration);
                        break;
                    case 1://Y
                        text = string.Format("accel-Y:{0}", gyroscope);
                        break;
                    case 2://Z
                        text = string.Format("accel-Z:{0}", gravity);
                        break;
                    case 3://X
                        text = string.Format("comps-X:{0}", attitude);
                        break;
                    case 4://Y
                        text = string.Format("comps-Y:{0}", linear_acceleration);
                        break;
                    case 5://Z
                        text = string.Format("comps-Z:{0}", magnetic);
                        break;
                    case 6://Z
                        text = string.Format("magneticHeading:{0}", ligth);
                        break;
                    case 7://Z
                        text = string.Format("trueHeading:{0}", pressure);
                        break;
                        /*
                        case 8://Y
                            text = string.Format("gyro-x:{0}", proximity);
                            break;
                        case 9://Y
                            text = string.Format("gyro-y:{0}", humidity);
                            break;
                        case 10://Y
                            text = string.Format("gyro-z:{0}", ambient_temp);
                            break;
                        case 11://Y
                            text = string.Format("gyro-w:{0}", step_counter);
                            break;
                        */
                        /*
                        default:
                            throw new System.InvalidOperationException();
                        */
                }

                GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);
            }
        }
    }
}
