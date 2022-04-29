using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healing : MonoBehaviour
{
    public Mining mining;
    public HotBar hotbar;
    public WarmthBar warmthbar;

    float warmthStart;

    void Update()
    {
        if (Input.GetKey("e") && HotBars.HotBarListP1[HotBars.HotBarPositionP1] != null && HotBars.HotBarListP1[HotBars.HotBarPositionP1].name == "BowlItem" && !mining.isMining && !warmthbar.isInteracting)
        {
            HotBars.HotBarListP1[HotBars.HotBarPositionP1] = null;
            hotbar.HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = hotbar.BackgroundImage.GetComponent<Image>().sprite;

            warmthStart = warmthbar.P1currentWarmth;
            if(warmthStart + 50 > warmthbar.P1MaxWarmth) warmthStart = warmthbar.P1MaxWarmth - 1;
            else warmthStart += 50;
            while(warmthbar.P1currentWarmth < warmthStart)
            {
                warmthbar.gainWarmth(0.5f);
            }
            
        }
    }

}
