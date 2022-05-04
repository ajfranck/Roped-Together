using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class P2HotBar : MonoBehaviour
{

    [SerializeField] private CanvasGroup BackpackUI;

    public Item TestItem;
    public Item CubeItem;
    public Item RopeItem;
    public Item BowlItem;
    public Item LadderItem;

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

    public List<GameObject> P1BackpackBackgroundsList = new List<GameObject>();

    public List<GameObject> P2BackpackSpritesList = new List<GameObject>();


    int addAmount = 0;
    public string name;
    public int BackpackPosition = 0;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //ClearInventory();
        BackgroundSelectHotBar(0);
    }

    void Update()
    {
        //Debug.Log("testing 12345 " + HotBars.HotBarListP2[HotBars.HotBarPositionP2].name);
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
            BackpackUI.alpha -= Time.deltaTime * 2f;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag.Contains("CubeItem"))
        {

            if (!hasGrabbed)
            {
                DisplayPrompt(CubeItem);
            }

            if (Input.GetKey("[4]"))
            {
                StartCoroutine(PickUp(CubeItem, other));
            }

        }

        if (other.gameObject.tag.Contains("RopeItem"))
        {
            if (!promptDisplayed && !hasGrabbed)
            {
                DisplayPrompt(RopeItem);
            }

            if (Input.GetKey("[4]") && !pickedUp)
            {
                StartCoroutine(PickUp(RopeItem, other));
            }
        }

        if (other.gameObject.tag.Contains("WarmthBowl"))
        {
            if (!hasGrabbed)
            {
                DisplayPrompt(BowlItem);
            }

            if (Input.GetKey("[4]"))
            {
                StartCoroutine(PickUp(BowlItem, other));
            }

        }
        if (other.gameObject.tag.Contains("LadderItem"))
        {

            if (!hasGrabbed)
            {
                DisplayPrompt(LadderItem);
            }

            if (Input.GetKey("[4]"))
            {
                StartCoroutine(PickUp(LadderItem, other));
            }

        }
    }

    IEnumerator PickUp(Item item, Collider other)
    {
        hasGrabbed = true;
        HidePrompt(item);
        GrabPrompt();
        isGrabbing = true;
        animator.SetTrigger("Grab");
        yield return new WaitForSeconds(1.5f);
        PickUpItem(item);
        //other.gameObject.SetActive(false);
        HideGrabPrompt();
        HidePrompt(item);
        if (other.gameObject.tag.Contains("Pickaxe") || other.gameObject.tag.Contains("Bowl") || other.gameObject.CompareTag("LadderItem")) other.gameObject.SetActive(false);
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

        if (other.gameObject.CompareTag("RopeItem"))
        {
            HidePrompt(RopeItem);
            pickedUp = false;
            promptDisplayed = false;
            hasGrabbed = false;
        }

        if (other.gameObject.CompareTag("WarmthBowl"))
        {
            HidePrompt(BowlItem);
            pickedUp = false;
            promptDisplayed = false;
            hasGrabbed = false;
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

            HotBars.HotBarListP2[HotBars.HotBarPositionP2] = TheItem;
            pickedUp = true;
            Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP2 + "is: " + HotBars.HotBarListP2[HotBars.HotBarPositionP2].name);
            HotBarSpritesP1[HotBars.HotBarPositionP2].GetComponent<Image>().sprite = HotBars.HotBarListP2[HotBars.HotBarPositionP2].artwork;

        }


        private void HotBarToBackpack()
        {
            //sets backpack to hotbar
            StaticBackPack.BackpackList[BackpackPosition] = HotBars.HotBarListP2[HotBars.HotBarPositionP2];
          
            
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
            HotBars.HotBarListP2[HotBars.HotBarPositionP2] = StaticBackPack.BackpackList[BackpackPosition];
          
            Debug.Log("HotBar is:" + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);

            //adds sprite to P1 Hotbar.
            HotBarSpritesP1[HotBars.HotBarPositionP2].GetComponent<Image>().sprite = StaticBackPack.BackpackList[BackpackPosition].artwork;
            

            //removes from backpack
            StaticBackPack.BackpackList[BackpackPosition] = null;

            //sets backpack sprites to null for both backpacks
            StaticBackPack.P1BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = HotBarBackground.GetComponent<Image>().sprite;
            StaticBackPack.P2BackpackSpritesList[BackpackPosition].GetComponent<Image>().sprite = HotBarBackground.GetComponent<Image>().sprite;

        }


        private void HandleBackPack()
        {

            SelectItemBackpack();
            if (Input.GetKeyDown("[5]") && BackpackPosition < 11)
            {
                addAmount = 1;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
                BackgroundSelect(addAmount);
            }
            if (Input.GetKeyDown("[1]") && BackpackPosition > 0)
            {
                addAmount = -1;
                //BackpackPosition--;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
                BackgroundSelect(addAmount);
            }
            if (Input.GetKeyDown("[2]") && BackpackPosition<9)
            {
                addAmount = 3;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
                BackgroundSelect(addAmount);
            }
            if (Input.GetKeyDown("[3]") && BackpackPosition > 2)
            {
                addAmount = -3;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
                BackgroundSelect(addAmount);
            }


            if (Input.GetKey("[4]") && HotBars.HotBarListP1[HotBars.HotBarPositionP1] != null && StaticBackPack.BackpackList[BackpackPosition] == null)
            {
                HotBarToBackpack();
                Debug.Log("runs f");
            }
            if (Input.GetKey("[6]") && StaticBackPack.BackpackList[BackpackPosition] != null && HotBars.HotBarListP1[HotBars.HotBarPositionP1] == null)
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
            StaticBackPack.P2BackpackBackgroundsList[oldPosition].SetActive(false);

            Debug.Log("old backpack spot " + BackpackPosition);
            BackpackPosition = oldPosition+amountToAdd;
            Debug.Log("new backpack spot " + BackpackPosition);

            StaticBackPack.P1BackpackBackgroundsList[BackpackPosition].SetActive(true);
            StaticBackPack.P2BackpackBackgroundsList[BackpackPosition].SetActive(true);
           
        }
        
        private void BackgroundSelectHotBar(int position)
        {
            int oldPosition = HotBars.HotBarPositionP2;
            HotBars.HotBarBackgroundsListP2[oldPosition].SetActive(false);
            HotBars.HotBarPositionP2 = position;
            HotBars.HotBarBackgroundsListP2[HotBars.HotBarPositionP2].SetActive(true);
        }
        

    private void HandleHotBar()
    {
            if (Input.GetKeyDown("[7]"))
            {

                HotBarPosition = 0;
                BackgroundSelectHotBar(HotBarPosition);
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
                
            }


            if (Input.GetKeyDown("[8]"))
            {

                HotBarPosition = 1;
                BackgroundSelectHotBar(HotBarPosition);
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);

            }



            if (Input.GetKeyDown("[4]"))
            {

                if (HotBars.HotBarListP2[HotBars.HotBarPositionP2] == null)
                {
                    Debug.Log("Nothing in this hotbar position");
                }
                else
                {
                    HotBars.HotBarListP2[HotBars.HotBarPositionP2].CallFunction();
                }
            }


    }


   


}