using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Health Player;
    public Health Enemy1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Player.TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Enemy1.TakeDamage(10);
        }
    }
}
