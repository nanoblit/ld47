using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersLoader : MonoBehaviour {
  public GameObject ManagersPrefab;

  private void Awake() {
    if (!GameManager.IsInitialized) {
      Instantiate(ManagersPrefab);
    }
  }
}