using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunCtl : MonoBehaviour
{
  GameObject L;
  // Use this for initialization
  void Start()
  {
    L = GameObject.FindGameObjectWithTag("sun");
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse0))
      L.SetActive(!L.activeSelf);
  }
}
