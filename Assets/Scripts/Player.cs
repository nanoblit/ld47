using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {
  public event Action<int> OnAddPoints; // Takes current points

  private int _points = 0;

  private Rigidbody2D _rigidbody;
  private Shooter _shooter;
  private Health _health;

  protected override void Awake() {
    base.Awake();
    _rigidbody = GetComponent<Rigidbody2D>();
    _shooter = GetComponentInChildren<Shooter>();
    _health = GetComponent<Health>();
  }

  private void Start() {
    StartCoroutine(AddPointsCoroutine());
    _health.OnKilled += UpdateBestScore;
  }

  private void Update() {
    // if (Input.GetMouseButtonDown(0)) {
    //   _shooter.StartShooting();
    // } else if (Input.GetMouseButtonUp(0)) {
    //   _shooter.StopShooting();
    // }
  }

  private IEnumerator AddPointsCoroutine() {
    var wait = new WaitForSeconds(1f);
    while (true) {
      yield return wait;
      AddPoints(10);
    }
  }

  private void FixedUpdate() {
    float rightSideOfView = GameManager.ViewHalfWidth;
    float leftSideOfView = -rightSideOfView;
    float newXPosition = Mathf.Clamp(_mousePosition.x, leftSideOfView, rightSideOfView);

    _rigidbody.position = new Vector2(newXPosition, _rigidbody.position.y);
  }

  private void UpdateBestScore() {
    var gm = GameManager.I;
    gm.BestScore = gm.BestScore < _points ? _points : gm.BestScore;
  }

  public void AddPoints(int points) {
    _points += points;

    OnAddPoints?.Invoke(_points);
  }

  private Vector2 _mousePosition {
    get {
      return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
  }

  protected override void OnDestroy() {
    base.OnDestroy();
    StopAllCoroutines();
  }
}