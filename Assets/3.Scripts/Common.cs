using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;


/// <summary>
/// 常量在这里。
/// </summary>
public static class Common
{
  /// <summary>
  /// 60fps时平均一帧的时间
  /// </summary>
  public const float avgframeTime = 0.01667f;

}


/// <summary>
/// fps平摊算法
/// <para>优化反复运行的函数</para>
/// </summary>
public class FPSavg
{
  private float refreshTime;
  private float timeCount = 1000f; //首次一定刷新


  /// <summary>
  /// fps平摊算法构造函数。
  /// </summary>
  /// <param name="refreshTime">更新时间间隔</param>
  /// <param name="disperse">分散度，决定散布在其后的多少帧里</param>
  public FPSavg(float refreshTime, int disperse = 0)
  {
    int bias = UnityEngine.Random.Range(0, disperse); //把计算散布在不同帧中，平衡负载
    //Debug.Log(String.Format("bias={0}", bias));
    this.refreshTime = refreshTime + bias * Common.avgframeTime;
  }

  /// <summary>
  /// 到了运行的时刻么？
  /// </summary>
  /// <returns></returns>
  public bool canRun()
  {
    timeCount += Time.deltaTime;
    if (timeCount > refreshTime)
    {
      timeCount = 0;
      return true;
    }
    else
      return false;
  }
}
