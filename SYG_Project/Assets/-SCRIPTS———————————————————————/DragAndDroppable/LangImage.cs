using UnityEngine;
using UnityEngine.UI;

public class LangImage : MonoBehaviour
{
    public Sprite en, fr; //    <-- 1. Add new language strings here


    SpriteRenderer sprite => GetComponentInChildren<SpriteRenderer>();
    Image image => GetComponentInChildren<Image>();

    void OnEnable()
    {
        SetLanguage();
        EVENTS.OnLanguageChange += SetLanguage;
    }

    void OnDisable()
    {
        EVENTS.OnLanguageChange -= SetLanguage;
    }


    public void SetLanguage()
    {
        switch (LanguageManager.currentLang)
        {
            default: SetEnglishByDefault(); break;
            case SystemLanguage.French: TryReplaceSprite(fr); break;
                //case SystemLanguage.Spanish: TryReplaceSprite(es); break;
                //case SystemLanguage.Portuguese: TryReplaceSprite(pt); break;
                //case SystemLanguage.Italian: TryReplaceSprite(it); break;
                //case SystemLanguage.Greek: TryReplaceSprite(el); break;
                //case SystemLanguage.German: TryReplaceSprite(de); break;
                //case SystemLanguage.Polish: TryReplaceSprite(pl); break;
                //case SystemLanguage.Swedish: TryReplaceSprite(sv); break;
                //case SystemLanguage.Finnish: TryReplaceSprite(fi); break;
                //case SystemLanguage.Norwegian: TryReplaceSprite(no); break;
                //case SystemLanguage.Russian: TryReplaceSprite(ru); break;
                //case SystemLanguage.Arabic: TryReplaceSprite(ar); break;
                //case SystemLanguage.Hebrew: TryReplaceSprite(he); break;
                //case SystemLanguage.Japanese: TryReplaceSprite(jp); break;
                //case SystemLanguage.Chinese: TryReplaceSprite(zh); break;
                //case SystemLanguage.Korean: TryReplaceSprite(ko); break;
                //case SystemLanguage.Thai: TryReplaceSprite(th); break;
                //                  <-- 2. Add (or uncomment) new language switch here. Done!
        }
    }

    void TryReplaceSprite(Sprite desired)
    {
        if (desired!=null) ReplaceSprite(desired);
        else SetEnglishByDefault();
    }

    void SetEnglishByDefault()
    {
        ReplaceSprite(en);
    }

    void ReplaceSprite(Sprite desired)
    {
        if (desired != null)
        {
            if (sprite) sprite.sprite = desired;
            if (image) image.sprite = desired;
        }
    }

#if UNITY_EDITOR
    private void OnValidate() // update the text component when modified in the inspector
    {
        SetLanguage();
    }
#endif

} // SCRIPT END
