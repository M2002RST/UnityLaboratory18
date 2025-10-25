using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LitMapCtl : MonoBehaviour
{
  bool loaded = false;
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
    {
      if (SceneManager.GetActiveScene().name.Equals("S0"))
      {
        SceneManager.LoadSceneAsync("S1", LoadSceneMode.Single);
      }
      else
      {
        SceneManager.LoadSceneAsync("S0", LoadSceneMode.Single);
      }
    }
    if (Input.GetKeyDown(KeyCode.Mouse1) && !loaded)
    {
      loaded = true;
      SceneManager.LoadSceneAsync("Bake1", LoadSceneMode.Additive);
    }
  }
}