using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int      numBaskets = 4;
    public float    basketBottomY = -14f;
    public float    basketSpacingY = 2f;
    public List<GameObject> basketList;

    [Header("UI")]
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;   // unpause in case we came from a Game Over restart

        if (gameOverPanel != null) {
            gameOverPanel.SetActive(false);
        }

        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++) {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleMissed() {
        // Ignore misses once the game is already over
        if (basketList.Count == 0) return;

        DestroyAllApples();

        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        if (basketList.Count == 0) {
            GameOver();
        }
    }

    // Called by a basket when it catches a bomb
    public void BombCaught() {
        if (basketList.Count == 0) return;   // already game over

        foreach (GameObject basketGO in basketList) {
            Destroy(basketGO);
        }
        basketList.Clear();

        DestroyAllApples();
        GameOver();
    }

    void DestroyAllApples() {
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tempGO in appleArray) {
            Destroy(tempGO);
        }
    }

    void GameOver() {
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
        Time.timeScale = 0f;   // freeze the tree and stop apples dropping
    }

    // Hooked up to the Restart button's On Click ()
    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}