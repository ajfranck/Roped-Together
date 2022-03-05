using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    //UI SPRITES
    public GameObject BackPackSprite;
    public GameObject DescriptionText;
    public GameObject ImageSprite;
    public GameObject p1Pickup;

    public List<GameObject> HotBarSpritesP1 = new List<GameObject>();

    public List<GameObject> P1BackpackSpritesList = new List<GameObject>();
    public List<GameObject> P2BackpackSpritesList = new List<GameObject>();

    public string name;
    public int BackpackPosition = 0;

  
    void Start()
    {
       
        ClearInventory();
    }

    void Update()
    {
        if(!promptDisplayed)
        {
            p1Pickup.SetActive(false);
        }
        HandleHotBar();
        if (warmthbar.isInteracting)
        {
            HandleBackPack();
            StartCoroutine(BackpackFadeIn());          
        }

        else{
            StartCoroutine(BackpackFadeOut());
        }


    }

    IEnumerator BackpackFadeIn()
    {
        yield return new WaitForSeconds(0.7f);
        if (BackpackUI.alpha < 1f)
        {

            BackpackUI.alpha += Time.deltaTime;
        }



    }

    IEnumerator BackpackFadeOut()
    {
        yield return new WaitForSeconds(0.0001f);
        if (BackpackUI.alpha > 0f)
        {
            BackpackUI.alpha -= Time.deltaTime*2f;
        }
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
            itemNear = TheItem.name;

            Debug.Log("pick up " + itemNear + "?");
            p1Pickup.SetActive(true);

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
            //sets backpack to hotbar
            StaticBackPack.BackpackList[BackpackPosition] = HotBars.HotBarListP1[HotBars.HotBarPositionP1];
          
            
            Debug.Log("Backpack at position " + BackpackPosition + "is: " + StaticBackPack.BackpackList[BackpackPosition]);


            //sets backpack sprites for P1 and P2 (they must be seperate because they are seperate sprites)
            StaticBackPack.P1BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = HotBars.HotBarListP1[HotBars.HotBarPositionP1].artwork;
            StaticBackPack.P2BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = HotBars.HotBarListP1[HotBars.HotBarPositionP1].artwork;

            //removes from hotbar
            HotBars.HotBarListP1[HotBars.HotBarPositionP1] = null;
            HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = null;

            
        }


        private void BackpackToHotbar()
        {
            //adds to p1 hotbar.
            HotBars.HotBarListP1[HotBars.HotBarPositionP1] = StaticBackPack.BackpackList[BackpackPosition];
          
            Debug.Log("HotBar is:" + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);

            //adds sprite to P1 Hotbar.
            HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = StaticBackPack.BackpackList[BackpackPosition].artwork;
            

            //removes from backpack
            StaticBackPack.BackpackList[BackpackPosition] = null;

            //sets backpack sprites to null for both backpacks
            StaticBackPack.P1BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = null;
            StaticBackPack.P2BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = null;

        }


        private void HandleBackPack()
        {
            SelectItemBackpack();
           
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

        private void SelectItemBackpack()
        {
            if (StaticBackPack.BackpackList[BackpackPosition] != null)
            {
                DescriptionText.GetComponent<TextMeshProUGUI>().text = StaticBackPack.BackpackList[BackpackPosition].description;
                ImageSprite.GetComponent<Image>().sprite = StaticBackPack.BackpackList[BackpackPosition].image;
        }
            else {
                DescriptionText.GetComponent<TextMeshProUGUI>().text = null;
                ImageSprite.GetComponent<Image>().sprite = null;
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