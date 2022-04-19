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
    
    private Animator animator;
    public bool isGrabbing = false;


    public bool pickedUp = false;
    public bool promptDisplayed = false;
    public bool hasGrabbed = false;

    public string itemNear = "";

    public int HotBarPosition = 0;

    public WarmthBar warmthbar;


    //UI SPRITES
    public GameObject BackPackSprite;
    public GameObject DescriptionText;
    public GameObject ImageSprite; //image to left of backPack, set to item image

    public GameObject BackgroundImage; //image that background list at position is set to when selected;

    public GameObject p1Pickup;
    public GameObject p1Grab;

    public GameObject HotBarBackground; // second hotbar border that gets replaced with item image


    public List<GameObject> HotBarSpritesP1 = new List<GameObject>();

    public List<GameObject> P1BackpackSpritesList = new List<GameObject>();

    public List<GameObject> HotBarBackgroundsListP1 = new List<GameObject>();

    public  List<GameObject> P1BackpackBackgroundsList = new List<GameObject>();

    public List<GameObject> P2BackpackSpritesList = new List<GameObject>();


    int addAmount = 0;
    public string name;
    public int BackpackPosition = 0;

    
    void Start()
    {       
        animator = gameObject.GetComponent<Animator>();
        ClearInventory();
        BackgroundSelectHotBar(0);
    }

    void Update()
    {
        
        HandleHotBar();

        if (warmthbar.isInteracting)
        {
            HandleBackPack();
            BackgroundSelect(0);
            StartCoroutine(BackpackFadeIn());          
        }
        else
        {
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

            if (!promptDisplayed && !hasGrabbed)
            {
                DisplayPrompt(CubeItem);
            }

            if (Input.GetKey("p") && !pickedUp)
            {
                StartCoroutine(PickUp(CubeItem, other));
            }

        }
    }

    IEnumerator PickUp(Item item, Collider other)
    {
        hasGrabbed = true;
        HidePrompt(CubeItem);
        GrabPrompt();
        isGrabbing = true;
        animator.SetTrigger("Grab");
        yield return new WaitForSeconds(1.5f);
        PickUpItem(item);
        other.gameObject.SetActive(false);
        HideGrabPrompt();
        HidePrompt(CubeItem);
        isGrabbing = false;
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
            HidePrompt(CubeItem);
            pickedUp = false;
            promptDisplayed = false;
            hasGrabbed = false;
        }

    }

        private void ClearInventory()
        {
            for (int i = 0; i < 2; i++)
            {
                HotBars.HotBarListP1.Add(null);
                HotBars.HotBarBackgroundsListP1.Add(null);
            }

            for (int i = 0; i < 11; i++)
            {
                 StaticBackPack.BackpackList.Add(null);
                 StaticBackPack.P1BackpackBackgroundsList.Add(null);

                 StaticBackPack.P1BackpackSpritesList = P1BackpackSpritesList;
                 StaticBackPack.P1BackpackBackgroundsList = P1BackpackBackgroundsList;
                 HotBars.HotBarBackgroundsListP1 = HotBarBackgroundsListP1;   

                    
                 StaticBackPack.P2BackpackSpritesList = P2BackpackSpritesList;

                 
                 Debug.Log(StaticBackPack.P1BackpackSpritesList[i]);
                 Debug.Log(StaticBackPack.P2BackpackSpritesList[i]);    

            }

        }

        private void DisplayPrompt(Item TheItem)
        {
            p1Pickup.SetActive(true);
            promptDisplayed = true;
            itemNear = TheItem.name;
            
        }  
        private void HidePrompt(Item TheItem)
        {
            p1Pickup.SetActive(false);
            promptDisplayed = false;
            itemNear = TheItem.name;  
        }
        private void GrabPrompt()
        {
            p1Grab.SetActive(true);      
        }  
        private void HideGrabPrompt()
        {
            p1Grab.SetActive(false); 
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
            HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = HotBarBackground.GetComponent<Image>().sprite;

            
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
            StaticBackPack.P1BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = HotBarBackground.GetComponent<Image>().sprite;
            StaticBackPack.P2BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = HotBarBackground.GetComponent<Image>().sprite;

        }


        private void HandleBackPack()
        {

            SelectItemBackpack();
            if (Input.GetKeyDown("d") && BackpackPosition < 11)
            {
                addAmount = 1;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
                BackgroundSelect(addAmount);
            }
            if (Input.GetKeyDown("a") && BackpackPosition > 0)
            {
                addAmount = -1;
                //BackpackPosition--;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
                BackgroundSelect(addAmount);
            }
            if (Input.GetKeyDown("s") && BackpackPosition<9)
            {
                addAmount = 3;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
                BackgroundSelect(addAmount);
            }
            if (Input.GetKeyDown("w") && BackpackPosition > 2)
            {
                addAmount = -3;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
                BackgroundSelect(addAmount);
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
           // StaticBackPack.P1BackpackBackgroundsList[BackpackPosition].SetActive(true);

            else {
                DescriptionText.GetComponent<TextMeshProUGUI>().text = null;
                ImageSprite.GetComponent<Image>().sprite = null;
               // StaticBackPack.P1BackpackBackgroundsList[BackpackPosition].SetActive(false);
            }
        }


        private void BackgroundSelect(int amountToAdd)
        {
            int oldPosition = BackpackPosition;                     
            StaticBackPack.P1BackpackBackgroundsList[oldPosition].SetActive(false);
            Debug.Log("old backpack spot " + BackpackPosition);
            BackpackPosition = oldPosition+amountToAdd;
            Debug.Log("new backpack spot " + BackpackPosition);
            StaticBackPack.P1BackpackBackgroundsList[BackpackPosition].SetActive(true);
           
        }
        
        private void BackgroundSelectHotBar(int position)
        {
            int oldPosition = HotBars.HotBarPositionP1;
            HotBars.HotBarBackgroundsListP1[oldPosition].SetActive(false);
            HotBars.HotBarPositionP1 = position;
            HotBars.HotBarBackgroundsListP1[HotBars.HotBarPositionP1].SetActive(true);
        }
        

    private void HandleHotBar()
    {
            if (Input.GetKeyDown("1"))
            {

                HotBarPosition = 0;
                BackgroundSelectHotBar(HotBarPosition);
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
                
            }


            if (Input.GetKeyDown("2"))
            {

                HotBarPosition = 1;
                BackgroundSelectHotBar(HotBarPosition);
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