using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class MonstersInLevel : MonoBehaviour
{
    public List<GameObject> monstersInLevel = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI monsterCountText;
    public int monsterCount = 0;

    private void Start()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            monstersInLevel.Add(monster);
        }
    }

    private void Update()
    {
        monsterCountText.text = "Monsters: " + monsterCount + "/" + monstersInLevel.Count.ToString();
    }
}
