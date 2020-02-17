using PeixeAbissal.Controller.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Controller.DessingPuzzle {

    public class DressingClairController : MonoBehaviour {

        [SerializeField]
        private Image clairImage;
        [SerializeField]
        private Sprite noClothes, capOnly, apronOnly, fullClothes;

        private DressingState currentDressingState;

        public void SetClairClothes (DressingState dressingState) {

            if (currentDressingState == DressingState.CAP_ONLY && dressingState == DressingState.APRON_ONLY ||
                currentDressingState == DressingState.APRON_ONLY && dressingState == DressingState.CAP_ONLY) {

                currentDressingState = DressingState.FULL_CLOTHES;
            } else {

                currentDressingState = dressingState;
            }

            switch (currentDressingState) {

                case DressingState.NO_CLOTHES:
                    clairImage.sprite = noClothes;
                    break;
                case DressingState.CAP_ONLY:
                    clairImage.sprite = capOnly;
                    break;
                case DressingState.APRON_ONLY:
                    clairImage.sprite = apronOnly;
                    break;
                case DressingState.FULL_CLOTHES:
                    clairImage.sprite = fullClothes;
                    break;
            }
        }
    }
}