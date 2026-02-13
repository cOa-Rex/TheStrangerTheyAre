using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class CodeTotemPuzzleHandler : MonoBehaviour
    {
        [SerializeField]
        public EclipseCodeController4[] codeControllers = new EclipseCodeController4[2];
        [SerializeField]
        public OWAudioSource oneShot;
        [SerializeField]
        public DitheringAnimator ditherAnim;

        public bool allCodesMatched => _allCodesMatched;

        private bool _isFirstCodeSolved;
        private bool _isSecondCodeSolved;
        private bool _allCodesMatched;
        private bool _isActive;

        public void Start()
        {
            // define booleans
            _allCodesMatched = false;
            _isFirstCodeSolved = false;
            _isSecondCodeSolved = false;
            _isActive = false;

            // setup events
            codeControllers[0].OnOpen += OnFirstCodeSolved;
            codeControllers[1].OnOpen += OnSecondCodeSolved;
            codeControllers[0].OnClose += OnFirstCodeUnsolved;
            codeControllers[1].OnClose += OnSecondCodeUnsolved;
        }

        public void OnFirstCodeSolved()
        {
            _isFirstCodeSolved = true;
        }

        public void OnSecondCodeSolved()
        {
            _isSecondCodeSolved = true;
        }

        public void OnFirstCodeUnsolved()
        {
            _isFirstCodeSolved = false;
        }

        public void OnSecondCodeUnsolved()
        {
            _isSecondCodeSolved = false;
        }

        private bool AreBothSolved()
        {
            if (_isFirstCodeSolved && _isSecondCodeSolved)
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
            if (AreBothSolved() && !_isActive)
            {
                Locator.GetShipLogManager().RevealFact("PREBRAMBLE_ARCHIVES_CODE_E2");
                oneShot.PlayOneShot(global::AudioType.VisionTorch_EnterVision, 1f);
                ditherAnim.SetVisible(false, 1f);
                _allCodesMatched = true;
            } else if (!AreBothSolved() && _isActive) {
                oneShot.PlayOneShot(global::AudioType.VisionTorch_ExitVision, 1f);
                ditherAnim.SetVisible(true, 1f);
                _allCodesMatched = true;
            }
        }
    }
}
