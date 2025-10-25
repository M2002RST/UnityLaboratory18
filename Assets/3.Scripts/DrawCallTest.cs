using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DrawCallTest : MonoBehaviour {
  [Header("push 1 2 3 4 5. 6 for new 100,000 objects")]
  public GameObject[] gameObjects = new GameObject[5];

	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown(KeyCode.Alpha1))
      gameObjects[0].SetActive(!gameObjects[0].activeSelf);
    if (Input.GetKeyDown(KeyCode.Alpha2))
      gameObjects[1].SetActive(!gameObjects[1].activeSelf);
    if (Input.GetKeyDown(KeyCode.Alpha3))
      gameObjects[2].SetActive(!gameObjects[2].activeSelf);
    if (Input.GetKeyDown(KeyCode.Alpha4))
      gameObjects[3].SetActive(!gameObjects[3].activeSelf);
    if (Input.GetKeyDown(KeyCode.Alpha5))
      gameObjects[4].SetActive(!gameObjects[4].activeSelf);
    if (Input.GetKeyDown(KeyCode.Alpha6))
    {
      for (int i = 0; i < 100000; i++)
      {
        GameObject.Instantiate(gameObjects[0]);
      }
    }
  }
}
