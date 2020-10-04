using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
  public int Damage = 1;
  public bool DestroyOnContact = true;
  public string EnemyTag;

  private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag == EnemyTag) {
      other.gameObject.GetComponent<Health>()?.Damage(Damage);
      if (DestroyOnContact) {
        var health = GetComponent<Health>();
        if (health != null) {
          health.Kill();
        } else {
          Destroy(gameObject);
        }
      }
    }
  }
}