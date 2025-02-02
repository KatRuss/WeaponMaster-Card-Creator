using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class CardController : MonoBehaviour
{

    [Header("Card Components")]
    public Deck deckToPrint;
    public string printDirectory;
    public new Camera camera;

    [Header("Top Element")]
    public Image headerTop;
    public TextMeshProUGUI actionTypeTop;
    public TextMeshProUGUI costTop;
    public TextMeshProUGUI titleTop;
    public TextMeshProUGUI descriptionTop;
    public TextMeshProUGUI speedTop;
    public SpriteRenderer iconTop;

    [Header("Bottom Element")]
    public Image headerBottom;
    public TextMeshProUGUI actionTypeBottom;
    public TextMeshProUGUI costBottom;
    public TextMeshProUGUI titleBottom;
    public TextMeshProUGUI descriptionBottom;
    public TextMeshProUGUI speedBottom;
    public SpriteRenderer iconBottom;


    [Header("Colour Palatte")]
    public Color actionColor;
    public Color attackColor;
    public Color parryColor;
    public Color moveColor;

    [Header("Other Icons")]
    public Image startingDeckIcon;
    public Image weaponDeckIcon;

    public Sprite genericImage;
    public Sprite longswordImage;
    public Sprite halberdImage;
    public Sprite sabreImage;
    public Sprite swordbuckImage;

    private void Start()
    {
        PrintDeck();
    }

    public void Capture(string id)
    {
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        camera.Render();

        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        File.WriteAllBytes(Application.dataPath + printDirectory + "/" + id + ".png", bytes);
        Debug.Log("Card: " + id + " Created");
    }

    public void PrintDeck()
    {
        for (int i = 0; i < deckToPrint.cards.Length; i++)
        {
            ConstructCard(deckToPrint.cards[i]);
            Capture(deckToPrint.cards[i].name);
            //Print Card to Image File
        }
    }


    public void ConstructCard(Card card)
    {

        //Starter Card Icon
        if (card.isStarter)
        {
            //make starter icon visable
            startingDeckIcon.enabled = true;
        } 
        else 
        {
            //make starter icon invisible
            startingDeckIcon.enabled = false;
        }

        //weapon type icon
        switch(card.weaponType){
            case WEAPON_TYPE.GENERIC:
                weaponDeckIcon.enabled = false;
                break;

            case WEAPON_TYPE.SWORDBUCKLER:
                weaponDeckIcon.enabled = true;
                weaponDeckIcon.sprite = swordbuckImage;                
                break;

            case WEAPON_TYPE.LONGSWORD:
                weaponDeckIcon.enabled = true;
                weaponDeckIcon.sprite = longswordImage;                
                break;
            case WEAPON_TYPE.HALBERD:
                weaponDeckIcon.enabled = true;
                weaponDeckIcon.sprite = halberdImage;                
                break;

            case WEAPON_TYPE.SABRE:
                weaponDeckIcon.enabled = true;
                weaponDeckIcon.sprite = sabreImage;                
                break;
        }

        //Top Element
        //Card Header
        switch (card.topAction.actionType)
        {
            case ACTION_TYPE.ACTION:
                headerTop.color = actionColor;
                actionTypeTop.text = "Ability";
                break;
            case ACTION_TYPE.ATTACK:
                headerTop.color = attackColor;
                actionTypeTop.text = "Attack";
                break;
            case ACTION_TYPE.PARRY:
                headerTop.color = parryColor;
                actionTypeTop.text = "Parry";
                break;
            case ACTION_TYPE.MOVE:
                headerTop.color = moveColor;
                actionTypeTop.text = "Move";
                break;
        }
        //Card Body
        costTop.text = card.topAction.cost.ToString();
        titleTop.text = card.topAction.title; ;
        descriptionTop.text = card.topAction.description;
        speedTop.text = card.topAction.speed;
        iconTop.sprite = card.topAction.referenceImage;

        //Bottom Element
        //Card Header
        switch (card.bottomAction.actionType)
        {
            case ACTION_TYPE.ACTION:
                headerBottom.color = actionColor;
                actionTypeBottom.text = "Ability";
                break;
            case ACTION_TYPE.ATTACK:
                headerBottom.color = attackColor;
                actionTypeBottom.text = "Attack";
                break;
            case ACTION_TYPE.PARRY:
                headerBottom.color = parryColor;
                actionTypeBottom.text = "Parry";
                break;
            case ACTION_TYPE.MOVE:
                headerBottom.color = moveColor;
                actionTypeBottom.text = "Move";
                break;
        }
        //Card Body
        costBottom.text = card.bottomAction.cost.ToString();
        titleBottom.text = card.bottomAction.title; ;
        descriptionBottom.text = card.bottomAction.description;
        speedBottom.text = card.bottomAction.speed;
        iconBottom.sprite = card.bottomAction.referenceImage;
    }

}

