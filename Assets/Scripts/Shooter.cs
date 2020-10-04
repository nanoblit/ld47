using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
  public Transform BulletPrefab;
  public float ShootingDelay;

  private Coroutine _shootCoroutine;

  private float _startShootingTimer = 0f;

  private void Update() {
    if (_startShootingTimer > 0f) {
      _startShootingTimer -= Time.deltaTime;
    }
  }

  public void StartShooting() {
    if (_shootCoroutine == null) {
      _shootCoroutine = StartCoroutine(Shoot());
    }
  }

  public void StopShooting() {
    if (_shootCoroutine != null) {
      StopCoroutine(_shootCoroutine);
      _shootCoroutine = null;
    }
  }

  private IEnumerator Shoot() {
    var wait = new WaitForSeconds(ShootingDelay);
    var waitToStartShooting = new WaitForSeconds(_startShootingTimer);

    if (_startShootingTimer > float.Epsilon) {
      yield return waitToStartShooting;
    }

    while (true) {
      var projectile = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
      projectile.transform.rotation = transform.rotation;

      _startShootingTimer = ShootingDelay;
      yield return wait;
    }
  }

  private void OnDisable() {
    StopShooting();
  }
}