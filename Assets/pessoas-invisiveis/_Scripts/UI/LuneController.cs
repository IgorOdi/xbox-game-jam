using System;
using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.UI.Enum;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.UI {

    public class LuneController : MonoBehaviour {

        [SerializeField]
        private AudioClip friendsMusic;
        [SerializeField]
        private BalloonController luneBallon, clairBalloon;

        public void ShowLune (Action callback) {

            gameObject.SetActive (true);
            GetComponent<CanvasGroup> ().DOFade (1, 1.5f)
                .From (0)
                .OnComplete (() => {

                    luneBallon.ShowBalloon (ShowType.Fade, () => {

                        clairBalloon.ShowBalloon (ShowType.Fade, () => {

                            this.RunDelayed (2f, () => {

                                luneBallon.ShowBalloon (ShowType.Fade, null);
                            });
                            this.RunDelayed (3f, () => {

                                clairBalloon.ShowBalloon (ShowType.Fade, () => callback?.Invoke ());

                            });
                        });
                    });
                });
            MusicPlayer.Instance.PlayMusic (friendsMusic);
        }
    }
}