using System;
using System.Collections.Generic;
using PeixeAbissal.UI;
using PeixeAbissal.UI.Enum;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Controller.Coffee {

    public class CoffeeMainPuzzleController : MonoBehaviour {

        [SerializeField]
        private List<ClientController> clientControllers = new List<ClientController> ();
        [SerializeField]
        private List<BalloonController> balloonControllers = new List<BalloonController> ();

        [SerializeField]
        private InteractableObject serveButton;

        private float clientDelay = 1f;
        private float clientInterval = 2f;
        private int destroyedBalloons;
        private int clientsServed;

        private int clientAmount;
        private Action onPuzzleComplete;

        public void StartPuzzle (int _clientAmount, Action _onPuzzleComplete) {

            clientAmount = _clientAmount;
            onPuzzleComplete = _onPuzzleComplete;

            for (int i = 0; i < _clientAmount; i++) {

                int index = i;
                balloonControllers[i].OnMouseClick += () => DestroyBalloon (index);
                this.RunDelayed (i * clientInterval + clientDelay, () => balloonControllers[index].ShowBalloon (ShowType.Fade, 0.75f));
                this.RunDelayed (i * clientInterval + clientDelay, clientControllers[i].StartClient);
            }
            serveButton.SetInteractable (false);
            serveButton.OnMouseClick += ServeClient;
        }

        private void DestroyBalloon (int index) {

            serveButton.SetInteractable (true);
            destroyedBalloons += 1;
            Destroy(balloonControllers[index].gameObject);
            //balloonControllers[index].HideBalloon (ShowType.Fade, 0.15f);
        }

        private void ServeClient () {

            clientControllers[clientsServed].ServeClient ();
            clientsServed += 1;
            if (destroyedBalloons <= clientsServed)
                serveButton.SetInteractable (false);

            if (clientsServed >= clientAmount)
                onPuzzleComplete?.Invoke ();
        }
    }
}