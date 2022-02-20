using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    
    public int maxHealth;
    private int curHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 10000;
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        curHealth--;
        changeHealthBar();
    }

    void changeHealthBar()
    {
        UI_HealthBar.instance.SetValue(curHealth / (float)maxHealth);
    }
}
