using System;
using System.Collections;
using UnityEngine;

namespace PeixeAbissal.Utils {

    [RequireComponent (typeof (Animator))]
    public class AnimationUtils : MonoBehaviour {

        public void PlayAnimation (string animationName, Action callback) {

            Animator animator = GetComponent<Animator> ();
            animator.Play (animationName);
            StartCoroutine (WaitForAnimationToEnd (callback, animator));
        }

        private IEnumerator WaitForAnimationToEnd (Action callback, Animator animator) {

            var duration = animator.GetCurrentAnimatorStateInfo (0).length;
            float t = 0;
            while (t <= duration) {
                t += Time.deltaTime;
                yield return null;
            }
            callback?.Invoke ();
        }
    }
}