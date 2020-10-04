using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
  public event Action OnBeginLoadLevel;

  public List<Transform> EnemyPrefabs;
  public Color StartingBackgoundColor;
  [HideInInspector] public int BestScore = 0;

  private System.Random _random;

  public static float ViewHalfWidth {
    get {
      return Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0f)).x;
    }
  }

  private Coroutine _runGameCoroutine;

  protected override void Awake() {
    base.Awake();
  }

  private void Start() {
    DontDestroyOnLoad(gameObject);
    if (SceneManager.GetActiveScene().name == "Game") {
      RunGame();
    }
    SceneManager.sceneLoaded += RunGameIfGameScene;
  }

  public void LoadScene(string sceneName) {
    OnBeginLoadLevel?.Invoke();
    StopGame();
    SceneManager.LoadScene(sceneName);
  }

  private void RunGameIfGameScene(Scene scene, LoadSceneMode mode) {
    if (scene.name == "Game") {
      RunGame();
    }
  }

  private void RunGame() {
    _random = new System.Random(150);
    _runGameCoroutine = StartCoroutine(RunGameCoroutine());
  }

  private void StopGame() {
    if (_runGameCoroutine != null) {
      StopCoroutine(_runGameCoroutine);
    }
  }

  private IEnumerator RunGameCoroutine() {
    float minWaitTime = 0.5f;
    while (true) {
      float higherTime = minWaitTime + 1f >= 0.5f ? minWaitTime + 1f : 0.5f;
      float lowerTime = minWaitTime >= 0.5f ? minWaitTime : 0.5f;
      yield return new WaitForSeconds((float) _random.NextDouble() * (higherTime - lowerTime) + lowerTime);
      minWaitTime -= 0.05f;

      if (EnemyPrefabs.Count > 0) {
        int randomEnemyIndex = (int) Mathf.Round(_random.Next(0, EnemyPrefabs.Count));
        var enemyToSpawn = EnemyPrefabs[randomEnemyIndex];

        float enemyHalfHeight = enemyToSpawn.GetComponent<SpriteRenderer>().bounds.extents.y;
        float spawnPostionX = -ViewHalfWidth + (float) _random.NextDouble() * 2 * ViewHalfWidth;
        float spawnPostionY = Camera.main.orthographicSize + enemyHalfHeight;
        var enemyPosition = new Vector2(spawnPostionX, spawnPostionY);

        var enemyRotation = enemyToSpawn.rotation * Quaternion.Euler(0f, 0f, -45f + (float) _random.NextDouble() * 90f);

        Instantiate(enemyToSpawn, enemyPosition, enemyRotation);
      }
    }
  }
}