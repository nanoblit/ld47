using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfOutOfBounds : MonoBehaviour {
  public bool upperBounds = true;
  public bool lowerBounds = true;

  private SpriteRenderer _spriteRender;

  private void Awake() {
    _spriteRender = GetComponent<SpriteRenderer>();
  }

  private void Update() {
    float spriteHalfHeight = _spriteRender.bounds.extents.y;;
    float bottom = transform.position.y - spriteHalfHeight;
    float top = transform.position.y + spriteHalfHeight;
    float cameraHalfSize = Camera.main.orthographicSize;

    if ((lowerBounds && top < -cameraHalfSize) ||
      (upperBounds && bottom > cameraHalfSize)) {
      Destroy(gameObject);
    }
  }
}