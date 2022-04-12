using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class descriptionManager2 : MonoBehaviour
{
    public GameObject creatorPanel;
    public GameObject descriptionPanel;

    public Image image;
    public Text text;

    public Sprite junHuman;
    public Sprite intHuman;
    public Sprite proHuman;
    public Sprite magicHuman;
    public Sprite hungryHuman;
    public Sprite foodMush;
    public Sprite poisonMush;
    public Sprite magicMush;

    int count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count < 500)
        {
            creatorPanel.SetActive(true);
            descriptionPanel.SetActive(false);
        }
        else if ( count > 499 && count < 1000)
        {
            creatorPanel.SetActive(false);
            descriptionPanel.SetActive(true);

            text.text = "Junior";
            image.sprite = junHuman;
        }
        else if (count > 999 && count < 1500)
        {
            text.text = "Intermediate";
            image.sprite = intHuman;
        }
        else if (count > 1499 && count < 2000)
        {
            text.text = "Pro";
            image.sprite = proHuman;
        }
        else if (count > 1999 && count < 2500)
        {
            text.text = "Normal";
            image.sprite = junHuman;
        }
        else if (count > 2499 && count < 3000)
        {
            text.text = "Hungry";
            image.sprite = hungryHuman;
        }
        else if (count > 2999 && count < 3500)
        {
            text.text = "High";
            image.sprite = magicHuman;
        }
        else if (count > 3499 && count < 4000)
        {
            text.text = "Food Mush";
            image.sprite = foodMush;
        }
        else if (count > 3999 && count < 4500)
        {
            text.text = "Poison Mush";
            image.sprite = poisonMush;
        }
        else if (count > 4499 && count < 5000)
        {
            text.text = "Magic Mush";
            image.sprite = magicMush;
        }
        else if (count == 5000)
        {
            count = 0;
        }
    }
}
