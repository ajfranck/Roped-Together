using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HotBar : MonoBehaviour
{

    [SerializeField] private CanvasGroup BackpackUI;

    public Item TestItem;
    public Item CubeItem;



    public bool pickedUp = false;
    public bool promptDisplayed = false;

    public string itemNear = "";

    public static int HotBarPosition = 0;

    public WarmthBar warmthbar;

    public GameObject BackPackSprite;

    


   

    public List<GameObject> HotBarSpritesP1 = new List<GameObject>();


    public List<GameObject> P1BackpackSpritesList = new List<GameObject>();
    public List<GameObject> P2BackpackSpritesList = new List<GameObject>();

    public string name;


    public int BackpackPosition = 0;



    void Start()
    {
        HideBackpack();
        ClearInventory();

    }


    void Update()
    {

        HandleHotBar();


        if (warmthbar.isInteracting)
        {
            HandleBackPack();
            if(BackpackUI.alpha < 1){
                BackpackUI.alpha += Time.deltaTime;
            }
            
        }
        else{
            if(BackpackUI.alpha >= 0){
                BackpackUI.alpha -= Time.deltaTime;
            }
        }


    }

    public void HideBackpack(){
        BackpackUI.alpha = 0;
    }










    private void OnTriggerStay(Collider other)
    {
            
           



            if (other.gameObject.tag.Contains("CubeItem"))
            {

                if (!promptDisplayed)
                {
                    DisplayPrompt(CubeItem);
                }

                if (Input.GetKey("p") && !pickedUp)
                {
                    PickUpItem(CubeItem);
                }

            }


    }

        private void OnTriggerExit(Collider other)
        {

            if (other.gameObject.CompareTag("Fire1"))
            {
                pickedUp = false;
                promptDisplayed = false;
                BackPackSprite.SetActive(false);
            }

            if (other.gameObject.CompareTag("CubeItem"))
            {
                pickedUp = false;
                promptDisplayed = false;
            }

        }



        private void ClearInventory()
        {
            for (int i = 0; i < 4; i++)
            {
                HotBars.HotBarListP1.Add(null);
            }

            for (int i = 0; i < 11; i++)
            {
                 StaticBackPack.BackpackList.Add(null);

                 StaticBackPack.P1BackpackSpritesList = P1BackpackSpritesList;
                 StaticBackPack.P2BackpackSpritesList = P2BackpackSpritesList;
                 Debug.Log(StaticBackPack.P1BackpackSpritesList[i]);
                 Debug.Log(StaticBackPack.P2BackpackSpritesList[i]);
                   

            }

        }




        private void DisplayPrompt(Item TheItem)
        {
            itemNear = CubeItem.name;

            Debug.Log("pick up " + itemNear + "?");

            promptDisplayed = true;
        }    


        private void PickUpItem(Item TheItem)
        {

            HotBars.HotBarListP1[HotBars.HotBarPositionP1] = TheItem;
            pickedUp = true;
            Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1].name);
            HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = HotBars.HotBarListP1[HotBars.HotBarPositionP1].artwork;

        }


        private void HotBarToBackpack()
        {
            
            StaticBackPack.BackpackList[BackpackPosition] = HotBars.HotBarListP1[HotBars.HotBarPositionP1];
          
            
            Debug.Log("Backpack at position " + BackpackPosition + "is: " + StaticBackPack.BackpackList[BackpackPosition]);

            StaticBackPack.P1BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = HotBars.HotBarListP1[HotBars.HotBarPositionP1].artwork;



            HotBars.HotBarListP1[HotBars.HotBarPositionP1] = null;
            HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = null;

            
        }


        private void BackpackToHotbar()
        {
            HotBars.HotBarListP1[HotBars.HotBarPositionP1] = StaticBackPack.BackpackList[BackpackPosition];
          
            Debug.Log("HotBar is:" + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);


            HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = StaticBackPack.BackpackList[BackpackPosition].artwork;
      


            StaticBackPack.BackpackList[BackpackPosition] = null;
            StaticBackPack.P1BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = null;

        }


        private void HandleBackPack()
        {
            BackPackSprite.SetActive(true);

            if (Input.GetKeyDown("d") && BackpackPosition < 11)
            {
                BackpackPosition++;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if (Input.GetKeyDown("a") && BackpackPosition > 0)
            {
                BackpackPosition--;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if (Input.GetKeyDown("s"))
            {
                BackpackPosition += 3;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if (Input.GetKeyDown("w") && BackpackPosition > 2)
            {
                BackpackPosition -= 3;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }


            if (Input.GetKey("f") && HotBars.HotBarListP1[HotBars.HotBarPositionP1] != null && StaticBackPack.BackpackList[BackpackPosition] == null)
            {
                HotBarToBackpack();
                Debug.Log("runs f");
            }
            if (Input.GetKey("g") && StaticBackPack.BackpackList[BackpackPosition] != null && HotBars.HotBarListP1[HotBars.HotBarPositionP1] == null)
            {
                BackpackToHotbar();
                Debug.Log("runs g");
            }
        }


        private void HandleHotBar()
        {
            if (Input.GetKeyDown("1"))
            {

                HotBars.HotBarPositionP1 = 0;
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);

            }


            if (Input.GetKeyDown("2"))
            {


                HotBars.HotBarPositionP1 = 1;
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);

            }


            if (Input.GetKeyDown("3"))
            {
                HotBars.HotBarPositionP1 = 2;
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
            }


            if (Input.GetKeyDown("4"))
            {
                HotBars.HotBarPositionP1 = 3;
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
            }



            if (Input.GetKeyDown("u"))
            {

                if (HotBars.HotBarListP1[HotBars.HotBarPositionP1] == null)
                {
                    Debug.Log("Nothing in this hotbar position");
                }
                else
                {
                    HotBars.HotBarListP1[HotBars.HotBarPositionP1].CallFunction();
                }
            }


        }



}