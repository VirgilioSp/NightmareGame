using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int health = 8; 
    public Image[] pills; 

    public Sprite FullPill;
    public Sprite HalfPill;
    public Sprite EmptyPill;

    void Update()
    {
        int totalPills = pills.Length;
        int fullPills = health / 2;
        bool hasHalfPill = (health % 2 == 1);

        foreach (Image img in pills)
        {
            img.sprite = EmptyPill;
        }

        for (int i = 0; i < fullPills; i++)
        {
            pills[i].sprite = FullPill;
        }

        if (hasHalfPill && fullPills < totalPills)
        {
            pills[fullPills].sprite = HalfPill;
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > pills.Length * 2)
        {
            health = pills.Length * 2;
        }
    }

}
