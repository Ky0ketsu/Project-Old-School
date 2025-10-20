using UnityEngine;

public class LanguageSettingsButtons : MonoBehaviour
{
    public void ButtonChangeFR()
    {
        LanguageManager.ChangeLanguage(SystemLanguage.French);
    }

    public void ButtonChangeEN()
    {
        LanguageManager.ChangeLanguage(SystemLanguage.English);
    }
} // SCRIPT END
