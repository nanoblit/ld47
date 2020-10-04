using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
  [Header("In Game")]
  public TMP_Text PointsText;
  public CanvasGroup HealthCanvasGroup;
  public List<Image> Hearts;
  public Image HurtOverlay;
  [Range(0f, 1f)] public float HurtOverlayFade;
  public float HurtOverlayFadeTime;
  [Header("End Game")]
  public CanvasGroup EndgameCanvasGroup;
  public TMP_Text GameOverText, BestScoreText;
  public CanvasGroup ButtonCanvasGroup;
  public float EndGameFadeTime;

  private int _points = 0;

  private void Start() {
    var player = Player.I;
    var playerHealth = player?.GetComponent<Health>();
    if (player && playerHealth) {
      player.OnAddPoints += UpdatePointsText;
      playerHealth.OnDamageDealt += UpdateHearts;
      playerHealth.OnDamageDealt += AnimateHurtOverlay;
      playerHealth.OnKilled += OpenEndGameMenu;
    }
    // HealthText.text = $"{playerHealth.Hp}";

    AnimateHearts();
  }

  public void TryAgain() {
    GameManager.I.LoadScene("Game");
  }

  private void UpdatePointsText(int points) {
    PointsText.text = $"{points}";
    _points = points;
  }

  private void UpdateHearts(int hp) {
    for (int i = 0; i < Hearts.Count; i++) {
      if (hp < i + 1) {
        Hearts[i].DOFade(0f, 0.5f);
      }
    }
  }

  private void AnimateHurtOverlay(int hp) {
    StartCoroutine(AnimateHurtOverlayCoroutine());
  }

  private IEnumerator AnimateHurtOverlayCoroutine() {
    HurtOverlay.DOFade(HurtOverlayFade, HurtOverlayFadeTime);
    yield return new WaitForSeconds(HurtOverlayFadeTime);
    HurtOverlay.DOFade(0f, HurtOverlayFadeTime);
  }

  private void OpenEndGameMenu() {
    StartCoroutine(AnimateEndGameMenu());
  }

  private void AnimateHearts() {
    for (int i = 0; i < Hearts.Count; i++) {
      Pulse(Hearts[i].transform, i * 0.7f);
    }
  }

  private void Pulse(Transform t, float delay) {
    var sequence = DOTween.Sequence();
    sequence
      .PrependInterval(delay)
      .AppendCallback(() => t.DOScale(0.85f, 0.2f).SetLoops(-1 , LoopType.Yoyo));
  }

  private IEnumerator AnimateEndGameMenu() {
    float bestScore = GameManager.I.BestScore;
    var wait = new WaitForSeconds(EndGameFadeTime);
    HealthCanvasGroup.DOFade(0f, EndGameFadeTime);
    PointsText.DOFade(0f, EndGameFadeTime);
    EndgameCanvasGroup.gameObject.SetActive(true);
    EndgameCanvasGroup.DOFade(1f, EndGameFadeTime);
    yield return wait;
    GameOverText.text = $"You died with <br> <size=130%><color=#FF0667>{_points}</color></size> points";
    GameOverText.DOFade(1f, EndGameFadeTime);
    yield return wait;
    if (bestScore > 0) {
      BestScoreText.text = $"Your best score was <size=130%><color=#FF0667>{bestScore}</color></size> points";
      BestScoreText.DOFade(1f, EndGameFadeTime);
      yield return wait;
    }
    ButtonCanvasGroup.DOFade(1f, EndGameFadeTime);
  }
}