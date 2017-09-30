using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	/***************************/
	/*	Constant area          */
	/***************************/
	public const int WALL_FRONT	= 1;		//画面転換:前
	public const int WALL_RIGHT	= 2;		//画面転換:右
	public const int WALL_BACK	= 3;		//画面転換:後
	public const int WALL_LEFT	= 4;		//画面転換:左
	public const int COLOR_GREEN	= 0;	//色転換:緑
	public const int COLOR_RED		= 1;	//色転換:赤
	public const int COLOR_BLUE		= 2;	//色転換:青
	public const int COLOR_WHITE	= 3;	//色転換:白

	/***************************/
	/*	Public Variable area   */
	/***************************/
	public GameObject panelWalls;						//壁全体
	public GameObject buttonHammer;						//ボタン:トンカチ
	public GameObject imageHammerIcon;					//アイコン:トンカチ
	public GameObject buttonMessage;					//ボタン:メッセージ
	public GameObject buttonMessageText;				//テキストメッセージ
	public GameObject[] buttonLamp = new GameObject[3];	//ボタン:金庫
	public Sprite[] buttonPicture = new Sprite[4];		//ボタンの絵
	public Sprite hammerPicture;						//トンカチの絵

	/****************************/
	/*	Private Variable area   */
	/****************************/
	private int wallNo;						//画面転換変数
	private bool doesHaveHammer;			//トンカチを持っているか？
	private int[] buttonColor = new int[3];	//金庫のボタン

	// Use this for initialization
	void Start () {
		wallNo = WALL_FRONT;

		buttonColor [0] = COLOR_GREEN;	//ボタン1の色は「緑」
		buttonColor [1] = COLOR_RED;	//ボタン2の色は「赤」
		buttonColor [2] = COLOR_BLUE;	//ボタン3の色は「青」
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//右(>）ボタンを押した
	public void PushButtonRight(){
		wallNo++;	//方向を１つ右に
					//「左」の１つ右は「前」
		if(wallNo > WALL_LEFT){
			wallNo = WALL_FRONT;
		}
		DisplayWall (); //壁表示更新
	}

	//左(<)ボタンを押した
	public void PushButtonLeft	(){
		wallNo--;	//方向を１つ左に
					//「前」の１つ右は「左」
		if(wallNo < WALL_FRONT){
			wallNo = WALL_LEFT;
		}
		DisplayWall (); //壁表示更新
	}

	//向いている方向の壁を表示
	void DisplayWall(){
		switch (wallNo) {
		case WALL_FRONT:
			panelWalls.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
			break;
		case WALL_RIGHT:
			panelWalls.transform.localPosition = new Vector3 (-1000.0f, 0.0f, 0.0f);
			break;
		case WALL_BACK:
			panelWalls.transform.localPosition = new Vector3 (-2000.0f, 0.0f, 0.0f);
			break;
		case WALL_LEFT:
			panelWalls.transform.localPosition = new Vector3 (-3000.0f, 0.0f, 0.0f);
			break;
		}
	}

	//メッセージを表示
	void DisplayMessage(string mes){
		buttonMessage.SetActive (true);
		buttonMessageText.GetComponent<Text> ().text = mes;
	}

	//メモをタップ
	public void PushButtonMemo(){
		DisplayMessage ("エッフェル塔と書いてある。");
	}

	//メッセージをタップ
	public void PushButtonMessage(){
		buttonMessage.SetActive (false);
	}

	//金庫のボタン1をタップ
	public void PushButtonLamp1(){
		ChangeButtonColor (0);
	}

	//金庫のボタン2をタップ
	public void PushButtonLamp2(){
		ChangeButtonColor (1);
	}

	//金庫のボタン3をタップ
	public void PushButtonLamp3(){
		ChangeButtonColor (2);
	}

	//金庫のボタンの色を変更
	void ChangeButtonColor(int buttonNo){
		buttonColor [buttonNo]++;
		//ここまで(P.162)
	}
}
