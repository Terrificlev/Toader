using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public TextMeshProUGUI lives;

    public TextMeshProUGUI youDied;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = player.amoountOfLivesLeft.ToString() ;

        if (player.amoountOfLivesLeft <= 0)
        {
            youDied.text = "Game Over!";
        }
    }
}
