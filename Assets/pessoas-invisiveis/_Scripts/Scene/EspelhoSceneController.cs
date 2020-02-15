using System;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EspelhoSceneController : SceneController {

        protected override string nextLevel { get { return "CenaReflexiva"; } }
        [SerializeField]
        private InteractableObject roupa, bone;
        [SerializeField]
        private Transform roupaFinalPosition, boneFinalPosition;

        internal override void StartScene () {

            roupa.followMouseOnClick = true;
            bone.followMouseOnClick = true;

            Action resolve = () => AddPoints(0.5f, false);
            roupa.OnMouseExit += () => CheckPosition(roupa, roupaFinalPosition, resolve);
            bone.OnMouseExit += () => CheckPosition(bone, boneFinalPosition, resolve);
        }
    }
}