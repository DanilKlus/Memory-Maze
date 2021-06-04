using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Diagnostics;
using System.Threading;

public class PlayerControl : MonoBehaviour
{
    public GameObject Minimap;
    private int count = 0;
    public int MaxCount = 3;
    private bool isMinimapActive = false;

    private double elapsedTime;

    public double ElapsedTime => elapsedTime;

    void Start()
    {
        Minimap.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && count < MaxCount && elapsedTime == 0)
        {
            count++;
            Minimap.SetActive(isMinimapActive = true);
            var thread = new Thread(new ThreadStart(() => UpdateMinimap()));
            thread.Start();
        }
        if (isMinimapActive && elapsedTime == 5)
        {
            Minimap.SetActive(isMinimapActive = false);
            elapsedTime = 0;
        }
    }

    private void UpdateMinimap()
    {
        if (isMinimapActive)
        {
            var watch = new Stopwatch();
            watch.Start();
            while (elapsedTime < 5)
            {
                elapsedTime = watch.ElapsedMilliseconds / 1000;
            }
            watch.Stop();
        }
    }

    public bool SwitchMinimapActivity()
    {
        return isMinimapActive == false
            ? isMinimapActive = true
            : isMinimapActive = false;
    }
}