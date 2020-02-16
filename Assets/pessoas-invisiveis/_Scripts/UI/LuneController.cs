using System;
using DG.Tweening;
using PeixeAbissal.Audio;
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

                    luneBallon.ShowBalloon (1f, () => {

                        clairBalloon.ShowBalloon (null, 1f);
                    }, 3f, () => {

                        clairBalloon.HideBallon (() => {

                            callback?.Invoke ();
                        }, 1f);
                    });
                });
            MusicPlayer.Instance.PlayMusic (friendsMusic);
        }
    }
}