using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalZoneManager : MonoBehaviour
{
    public bool AllValid = false;
    private List<GoalZone> Zones = new List<GoalZone>();

    private UIController ui;
    private void Awake()
    {
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
        Zones = GameObject.FindObjectsOfType<GoalZone>().ToList();
    }
    // Update is called once per frame
    void Update()
    {
        AllValid = Zones.All(x => x.GoalZoneValid);
        if (AllValid)
        {
            ui.Over();
        }
    }
}
