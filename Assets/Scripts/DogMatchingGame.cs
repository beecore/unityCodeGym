using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DogMatchingGame : MonoBehaviour
{
    public Sprite[] dogBreeds; // Array of dog breed images
    public TextMeshPro dogTranslatorText; // Text displaying dog breed translations
    public GameObject tilePrefab; // Prefab for the game tiles
    public Transform gridParent; // Parent object for the grid of tiles
    public float timeLimit = 60f; // Time limit for the game
    public int maxMistakes = 3; // Maximum allowed mistakes

    private List<Sprite> shuffledDogBreeds = new List<Sprite>(); // Shuffled list of dog breed images
    private List<GameObject> tiles = new List<GameObject>(); // List of game tiles
    private bool gameActive = false; // Flag to track game state
    private int matchesMade = 0; // Number of matches made by the player
    private int mistakesMade = 0; // Number of mistakes made by the player
    private float currentTime = 0f; // Current time remaining




    void Start()
    {
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0 || mistakesMade >= maxMistakes)
            {
                EndGame(false);
            }
        }
    }
    void InitializeGame()
    {
        // Reset game variables
        gameActive = true;
        matchesMade = 0;
        mistakesMade = 0;
        currentTime = timeLimit;

        // Shuffle dog breed images
        shuffledDogBreeds.Clear();
        foreach (Sprite breed in dogBreeds)
        {
            shuffledDogBreeds.Add(breed);
            shuffledDogBreeds.Add(breed); // Duplicate each breed for matching pairs
        }
        //  shuffledDogBreeds.Shuffle();

        // Create game tiles
        foreach (Sprite breed in shuffledDogBreeds)
        {
            GameObject tile = Instantiate(tilePrefab, gridParent);
            tile.GetComponentInChildren<SpriteRenderer>().sprite = breed;
            tiles.Add(tile);
        }

        //// Update dog translator text
        UpdateDogTranslatorText();
    }
    public void TileClicked(GameObject clickedTile)
    {
        if (!gameActive) return;

        // Check if the clicked tile matches the current dog breed
        Sprite currentBreed = shuffledDogBreeds[matchesMade];
        Sprite clickedBreed = clickedTile.GetComponentInChildren<Image>().sprite;

        if (currentBreed == clickedBreed)
        {
            matchesMade++;
            clickedTile.SetActive(false);

            if (matchesMade >= dogBreeds.Length)
            {
                EndGame(true);
            }
            else
            {
                UpdateDogTranslatorText();
            }
        }
        else
        {
            mistakesMade++;
        }
    }

    void UpdateDogTranslatorText()
    {
       // dogTranslatorText.text = $"Translate: {shuffledDogBreeds[matchesMade].name}";
    }

    void EndGame(bool victory)
    {
        gameActive = false;
        if (victory)
        {
            Debug.Log("Congratulations! You win!");
        }
        else
        {
            Debug.Log("Game over! You lose!");
        }
    }
}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
