using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour{
    [Header("Inscribed")]
    //Prefab for instantiating apples
    public GameObject   applePrefab;

    //Prefab for the bomb cube
    public GameObject   bombPrefab;

    //Chance each drop is a bomb instead of an apple
    [Range(0f, 1f)]
    public float        bombChance = 0.1f;

    //Speed at which the appleTree moves
    public float        speed = 1f;

    //Distance where AppleTree turns around
    public float        leftAndRightEdge = 10f;

    //Chance that the AppleTree will change directions
    public float        changeDirChance = 0.1f;

    //Seconds between Apples instantiations
    public float        appleDropDelay = 1f;

    [Header("Difficulty per Round")]
    //Extra tree speed added each round (0.3 = +30%)
    public float        speedIncreasePerRound = 0.3f;

    //Drop delay multiplied by this each round (lower = faster drops)
    public float        dropDelayMultPerRound = 0.8f;

    private RoundCounter roundCounter;

    // Start is called before the first frame update
    void Start()
    {
        roundCounter = FindFirstObjectByType<RoundCounter>();

        //Start dropping apples
        Invoke("DropApple", 2f);
    }

    int CurrentRound(){
        return (roundCounter != null) ? roundCounter.round : 1;
    }

    void DropApple(){
        //Occasionally drop a bomb instead of an apple
        GameObject prefab = applePrefab;
        if (bombPrefab != null && Random.value < bombChance) {
            prefab = bombPrefab;
        }

        GameObject drop = Instantiate<GameObject>(prefab);
        drop.transform.position = transform.position;

        //Round 1: 1.0x delay, 2: 0.8x, 3: 0.64x, 4: ~0.51x
        float delay = appleDropDelay * Mathf.Pow(dropDelayMultPerRound, CurrentRound() - 1);
        Invoke( "DropApple", delay);
    }

    // Update is called once per frame
    void Update()
    {
        //Round 1: 1.0x speed, 2: 1.3x, 3: 1.6x, 4: 1.9x
        float speedMult = 1f + speedIncreasePerRound * (CurrentRound() - 1);

        //Basic movement
        Vector3 pos = transform.position;
        pos.x += speed * speedMult * Time.deltaTime;
        transform.position = pos;

        //Change direction
        if (pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed); //move right
        } 
        else if (pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed); //move left
        }
    }

    void FixedUpdate() {
        if (Random.value < changeDirChance) {
            speed *= -1;
        }
    }
}