using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toGalaxy : MonoBehaviour
{
    // Start is called before the first frame update
    int DEBUG_FRAG=0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStartButton()
    {
        var datainfo =GameObject.Find("infoButton").GetComponent<DataInfo>();
        DEBUG_FRAG=datainfo.DEBUG_FRAG;
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene("SampleScene");
    }
    public void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
      var datainfo =GameObject.Find("infoButton").GetComponent<DataInfo>();
      datainfo.DEBUG_FRAG=DEBUG_FRAG;
      SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
