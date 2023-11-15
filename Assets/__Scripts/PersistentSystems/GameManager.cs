public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        base.Awake();
        SetHandItemsAccordingToPreference();
    }

    void SetHandItemsAccordingToPreference()
    {
        HandHeldType handPreference;
        if (Systems.Instance.KatanaRight)
        {
            handPreference = HandHeldType.PistolLeftKatanaRight;
        }
        else
        {
            handPreference = HandHeldType.KatanaLeftPistolRight;
        }

        FindObjectOfType<SetPlayerPreferences>().SetHandItems(handPreference);
    }
}