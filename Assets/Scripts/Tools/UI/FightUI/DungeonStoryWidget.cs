using UnityEngine;
using System.Collections;

public class DungeonStoryWidget : BaseWidget{
    public const string nameExtension = "DungeonStory";

    public UISprite headShotSprite;
    public UILabel nameLabel;
    public UILabel txtLabel;

    public WidgetEvent onClick;

    public void DisplayDialog(DungeonStoryData.Role role,string dialog)
    {
        headShotSprite.atlas = role.atlas;
        headShotSprite.spriteName = role.headShot;
        nameLabel.text = role.name;
        txtLabel.text = dialog;
        Display();
    }

    public void Display()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnClick()
    {
        if (onClick != null)
            onClick();
    }
}
