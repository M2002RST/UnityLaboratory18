using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]

// 名称：动态物体自动遮蔽
// 作用：在室内，同步来自建筑的ao强度
// 可用对象:小物件或家具,或玩家
// 注意：在编辑器里面也可以运行(不稳定，无法编译)
// 2021-7-11
public class DynamicAmbientOcclusion : MonoBehaviour
{
  // Use this for initialization
  public enum DynamicType { normal, npc, player }
  [Tooltip("refresh speed.\nplayer > npc > normal")]
  public DynamicType refreshFrequent = DynamicType.normal;
  float refreshTime = 0;
  float s = 0;
  float s2 = 0;
  Material mtl;
  Material mtl2;
  Texture2D t;
  Ray ray = new Ray(Vector3.zero, Vector3.down);//向下投射
  RaycastHit hitinfo;
  Vector2 uv;
  FPSavg fPSavg;

  void Start()
  {
    switch (refreshFrequent)
    {
      case DynamicType.normal://普通刷新间隔
        refreshTime = 1f;
        break;
      case DynamicType.npc://npc刷新间隔
        refreshTime = 0.5f;
        break;
      case DynamicType.player://玩家刷新间隔
        refreshTime = 0.2f;
        break;
    }

    fPSavg = new FPSavg(refreshTime, 6);
    mtl = this.GetComponent<MeshRenderer>().material;
  }


  void Update()
  {
    if (fPSavg.canRun()) //降低开销
    {
      //Debug.Log(Time.frameCount,this);
      UpdateAO();
    }

    if (s2 != s)
    {
      LearpIntensty();
      mtl.SetFloat("_OcclusionStrength", s2); //写入AO值
    }
  }


  private void LearpIntensty()
  {
    s2 = //平滑处理
       Mathf.Clamp(s2 + (s - s2) * Time.deltaTime * 5f, Mathf.Min(s, s2), Mathf.Max(s, s2));
  }


  /// <summary>
  /// 核心功能函数
  /// 功能：自动更新动态物体的ao值
  /// 原理：抓取下方建筑的ao贴图像素，转换为
  /// ao强度值赋予该物体独享的材质
  /// </summary>
  /// <returns></returns>
  void UpdateAO()
  {
    t = null;
    ray.origin = transform.position + Vector3.up * 0.1f; //更新，获得物体位置稍上方的位置
    if (Physics.Raycast(ray, out hitinfo, 10f, 1 << 8)) //只检测static(8)层，即静态建筑
    {
      uv = hitinfo.textureCoord2;
      mtl2 = hitinfo.transform.gameObject.GetComponent<MeshRenderer>().material;
      t = (Texture2D)mtl2.GetTexture("_OcclusionMap");
      if (t != null)
      {
        Color c = t.GetPixelBilinear(uv.x, uv.y);
        s = 1f - c.g; //获得建筑ao贴图的值，转换为ao强度
                                 //Debug.Log("ao strength = " + strength.ToString());
      }
      else s = 0; //有的建筑没有ao，关闭AO
    }
    else s = 0; //找不到建筑，关闭AO
  }
}