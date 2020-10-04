using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public float Speed;
  public int Points;

  private Rigidbody2D _rigidbody;
  private Shooter _shooter;
  private Health _health;

  private void Awake() {
    _rigidbody = GetComponent<Rigidbody2D>();
    _shooter = GetComponentInChildren<Shooter>();
    _health = GetComponent<Health>();
  }

  private void Start() {
    _shooter.StartShooting();
    // _health.OnKilled += AwardPlayerPoints;
  }

  private void FixedUpdate() {
    _rigidbody.velocity = new Vector2(0f, -Speed * Time.deltaTime);
  }

  private void AwardPlayerPoints() {
    // Player.I?.AddPoints(Points);
  }

  private void OnDestroy() {
    _health.OnKilled -= AwardPlayerPoints;
  }
}
