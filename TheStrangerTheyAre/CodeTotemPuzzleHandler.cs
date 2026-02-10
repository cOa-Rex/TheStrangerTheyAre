using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class CodeTotemPuzzleHandler : MonoBehaviour
    {
        [SerializeField]
        private EclipseCodeController4[] codeControllers = new EclipseCodeController4[2];
        [SerializeField]
        private OWAudioSource oneShot;
        [SerializeField]
        private DitheringAnimator ditherAnim;
        private bool isFirstCodeSolved;
        private bool isSecondCodeSolved;
        public bool areAllCodesMatched;
        private bool isActive;

        public void Start()
        {
            // define booleans
            areAllCodesMatched = false;
            isFirstCodeSolved = false;
            isSecondCodeSolved = false;
            isActive = false;

            // setup events
            codeControllers[0].OnOpen += OnFirstCodeSolved;
            codeControllers[1].OnOpen += OnSecondCodeSolved;
            codeControllers[0].OnClose += OnFirstCodeUnsolved;
            codeControllers[1].OnClose += OnSecondCodeUnsolved;
        }

        public void OnFirstCodeSolved()
        {
            isFirstCodeSolved = true;
        }

        public void OnSecondCodeSolved()
        {
            isSecondCodeSolved = true;
        }

        public void OnFirstCodeUnsolved()
        {
            isFirstCodeSolved = false;
        }

        public void OnSecondCodeUnsolved()
        {
            isSecondCodeSolved = false;
        }

        private bool AreBothSolved()
        {
            if (isFirstCodeSolved && isSecondCodeSolved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update()
        {
            if (AreBothSolved() && !isActive)
            {
                Locator.GetShipLogManager().RevealFact("PREBRAMBLE_ARCHIVES_CODE_E2");
                oneShot.PlayOneShot(global::AudioType.VisionTorch_EnterVision, 1f);
                ditherAnim.SetVisible(false, 1f);
                areAllCodesMatched = true;
            } else if (!AreBothSolved() && isActive) {
                oneShot.PlayOneShot(global::AudioType.VisionTorch_ExitVision, 1f);
                ditherAnim.SetVisible(true, 1f);
                areAllCodesMatched = true;
            }
        }
    }
}
