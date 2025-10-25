using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SSAOAdaption : MonoBehaviour
{
  bool indoor = false;  //默认在室外
  float intensity = 1;
  [Header("decrease ssao when indoor")]
  public float lowIntensity = 0.15f;
  Ray ray = new Ray(Vector3.zero, Vector3.up);//向上投射
  UnityEngine.PostProcessing.PostProcessingProfile ppp;
  FPSavg fPSavg;


  void Start()
  {
    UnityEngine.PostProcessing.PostProcessingBehaviour ppb
      = GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>();
    ppp = ppb.profile;
    fPSavg = new FPSavg(0.1f);
  }


#if UNITY_EDITOR
  private void OnGUI()
  {
    GUIStyle style = new GUIStyle();
    style.fontSize = 16;
    style.normal.textColor = new Color(1, 1, 1);
    GUI.Label(new Rect(0, 0, 256, 32), "Time.deltaTime=" + Time.deltaTime.ToString(), style);
  }
#endif


  // Update is called once per frame
  void Update()
  {
    if (fPSavg.canRun()) //降低开销
      UpdateIndoor();

    if (indoor) //在室内，ssao强度降低到0.3
      intensity = Mathf.Clamp(intensity - Time.deltaTime, lowIntensity, 1.0f);
    else
      intensity = Mathf.Clamp(intensity + Time.deltaTime, lowIntensity, 1.0f);

    ppp.ambientOcclusion.SetAOIntensity(intensity);
  }


  private void OnDestroy()//复原
  {
    ppp.ambientOcclusion.SetAOIntensity(1);
  }


  private void UpdateIndoor()
  {
    ray.origin = transform.position; //更新，获得相机的位置
    if (Physics.Raycast(ray, 25f, 1 << 8)) //发现屋顶。只检测static(8)层，即静态建筑
      indoor = true;
    else
      indoor = false;
  }
}
