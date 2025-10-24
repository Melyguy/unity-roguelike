using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public LightingManager DayNight;
    public GameObject boss;
    private GameObject Player;

    private void Update()
    {
        Player = GameObject.FindWithTag("Player");
        if (DayNight.daysPassed == 3)
        {
            boss.transform.position = Player.transform.position + new Vector3(0, 15, 10);

        }
    }
}
