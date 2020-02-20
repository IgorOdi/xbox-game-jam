using PeixeAbissal.Scene.Coffee;
using UnityEngine;

public class DayController : MonoBehaviour {

    public static int day;
    public static bool metLune;

    public static void ResetSave () {

        day = 0;
        metLune = false;
        CoffeeMainSceneController.coffeeMainPuzzleIndex = 0;
        CoffeePrepareSceneController.cafeIndex = 0;
    }
}