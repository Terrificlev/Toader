using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public TextMeshProUGUI lives;

   

    public TextMeshProUGUI youDied;
    
    public bool won = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        lives.text = player.amoountOfLivesLeft.ToString() ;
        if (player.amountOflilypadsPopulated >= 5)
        {
            youDied.text = "You Won!";
            won = true;
        }
        if (player.amoountOfLivesLeft <= 0 && !won)
        {
            youDied.text = "Game Over!";
        }
        
       
    }
    
}
