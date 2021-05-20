using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject Minimap;

    private bool isMinimapActive = false;

    void Start()
    {
        Minimap.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Minimap.SetActive(SwitchMinimapActivity());
        }
    }

    public bool SwitchMinimapActivity()
    {
        return isMinimapActive == false
            ? isMinimapActive = true
            : isMinimapActive = false;
    }
}
