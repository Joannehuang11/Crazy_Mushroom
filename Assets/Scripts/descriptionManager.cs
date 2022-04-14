using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class descriptionManager : MonoBehaviour
{
    public GameObject video;
    public VideoPlayer vp;

    public Image image1;
    public Text text1;
    public Text text1_des;

    public Image image2;
    public Text text2;
    public Text text2_des;

    public Image image3;
    public Text text3;
    public Text text3_des;

    public int frame;
    public float timer;

    public Sprite junHuman;
    public Sprite intHuman;
    public Sprite proHuman;
    public Sprite magicHuman;
    public Sprite hungryHuman;
    public Sprite foodMush;
    public Sprite poisonMush;
    public Sprite magicMush;


    // Start is called before the first frame update
    void Start()
    {
        vp = video.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        frame = (int)vp.frame;

        timer += Time.deltaTime;
        float seconds = timer % 60;

        if (frame < 300)
        {
            text1.text = "Junior";
            text1_des.text = "Might eat Poison Mush";
            image1.sprite = junHuman;

            text2.text = "Intermediate";
            text2_des.text = "Can avoid Poison Mush";
            image2.sprite = intHuman;

            text3.text = "Pro";
            text3_des.text = "Can kill Poison Mush";
            image3.sprite = proHuman;
        }
        else if (frame > 299 && frame < 600)
        {
            text1.text = "Hungry";
            text1_des.text = "Need food immediately";
            image1.sprite = hungryHuman;

            text2.text = "Normal";
            text2_des.text = "Enjoying life";
            image2.sprite = junHuman;

            text3.text = "Too high";
            text3_des.text = "Get high and crazy";
            image3.sprite = magicHuman;
        }
        else if (frame > 599 && frame < 900)
        {
            text1.text = "Food Mush";
            text1_des.text = "Eat will increase health";
            image1.sprite = foodMush;

            text2.text = "Poison Mush";
            text2_des.text = "Eat will harm health";
            image2.sprite = poisonMush;

            text3.text = "Magic Mush";
            text3_des.text = "Eat will turn crazy";
            image3.sprite = magicMush;
        }
        else if (frame > 899 && frame < 1200)
        {
            text1.text = "Junior";
            text1_des.text = "Might eat Poison Mush";
            image1.sprite = junHuman;

            text2.text = "Intermediate";
            text2_des.text = "Can avoid Poison Mush";
            image2.sprite = intHuman;

            text3.text = "Pro";
            text3_des.text = "Can kill Poison Mush";
            image3.sprite = proHuman;
        }
        else if (frame > 1199 && frame < 1500)
        {
            text1.text = "Hungry";
            text1_des.text = "Need food immediately";
            image1.sprite = hungryHuman;

            text2.text = "Normal";
            text2_des.text = "Enjoying life";
            image2.sprite = junHuman;

            text3.text = "Too high";
            text3_des.text = "Get high and crazy";
            image3.sprite = magicHuman;
        }
        else if (frame > 1499 && frame < 1800)
        {
            text1.text = "Food Mush";
            text1_des.text = "Eat will increase health";
            image1.sprite = foodMush;

            text2.text = "Poison Mush";
            text2_des.text = "Eat will harm health";
            image2.sprite = poisonMush;

            text3.text = "Magic Mush";
            text3_des.text = "Eat will turn crazy";
            image3.sprite = magicMush;
        }
    }
}
