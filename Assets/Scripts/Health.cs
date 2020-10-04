using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour {
  public event Action OnKilled;
  public event Action<int> OnDamageDealt; // Takes current hp

  [SerializeField] private int _hp;

  public float Hp {
    get {
      return _hp;
    }
  }

  public void Damage(int dmg) {
    _hp -= dmg;
    OnDamageDealt?.Invoke(_hp);

    if (_hp <= 0) {
      Kill();
    }
  }

  public void Kill() {
    OnKilled?.Invoke();
    var sprite = GetComponent<SpriteRenderer>();
    var sequence = DOTween.Sequence();
    sequence
      .Append(sprite.DOFade(0f, 0.2f))
      .AppendCallback(() => Destroy(gameObject));
  }
}