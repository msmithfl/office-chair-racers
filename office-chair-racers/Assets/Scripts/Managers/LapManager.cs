using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    /*
    this script is placed on the finish line/lap marker

    when a game object enters the lap collider, it checks if it is the player
    it then grabs a reference to the player and checks if the player's checkpointIndex equals the total amount of checkpoints
    if the check is successful, the player's lapNumber is increased and the checkpointIndex is reset to 0
    last, it checks if the player's lapNumber is greater than the total lap count to determine a winner
    */

    public List<Checkpoint> checkpoints;
    public int totalLaps;

    private bool m_RaceIsOver = false;

    [SerializeField] private PlayModeCanvasManager m_PlayModeCanvasManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            int playerIndex = other.gameObject.GetComponent<PlayerSpawnSetup>().playerIndex;

            if(player.checkpointIndex == checkpoints.Count)
            {
                player.lapNumber++;
                player.checkpointIndex = 0;

                //check for winner
                if (player.lapNumber > totalLaps)
                {
                    if (!m_RaceIsOver)
                    {
                        m_RaceIsOver = true;
                        m_PlayModeCanvasManager.DisplayWinnerText(playerIndex);
                    }
                }

                m_PlayModeCanvasManager.UpdateLapUI(playerIndex, player); 
            }
        }
    }
}
