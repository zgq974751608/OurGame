using UnityEngine;
using System.Collections;

public class ArrangeHeroButton : BaseButton
{
    private float currentTime;
    float pressTime;
    bool longPress = false;
    public float pressDetection = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentTime = Time.realtimeSinceStartup;
        if (currentTime > pressTime + pressDetection && longPress == true)
        {
            io.panelManager.CreatePanel(DialogType.HeroTips);
            longPress = false;
        }

    }

    public void OnPress(bool press)
    {
        longPress = press;

        if (currentTime <= pressTime + pressDetection)
        {
            GameObject obj = this.gameObject;
            if (obj == null)
            {
                return;
            }

            UISprite sprite = obj.GetComponent<UISprite>();
            if (sprite.atlas)
            {
                foreach (var element in ArrangementPanel.spritename)
                {
                    if (sprite.spriteName == element.Value)
                    {
                        PlayerData.GetInstance().CurrentFightHero.Remove(element.Key);
                        break;
                    }
                }
                sprite.atlas = null;
                sprite.MakePixelPerfect();

                if (sprite.spriteName == "Headpic0008.PNG")
                {
                    GameObject captian = GameObject.Find("Bottom/Captainbag/Grid").gameObject;
                    int count = captian.transform.childCount;
                    for (int i = 0; i < count; i++)
                    {
                        UISprite captiansprite = captian.transform.GetChild(i).FindChild("Sprite").GetComponent<UISprite>();
                        if (captiansprite.atlas == null)
                        {
                            captiansprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
                            captiansprite.spriteName = sprite.spriteName;
                            //captiansprite.MakePixelPerfect();
                        }
                    }
                }
                else
                {
                    GameObject heroobj = GameObject.Find("Bottom/Herobag/Grid").gameObject;
                    int count = heroobj.transform.childCount;
                    for (int i = 0; i < count; i++)
                    {
                        UISprite herosprite = heroobj.transform.GetChild(i).FindChild("Sprite").GetComponent<UISprite>();
                        if (herosprite.atlas == null)
                        {
                            herosprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
                            herosprite.spriteName = sprite.spriteName;
                            //herosprite.MakePixelPerfect();
                            break;
                        }
                    }
                }
            }
        }
        pressTime = Time.realtimeSinceStartup;  
    }
}

//     public void OnClick()
//     {
//         GameObject obj = this.gameObject;
//         if (obj == null)
//         {
//             return;
//         }
// 
//         UISprite sprite = obj.GetComponent<UISprite>();
//         if (sprite.atlas)
//         {
//             foreach (var element in ArrangementPanel.spritename)
//             {
//                 if (sprite.spriteName == element.Value)
//                 {
//                     PlayerData.GetInstance().CurrentFightHero.Remove(element.Key);
//                     break;
//                 }
//             }
//             sprite.atlas = null;
//             sprite.MakePixelPerfect();
// 
//             if (sprite.spriteName == "Headpic0008")
//             {
//                 GameObject captian = GameObject.Find("Bottom/Captainbag/Grid").gameObject;
//                 int count = captian.transform.childCount;
//                 for (int i = 0; i < count; i++)
//                 {
//                     UISprite captiansprite = captian.transform.GetChild(i).FindChild("Sprite").GetComponent<UISprite>();
//                     if (captiansprite.atlas == null)
//                     {
//                         captiansprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
//                         captiansprite.spriteName = sprite.spriteName;
//                         captiansprite.MakePixelPerfect();
//                     }
//                 }
//             }
//             else
//             {
//                 GameObject heroobj = GameObject.Find("Bottom/Herobag/Grid").gameObject;
//                 int count = heroobj.transform.childCount;
//                 for (int i = 0; i < count; i++)
//                 {
//                    UISprite herosprite =  heroobj.transform.GetChild(i).FindChild("Sprite").GetComponent<UISprite>();
//                    if (herosprite.atlas == null)
//                    {
//                        herosprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
//                        herosprite.spriteName = sprite.spriteName;
//                        herosprite.MakePixelPerfect();
//                        break;
//                    }
//                 }
//             }
//         }
//    }
