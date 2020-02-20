﻿using System;
using System.Collections.Generic;
using DG.Tweening;
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
        private Sprite[] balloonSprites;

        [SerializeField]
        private InteractableObject serveButton;

        private float clientDelay = 1f;
        private float clientInterval = 2f;
        private int destroyedBalloons;
        private int clientsServed;

        private int clientAmount;
        private Action onPuzzleComplete;

        public void StartPuzzle (int puzzleIndex, Action _onPuzzleComplete) {

            clientAmount = GetClientAmountPerPuzzle (puzzleIndex);
            onPuzzleComplete = _onPuzzleComplete;

            clientInterval = clientAmount == 4 ? 1f : clientInterval;
            for (int i = 0; i < clientAmount; i++) {

                int index = i;
                int bSpriteSort = i >= 2 ? 0 : i;
                int balloonSpritesIndex = clientAmount <= 3 ? bSpriteSort : 2;
                balloonControllers[i].OnMouseClick += () => DestroyBalloon (index);
                this.RunDelayed (i * clientInterval + clientDelay, () => balloonControllers[index].ShowBalloon (ShowType.Fade, 0.75f,
                    Ease.InOutSine, null, balloonSprites[balloonSpritesIndex]));
                this.RunDelayed (i * clientInterval + clientDelay, clientControllers[i].StartClient);
            }
            serveButton.SetInteractable (false);
            serveButton.OnMouseClick += ServeClient;
        }

        private void DestroyBalloon (int index) {

            serveButton.SetInteractable (true);
            destroyedBalloons += 1;
            Destroy (balloonControllers[index].gameObject);
        }

        private void ServeClient () {

            clientControllers[clientsServed].ServeClient ();
            clientsServed += 1;
            if (destroyedBalloons <= clientsServed)
                serveButton.SetInteractable (false);

            if (clientsServed >= clientAmount)
                onPuzzleComplete?.Invoke ();
        }

        private int GetClientAmountPerPuzzle (int coffeeMainPuzzleIndex) {

            if (coffeeMainPuzzleIndex == 3 || coffeeMainPuzzleIndex == 4)
                return 4;
            else if (coffeeMainPuzzleIndex == 1)
                return 3;
            else
                return 1;
        }
    }
}