using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

//Serialization make classes visible in inspector view

[System.Serializable]

//check this here
public class Player {
    public Image panel;
    public Text text;
    public Button button;
    public Image thumbnail;

}
[System.Serializable]
public class PlayerColor {
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour {
    public Text[] buttonList;
    private string selectedTool;
    //private string playerSide;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;

    public Player playerX;
    public Player blackCircleLegendTile;
    public Player whiteCircleLegendTile;
    public Player blackSquareLegendTile;
    public Player whiteSquareLegendTile;
    public Player blackTriangleLegendTile;
    public Player whiteTriangleLegendTile;
    public Player removeTool;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    public GameObject startInfo;
    public Sprite[] boardToolSprite;
    public Sprite toolSprite;
    //public Player[] legendTiles;
    //public float speed = 0.4f;
    public float timer;
    public Button[] gridSpaces;
    public Image[] restingStates;
    public Button StartTrialBtn;
    public Button ResetTrialBtn;
	public Button NextTrialBtn;
    public Button StartFirstTrialBtn;
    public Button BlackCircleLegendBtn;
    public Button WhiteCircleLegendBtn;
    public Button BlackSquareLegendBtn;
    public Button WhiteSquareLegendBtn;
    public Button BlackTriangleLegendBtn;
    public Button WhiteTriangleLgendBtn;
	public float playerSpeed;

    public int selection;
    public int n;
    List<int> xTiles_level1 = new List<int>() { 0, 1, 2, 5, 6, 7, 8, 16, 24, 32, 40, 48, 56, 57, 58, 60, 62, 63, 55, 47, 39, 31, 23, 15, 7,64,65,66,67};
    List<int> checkTiles_level1 = new List<int>() { 59, 61 };
	List<int> xTiles_level2 = new List<int>() { 0, 1, 2, 5, 6, 7, 8, 16, 24, 32, 40, 48,57, 58, 60, 62, 55, 47, 39, 31, 23, 15, 7,64,65,66,67, 59,61};
	List<int> checkTiles_level2 = new List<int>() { 56, 63 };
	List<int> xTiles_level3 = new List<int>() { 0, 1, 2, 5, 6, 7, 8, 16, 32, 40, 48,57, 58, 60, 62, 55, 47, 39, 31, 23, 15, 7,64,65,66,67, 59,61, 18, 26, 34,56,63};
	List<int> checkTiles_level3 = new List<int>() {24};
	List<int> xTiles_level4 = new List<int>() { 0, 1, 2, 5, 6, 7, 8, 16, 24, 32, 40, 48,57, 58, 60, 55, 47, 39, 31, 23, 15, 7,64,65,66,67, 59,61, 56, 63, 25, 19, 14, 20, 29,30, 34,35,43,46,53};
	List<int> checkTiles_level4 = new List<int>() { 17,62 };
	List<int> xTiles_level5 = new List<int>() { 0, 1, 2, 5, 6, 7, 8, 24, 32, 40, 48,57, 58, 60, 55, 47, 39, 31, 23, 15, 7,64,65,66,67, 59,61,56, 63, 14, 19, 20, 25, 26, 29, 30, 34, 35,43, 46, 53};
	List<int> checkTiles_level5 = new List<int>() { 16,62 };
	//List<string> tags_for_centering = new List<string>() {"BlackCircleTool”,“WhiteCircleTool”,“WhiteTriangleTool”,“BlackTriangleTool","WhiteSquareTool","XTile”,"CheckTile”,"EmptyGridSpace"};
	//public List<string> tags_for_centering = new List<string>() {"BlackCircleTool", "WhiteCircleTool", "WhiteTriangleTool", 
		//"BlackTriangleTool", "WhiteSquareTool", "XTile", "CheckTile", "BlackSquareTool"};
	public List<string> blackList = new List <string>(){"blackball", "whiteball", "EmptyGridSpace" };
	//public List<string> tags = new List<string>(){}


    public MovementScript movementScript_bb1;
    public MovementScript movementScript_bb2;
    public MovementScript movementScript_wb1;
    public MovementScript movementScript_wb2;
	//public MovementScript ms;
	//public LoadNextLevel lnl;
    public GameObject BB1;
    public GameObject BB2;
    public GameObject WB1;
    public GameObject WB2;
	public Player MessageBox;
	public GameObject MessageBoxPanel;
	public Text StartFirstTrialMsg;
	public Text SuccessfulAttemptMsg;
	public Text FailedAttemptMsg;
	public Text TooManyStepsMsg;
	public Text TimeAllottedExpiredMsg;
	public Text XGridSpaceMsg;
	public Text CheckGridSpaceMsg;
	public Text CompletedExerciseMsg;
	//public Text StartNextTrialMsg;
	public Text timerText;
	public int winLevelCondition;
	public int level;
	public bool GameStateOn;
	public bool finished;
	public bool waitingforyoutoplacefirsttool;
	private float startTime;
	Vector2 initialPosition_BB1;
	Vector2 initialPosition_BB2;
	Vector2 initialPosition_WB1;
	Vector2 initialPosition_WB2;
    Vector2 initialPlayerPosition;


    //public MessageBoxButtons;
      
    //public GameObject bla;

    private void Awake()
    {
        DisableAllThumbnails();
        DisableLegendToolSelectionButtons();
		winLevelCondition = 0;
        DisableResetTrialBtn();
        DisableStartTrialBtn();
        EnableStartFirstTrialBtn();
		DisableStartNextTrialBtn ();
		initialPosition_BB1 = BB1.gameObject.transform.position;
		initialPosition_BB2 = BB2.gameObject.transform.position;
		initialPosition_WB1 = WB1.gameObject.transform.position;
		initialPosition_WB2 = WB2.gameObject.transform.position;
		level = 1;
		SetUpBoard ();
		MakeAllTextInvisible ();
		DisplayNotification_StartFirstTrial ();
		GameStateOn = false;
		finished = true;
		playerSpeed = 30f;
    }
	public void CheckWinCondition(){
		winLevelCondition += 1;
        print("-------------------------");
		print ("Win Condition: " + winLevelCondition);
        MakeAllTextInvisible();
		if (winLevelCondition == 4) {

			if (level == 5) {
				DisplayNotification_CompletedExercise ();

			} else {
				level += 1;
				DisplayNotification_SuccessfulAttempt();
			}
			DisableStartFirstTrialBtn ();
			DisableStartTrialBtn ();
		}
	}

	public void MakeAllTextInvisible(){
		StartFirstTrialMsg.gameObject.SetActive (false);
		SuccessfulAttemptMsg.gameObject.SetActive (false);
		FailedAttemptMsg.gameObject.SetActive (false);
		TooManyStepsMsg.gameObject.SetActive (false);
		TimeAllottedExpiredMsg.gameObject.SetActive (false);
		XGridSpaceMsg.gameObject.SetActive (false);
		CheckGridSpaceMsg.gameObject.SetActive (false);
		CompletedExerciseMsg.gameObject.SetActive (false);
	}
	public void MakeAllTextInvisible_whenFinished(){
		StartFirstTrialMsg.gameObject.SetActive (false);
		SuccessfulAttemptMsg.gameObject.SetActive (false);
		FailedAttemptMsg.gameObject.SetActive (false);
		TooManyStepsMsg.gameObject.SetActive (false);
		//TimeAllottedExpiredMsg.gameObject.SetActive (false);
		XGridSpaceMsg.gameObject.SetActive (false);
		CheckGridSpaceMsg.gameObject.SetActive (false);
		CompletedExerciseMsg.gameObject.SetActive (false);
	}

    public void StartFirstTrial(){
        
        DisableStartFirstTrialBtn();
        EnableResetTrialBtn();
        EnableStartTrialBtn();
        EnableAllThumbnails();
        EnableLegendToolSelectionButtons();
		//MovePlayersToStartingPosition ();
		HideUserNotificationPanel ();
		startTime = Time.time;
		finished = false;


    }
    public void ResetTrial() {
		GameStateOn = false;
		SetUpBoard ();
        winLevelCondition = 0;
		MovePlayersToStartingPosition();
        movementScript_bb1.keepMoving = false;
        movementScript_bb2.keepMoving = false;
        movementScript_wb1.keepMoving = false;
        movementScript_wb2.keepMoving = false;
		//ms.keepMoving = false;

		movementScript_bb1.turn = 0;
		movementScript_bb2.turn = 0;
		movementScript_wb1.turn = 0;
		movementScript_wb2.turn = 0;
		//ms.turn = 0;


		//refresh vectors
//		movementScript_bb1.destination = Vector2.zero;
//		movementScript_bb2.destination = Vector2.zero;
//		movementScript_wb1.destination = Vector2.zero;
//		movementScript_wb2.destination = Vector2.zero;
		movementScript_bb1.rb.velocity = Vector2.zero;
		movementScript_bb2.rb.velocity = Vector2.zero;
		movementScript_wb1.rb.velocity = Vector2.zero;
		movementScript_wb2.rb.velocity = Vector2.zero;
		//ms.rb.velocity = Vector2.zero;
	

        EnableAllThumbnails();
        EnableLegendToolSelectionButtons();
        
        EnableStartTrialBtn();

    }
//	public void NextTrial(){
//		//load new level
//		//lnl.Load();
//		//SetUpBoardLevel2();
//		LoadNextLevel(level);
//	}
	public void SetUpBoard(){
		//set up board 
		if (level == 1) {
			SetUpBoardLevel1 ();
		} else if (level == 2) {
			SetUpBoardLevel2 ();
		} else if (level == 3) {
			SetUpBoardLevel3 ();
		} else if (level == 4) {
			SetUpBoardLevel4 ();
		} else if (level == 5) {
			SetUpBoardLevel5();
		}
	}
	public void LoadNextLevel(){
        winLevelCondition = 0;
		MovePlayersToStartingPosition ();
		SetUpBoard();

		//enable and diable appropriate buttons
		//restore game to awake state
		movementScript_bb1.turn = 0;
		movementScript_bb2.turn = 0;
		movementScript_wb1.turn = 0;
		movementScript_wb2.turn = 0;
		//ms.turn = 0;

		EnableAllThumbnails();
		EnableLegendToolSelectionButtons();
		winLevelCondition = 0;
		EnableResetTrialBtn();
		EnableStartTrialBtn();
		DisableStartNextTrialBtn ();


		//EnableStartFirstTrialBtn();
		//EnableStartNextTrialBtn ();
		//SetUpBoardLevel1();
		//MakeAllTextInvisible ();
		//DisplayNotification_StartFirstTrial ();
		GameStateOn = false;

	}

    public void StartTrial() {
		GameStateOn = true;
        //print("StartingTrial");

		//Move ();
		//movementScript_bb1.Move();
		//refresh vector

		movementScript_bb1.destination = transform.position;
		movementScript_bb2.destination = transform.position;
		movementScript_wb1.destination = transform.position;
		movementScript_wb2.destination = transform.position;
		//ms.destination = transform.position;

		movementScript_bb1.direction = "down";
		movementScript_bb2.direction = "down";
		movementScript_wb1.direction = "down";
		movementScript_wb2.direction = "down";
		//ms.direction = "down";

		movementScript_bb1.keepMoving = true;
		movementScript_bb2.keepMoving = true;
		movementScript_wb1.keepMoving = true;
		movementScript_wb2.keepMoving = true;
		//ms.keepMoving = true;



        DisableStartTrialBtn();
        DisableAllThumbnails();
        DisableLegendToolSelectionButtons();

//		for (int i = 0; (i < 68); i++)
//		{
//			if (!(xTiles.Contains(i)) && !(checkTiles.Contains(i))){
//				gridSpaces [i].gameObject.SetActive (false);
//			}
//		}

    }
    public void MovePlayersToStartingPosition()
    {
        movementScript_bb1.rb.transform.position = initialPosition_BB1;
        movementScript_wb2.rb.transform.position = initialPosition_WB2;
        movementScript_wb1.rb.transform.position = initialPosition_WB1;
        movementScript_bb2.rb.transform.position = initialPosition_BB2;

    }
 
    public void DisableResetTrialBtn() {
        ResetTrialBtn.gameObject.SetActive(false);
    }
    public void EnableResetTrialBtn()
    {
        ResetTrialBtn.gameObject.SetActive(true);
    }
    public void DisableStartTrialBtn() {
        StartTrialBtn.gameObject.SetActive(false);
    }
    public void EnableStartTrialBtn()
    {
        StartTrialBtn.gameObject.SetActive(true);
    }
    public void DisableStartFirstTrialBtn() {
        StartFirstTrialBtn.gameObject.SetActive(false);
    }
    public void EnableStartFirstTrialBtn()
    {
        StartFirstTrialBtn.gameObject.SetActive(true);
    }
	public void DisableStartNextTrialBtn() {
		NextTrialBtn.gameObject.SetActive(false);
	}
	public void EnableStartNextTrialBtn()
	{
		NextTrialBtn.gameObject.SetActive(true);
	}

    public void SetUpBoardLevel1(){
        for (int i = 0; (i < 68); i++)
        {

            if (xTiles_level1.Contains(i))
            {
                //print("making it to x tile changes!!");
                gridSpaces[i].image.sprite = boardToolSprite[9];
                gridSpaces[i].tag = "XTile";
            }
            else if (checkTiles_level1.Contains(i))
            {
                gridSpaces[i].image.sprite = boardToolSprite[8];
                gridSpaces[i].tag = "CheckTile";
            }
            else
            {
                gridSpaces[i].image.sprite = boardToolSprite[10];
				gridSpaces [i].tag = "EmptyGridSpace";
            }
            //print("tag" + i + gridSpaces[i].tag);
        }

    }
	public void SetUpBoardLevel2(){
		for (int i = 0; (i < 68); i++)
		{

			if (xTiles_level2.Contains(i))
			{
				//print("making it to x tile changes!!");
				gridSpaces[i].image.sprite = boardToolSprite[9];
				gridSpaces[i].tag = "XTile";
			}
			else if (checkTiles_level2.Contains(i))
			{
				gridSpaces[i].image.sprite = boardToolSprite[8];
				gridSpaces[i].tag = "CheckTile";
			}
			else
			{
				gridSpaces[i].image.sprite = boardToolSprite[10];
				gridSpaces [i].tag = "EmptyGridSpace";
			}
			//print("tag" + i + gridSpaces[i].tag);
		}

	}
	public void SetUpBoardLevel3(){
		for (int i = 0; (i < 68); i++)
		{

			if (xTiles_level3.Contains(i))
			{
				//print("making it to x tile changes!!");
				gridSpaces[i].image.sprite = boardToolSprite[9];
				gridSpaces[i].tag = "XTile";
			}
			else if (checkTiles_level3.Contains(i))
			{
				gridSpaces[i].image.sprite = boardToolSprite[8];
				gridSpaces[i].tag = "CheckTile";
			}
			else
			{
				gridSpaces[i].image.sprite = boardToolSprite[10];
				gridSpaces [i].tag = "EmptyGridSpace";
			}
			//print("tag" + i + gridSpaces[i].tag);
		}

	}
	public void SetUpBoardLevel4(){
		for (int i = 0; (i < 68); i++)
		{

			if (xTiles_level4.Contains(i))
			{
				//print("making it to x tile changes!!");
				gridSpaces[i].image.sprite = boardToolSprite[9];
				gridSpaces[i].tag = "XTile";
			}
			else if (checkTiles_level4.Contains(i))
			{
				gridSpaces[i].image.sprite = boardToolSprite[8];
				gridSpaces[i].tag = "CheckTile";
			}
			else
			{
				gridSpaces[i].image.sprite = boardToolSprite[10];
				gridSpaces [i].tag = "EmptyGridSpace";
			}
			//print("tag" + i + gridSpaces[i].tag);
		}

	}
	public void SetUpBoardLevel5(){
		for (int i = 0; (i < 68); i++)
		{

			if (xTiles_level5.Contains(i))
			{
				//print("making it to x tile changes!!");
				gridSpaces[i].image.sprite = boardToolSprite[9];
				gridSpaces[i].tag = "XTile";
			}
			else if (checkTiles_level5.Contains(i))
			{
				gridSpaces[i].image.sprite = boardToolSprite[8];
				gridSpaces[i].tag = "CheckTile";
			}
			else
			{
				gridSpaces[i].image.sprite = boardToolSprite[10];
				gridSpaces [i].tag = "EmptyGridSpace";
			}
			//print("tag" + i + gridSpaces[i].tag);
		}

	}
    public void SelectBlackCircle() {
        DisableAllThumbnails();
        //blackCircleLegendTile.text.enabled = true;
        blackCircleLegendTile.thumbnail.enabled = true;
        toolSprite = boardToolSprite[0];
        selection = 0;

    }
    public void SelectWhiteCircle()
    {
        DisableAllThumbnails();
        //whiteCircleLegendTile.text.enabled = true;
        whiteCircleLegendTile.thumbnail.enabled = true;
        toolSprite = boardToolSprite[1];
        selection = 1;

    }
    public void SelectBlackTriangle()
    {
        DisableAllThumbnails();
        //blackTriangleLegendTile.text.enabled = true;
        blackTriangleLegendTile.thumbnail.enabled = true;
        toolSprite = boardToolSprite[2];
        selection = 2;

    }
    public void SelectWhiteTriangle()
    {
        DisableAllThumbnails();
        //whiteTriangleLegendTile.text.enabled = true;
        whiteTriangleLegendTile.thumbnail.enabled = true;
        toolSprite = boardToolSprite[3];
        selection = 3;

    }
    public void SelectBlackSquare()
    {
        DisableAllThumbnails();
        //blackSquareLegendTile.text.enabled = true;
        blackSquareLegendTile.thumbnail.enabled = true;
        toolSprite = boardToolSprite[4];
        selection = 4;

    }
    public void SelectWhiteSquare()
    {
        DisableAllThumbnails();
        //whiteSquareLegendTile.text.enabled = true;
        whiteSquareLegendTile.thumbnail.enabled = true;
        toolSprite = boardToolSprite[5];
        selection = 5;
    }
    public void RemoveTileTool()
    {
        DisableAllThumbnails();
        //whiteSquareLegendTile.text.enabled = true;
        removeTool.thumbnail.enabled = true;
        toolSprite = boardToolSprite[6];
        selection = 6;

    }
    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {

            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);

        }
    }
    public string GetTool()
    {
        return selectedTool;
    }
    public Sprite GetToolSprite()
    {
        return toolSprite;
    }
    public int GetSelection(){
        return selection;
    }
    void SetTool(string tool)
    {
        selectedTool = tool;
        
    }
    void DisableAllThumbnails() {
        
        blackCircleLegendTile.thumbnail.enabled = false;
        whiteCircleLegendTile.thumbnail.enabled = false;
        blackSquareLegendTile.thumbnail.enabled = false;
        whiteSquareLegendTile.thumbnail.enabled = false;
        blackTriangleLegendTile.thumbnail.enabled = false;
        whiteTriangleLegendTile.thumbnail.enabled = false;
        removeTool.thumbnail.enabled = false;
    }
    void EnableAllThumbnails()
    {

        blackCircleLegendTile.thumbnail.enabled = true;
        whiteCircleLegendTile.thumbnail.enabled = true;
        blackSquareLegendTile.thumbnail.enabled = true;
        whiteSquareLegendTile.thumbnail.enabled = true;
        blackTriangleLegendTile.thumbnail.enabled = true;
        whiteTriangleLegendTile.thumbnail.enabled = true;
        removeTool.thumbnail.enabled = true;
    }
    void DisableLegendToolSelectionButtons()
    {

        blackCircleLegendTile.button.enabled = false;
        whiteCircleLegendTile.button.enabled = false;
        blackSquareLegendTile.button.enabled = false;
        whiteSquareLegendTile.button.enabled = false;
        blackTriangleLegendTile.button.enabled = false;
        whiteTriangleLegendTile.button.enabled = false;
		//print ("legend selection disable.");
        
    }
    void EnableLegendToolSelectionButtons()
    {
		//print ("legend selection enable.");
        blackCircleLegendTile.button.enabled = true;
        whiteCircleLegendTile.button.enabled = true;
        blackSquareLegendTile.button.enabled = true;
        whiteSquareLegendTile.button.enabled = true;
        blackTriangleLegendTile.button.enabled = true;
        whiteTriangleLegendTile.button.enabled = true;
    }
	public void DisplayNotification_StartFirstTrial(){
		MessageBoxPanel.SetActive (true);
        MakeAllTextInvisible();
		StartFirstTrialMsg.gameObject.SetActive (true);
	}
	public void DisplayNotification_SuccessfulAttempt(){
		MessageBoxPanel.SetActive (true);
        MakeAllTextInvisible();
		SuccessfulAttemptMsg.gameObject.SetActive (true);
	}
	public void DisplayNotification_FailedAttempt(){
		MessageBoxPanel.SetActive (true);
        MakeAllTextInvisible();
		FailedAttemptMsg.gameObject.SetActive (true);
		movementScript_bb1.keepMoving = false;
		movementScript_bb2.keepMoving = false;
		movementScript_wb1.keepMoving = false;
		movementScript_wb2.keepMoving = false;
		//ms.keepMoving = false;
		//print ("Failed attempt.");
	}
	public void DisplayNotification_TooManySteps(){
		MessageBoxPanel.SetActive (true);
        MakeAllTextInvisible();
		TooManyStepsMsg.gameObject.SetActive (true);
		movementScript_bb1.keepMoving = false;
		movementScript_bb2.keepMoving = false;
		movementScript_wb1.keepMoving = false;
		movementScript_wb2.keepMoving = false;
		//ms.keepMoving = false;

	}
	public void DisplayNotification_TimeAllottedExpired(){
		MessageBoxPanel.SetActive (true);
        MakeAllTextInvisible();
		TimeAllottedExpiredMsg.gameObject.SetActive (true);
	}
	public void DisplayNotification_XGridSpace(){
		MessageBoxPanel.SetActive (true);
        MakeAllTextInvisible();
		XGridSpaceMsg.gameObject.SetActive (true);
	}
	public void DisplayNotification_CheckGridSpace(){
		MessageBoxPanel.SetActive (true);
        MakeAllTextInvisible();
		CheckGridSpaceMsg.gameObject.SetActive (true);
	}
	public void DisplayNotification_CompletedExercise(){
		MessageBoxPanel.SetActive (true);
        MakeAllTextInvisible();
		CompletedExerciseMsg.gameObject.SetActive (true);
		//finished = true;
	}
	public void HideUserNotificationPanel(){
		MakeAllTextInvisible ();
        MessageBoxPanel.SetActive(false);
		if (level >= 2) {
            if (winLevelCondition == 4){
                EnableStartNextTrialBtn();
            }
            else{
                EnableResetTrialBtn();
            }
			//LoadNextLevel();
			//DisableStartNextTrialBtn ();
		}
	}
	public void finish(){
		timerText.color = Color.yellow;
        MakeAllTextInvisible();
		DisplayNotification_TimeAllottedExpired ();
		MakeAllTextInvisible_whenFinished ();
	}




    void Update()
    {
		if (finished) {
			return;
		}

		float t = Time.time - startTime;
		string minutes = ((int) t / 60).ToString();
		string seconds = (t % 60).ToString ("f2");
		timerText.text = minutes + " : " + seconds; 

		if ( ((int) t/60) >= 12f) {
			finished = true;
			finish ();
			//MakeAllTextInvisible_whenFinished ();

		}

		//print (timer);
//        if (timer < 0f)
//        {
//            n += 8;
//
//            //FIXME: TRIGGERS TILE BY TILE MOVEMENT ----> CHANGING TO VECTOR BASED MOVEMENT
//          
//            timer = 100f;
//
//        }
//        timer -= 5f;
	


    }




}
