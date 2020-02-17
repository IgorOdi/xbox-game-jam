using System;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Controller.DessingPuzzle {

    public class DressingPuzzleController : MonoBehaviour {

        [SerializeField]
        private DressingClothController[] clothes;
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

                    dressingClairController.SetClairClothes (dressingState);
                });
            }
        }
    }
}