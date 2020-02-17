using System;
using PeixeAbissal.Controller.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Controller.DessingPuzzle {

    public class DressingClothController : MonoBehaviour {

        public InteractableObject clothInteractable;
        public Transform clothDestination;
        public DressingState dressingStateToSet;

        public void InitializeCloth (Action onClothResolve, Action<DressingState> dressingState) {

            clothInteractable.followMouseOnClick = true;
            clothInteractable.OnMouseUp += () => {

                if (DragHelper.CheckPosition (clothInteractable, clothDestination, onClothResolve)) {

                    clothInteractable.gameObject.SetActive (false);
                    dressingState (dressingStateToSet);
                }
            };
        }
    }
}