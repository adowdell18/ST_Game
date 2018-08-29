using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

    public Button button;
    public Text buttonText;
    //public Image buttonSprite;
    public GameController gameController;



    public void SetSpace() {
        
		if ((button.gameObject.tag != "XTile") && (button.gameObject.tag != "CheckTile") && (gameController.GameStateOn == false)) {
			button.image.sprite = gameController.GetToolSprite ();
			//button.gameObject.AddComponent<Collide> ();
			TagSelectedTileWithToolType ();
		} else {
			if (button.gameObject.tag == "XTile") {
				gameController.DisplayNotification_XGridSpace();
			} else if (button.gameObject.tag == "CheckTile") {
				gameController.DisplayNotification_CheckGridSpace();
			}
		}
    }

    public void SetGameControllerReference(GameController controller) {
        gameController = controller;
    }
    public void TagSelectedTileWithToolType(){
        if (gameController.GetSelection() == 0)
        {
            button.gameObject.tag = "BlackCircleTool";
        }
        else if (gameController.GetSelection() == 1)
        {
            button.gameObject.tag = "WhiteCircleTool" ;
        }
        else if (gameController.GetSelection() == 2)
        {
            button.gameObject.tag = "BlackTriangleTool";
        }
        else if (gameController.GetSelection() == 3)
        {
            button.gameObject.tag = "WhiteTriangleTool";
        }
        else if (gameController.GetSelection() == 4)
        {
            button.gameObject.tag = "BlackSquareTool";
        }
        else if (gameController.GetSelection() == 5)
        {
            button.gameObject.tag = "WhiteSquareTool";
        }

		//print ("Tagging grid space with " + button.gameObject.tag + " tool");
    }


   

   
}
