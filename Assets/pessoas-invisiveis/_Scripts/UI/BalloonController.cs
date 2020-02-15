using System;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.UI {

    public class BalloonController : MonoBehaviour {

        public void ShowBalloon (Action callback, float secondsToHide, Action hideCallback) {

            gameObject.SetActive(true);
            GetComponent<AnimationUtils> ().PlayAnimation ("BallonEnter", callback);
            this.RunDelayed (secondsToHide, () => {

                HideBallon (hideCallback);
            });
        }

        public void ShowBalloon (Action callback) {

            gameObject.SetActive(true);
            GetComponent<AnimationUtils> ().PlayAnimation ("BallonEnter", callback);
        }

        public void HideBallon (Action callback) {

            GetComponent<AnimationUtils> ().PlayAnimation ("BallonExit", callback);
        }
    }
}