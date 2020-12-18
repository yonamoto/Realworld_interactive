using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInfo : MonoBehaviour
{
    public int DEBUG_FRAG=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickStartButton()
    {
      if(DEBUG_FRAG==1)
      {
        DEBUG_FRAG=0;
      }else
      {
        DEBUG_FRAG=1;
      }
    }
    public int GetDebugFrag()
    {
      return DEBUG_FRAG;
    }
}
