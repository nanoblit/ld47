﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {
  public void StartGame() {
    GameManager.I.LoadScene("Game");
  }
}
