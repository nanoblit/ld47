using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
  private static T _instance;

  public static T I {
    get {
      return _instance;
    }
  }

  public static bool IsInitialized {
    get {
      return _instance != null;
    }
  }

  protected virtual void Awake() {
    if (_instance == null) {
      _instance = (T) this;
    } else {
      Debug.LogError($"Instance of singleton already exist");
    }
  }

  protected virtual void OnDestroy() {
    if (_instance == this) {
      _instance = null;
    }
  }
}