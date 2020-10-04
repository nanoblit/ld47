using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
  public float Speed;

  private Rigidbody2D _rigidbody;

  private void Awake() {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    _rigidbody.velocity = transform.rotation * new Vector2(Speed * Time.fixedDeltaTime, 0f);
  }
}