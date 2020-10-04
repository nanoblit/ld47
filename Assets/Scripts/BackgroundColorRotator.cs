using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorRotator : MonoBehaviour {
  public float RotationSpeed;

  private void Start() {
    GameManager.I.OnBeginLoadLevel += SetNextBackgroundColor;
    Camera.main.backgroundColor = GameManager.I.StartingBackgoundColor;
  }

  private void Update() {
    var backgroundColor = Camera.main.backgroundColor;
    Camera.main.backgroundColor = RotateColor(backgroundColor);
  }

  private void SetNextBackgroundColor() {
    GameManager.I.StartingBackgoundColor = Camera.main.backgroundColor;
    GameManager.I.OnBeginLoadLevel -= SetNextBackgroundColor;
  }

  private Color RotateColor(Color color) {
    float h, s, v;
    Color.RGBToHSV(color, out h, out s, out v);
    h += RotationSpeed * Time.deltaTime;
    h = h > 1f ? h - Mathf.Floor(h) : h;

    return Color.HSVToRGB(h, s, v);
  }
}
