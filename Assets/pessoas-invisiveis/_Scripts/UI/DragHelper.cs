using System;
using UnityEngine;

namespace PeixeAbissal.UI {

    public static class DragHelper {

        public static bool CheckPosition (InteractableObject originObject, Transform destination, bool resetIfFail = true) {

            float acceptableDistance = Vector3.Distance (originObject.originPosition, destination.position) / 3;
            if (Vector3.Distance (originObject.transform.position, destination.position) <= acceptableDistance) {

                originObject.transform.position = destination.position;
                return true;
            }
            if (resetIfFail)
                originObject.ResetPosition ();
            return false;
        }
    }
}