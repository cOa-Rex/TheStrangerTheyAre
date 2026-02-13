using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre;

public class SolanumDialogueFix : MonoBehaviour
{
    private GameObject _solanumDialogue;
    private GameObject _solanumTorch;
    private GameObject _baseDialogue;
    private bool _hasDestroyed;

    public void Start()
    {
        _hasDestroyed = false;
        _solanumDialogue = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/TSTA_Solanum");
        _solanumTorch = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/TSTA_SolanumTorch");
        _baseDialogue = GameObject.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/ConversationZone"); // gets the base game dialogue of solanum
    }

    public void Update()
    {
        if (DialogueConditionManager.SharedInstance.GetConditionState("TSTA_SOL_SCANNED") == true)
        {
            _solanumDialogue.SetActive(true);
            if (!_hasDestroyed)
            {
                Destroy(_baseDialogue);
                Destroy(_solanumTorch);
                _hasDestroyed = true;
            }
        } else
        {
            _solanumDialogue.SetActive(false);
        }
    }
}
