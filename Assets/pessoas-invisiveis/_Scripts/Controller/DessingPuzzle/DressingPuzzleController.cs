﻿using System;
using PeixeAbissal.Controller.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Controller.DessingPuzzle {

    public class DressingPuzzleController : MonoBehaviour {

        [SerializeField]
        private DressingClothController[] clothes;
        [SerializeField]
        private GameObject capReflex, apronReflex;
        [SerializeField]
        private DressingClairController dressingClairController;

        private int resolvedClothes;
        private const int MAX_CLOTHES = 2;

        public void InitializePuzzle (Action onPuzzleResolve) {

            for (int i = 0; i < clothes.Length; i++) {

                clothes[i].InitializeCloth (() => {

                    resolvedClothes += 1;
                    if (resolvedClothes >= MAX_CLOTHES) {

                        onPuzzleResolve ();
                    }
                }, (dressingState) => {

                    if (dressingState == DressingState.APRON_ONLY) apronReflex.SetActive (true);
                    else if (dressingState == DressingState.CAP_ONLY) capReflex.SetActive (true);
                    dressingClairController.SetClairClothes (dressingState);
                });
            }
        }

        public void HideClothes (DressingState dressingState) {

            for (int i = 0; i < clothes.Length; i++) {

                clothes[i].gameObject.SetActive (false);
            }

            dressingClairController.SetClairClothes (dressingState);
        }
    }
}