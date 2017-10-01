using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	/***************************/
	/*	Constant area          */
	/***************************/
	public const byte WALL_FRONT	= 1;	//画面転換:前
	public const byte WALL_RIGHT	= 2;	//画面転換:右
	public const byte WALL_BACK		= 3;	//画面転換:後
	public const byte WALL_LEFT		= 4;	//画面転換:左
	public const byte COLOR_GREEN	= 0;	//色転換:緑
	public const byte COLOR_RED		= 1;	//色転換:赤
	public const byte COLOR_BLUE	= 2;	//色転換:青
	public const byte COLOR_WHITE	= 3;	//色転換:白

	/***************************/
	/*	Public Variable area   */
	/***************************/
	public GameObject panelWalls;						//壁全体
	public GameObject buttonHammer;						//ボタン:トンカチ
	public GameObject buttonKey;						//ボタン:鍵
	public GameObject imageHammerIcon;					//アイコン:トンカチ
	public GameObject imageKeyIcon;						//アイコン:鍵
	public GameObject buttonPig;						//ボタン:ブタの貯金箱
	public GameObject buttonMessage;					//ボタン:メッセージ
	public GameObject buttonMessageText;				//テキストメッセージ
	public GameObject[] buttonLamp = new GameObject[3];	//ボタン:金庫
	public Sprite[] buttonPicture = new Sprite[4];		//ボタンの絵
	public Sprite hammerPicture;						//トンカチの絵
	public Sprite KeyPicture;							//鍵の絵

	/****************************/
	/*	Private Variable area   */
	/****************************/
	private byte wallNo;						//画面転換変数
	private bool doesHaveHammer;			//トンカチを持っているか？
	private bool doesHaveKey;				//鍵を持っているか？
	private int[] buttonColor = new int[3];	//金庫のボタン

	// Use this for initialization
	void Start () {
		wallNo = WALL_FRONT;
		doesHaveHammer = false;
		doesHaveKey = false;

		buttonColor [0] = COLOR_GREEN;	//ボタン1の色は「緑」
		buttonColor [1] = COLOR_RED;	//ボタン2の色は「赤」
		buttonColor [2] = COLOR_BLUE;	//ボタン3の色は「青」
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//ボックスをタップ
	public void PushButtonBox(){
		if (doesHaveKey == false) {
			//鍵を持っていない
			DisplayMessage ("鍵がかかっている。");
		} else {
			//鍵を持っている
			SceneManager.LoadScene ("ClearScene");
		}
	}

	//右(>）ボタンを押した
	public void PushButtonRight(){
		wallNo++;	//方向を１つ右に
					//「左」の１つ右は「前」
		if(wallNo > WALL_LEFT){
			wallNo = WALL_FRONT;
		}
		DisplayWall (); //壁表示更新
		ClearButton();	//いらない物を消す
	}

	//左(<)ボタンを押した
	public void PushButtonLeft	(){
		wallNo--;	//方向を１つ左に
					//「前」の１つ右は「左」
		if(wallNo < WALL_FRONT){
			wallNo = WALL_LEFT;
		}
		DisplayWall (); //壁表示更新
		ClearButton();	//いらない物を消す
	}

	void ClearButton(){
		buttonHammer.SetActive (false);
		buttonKey.SetActive (false);
		buttonMessage.SetActive (false);
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

	//貯金箱をタップ
	public void PushButtonPig(){
		//トンカチを持っているか？
		if (doesHaveHammer == false) {
			//トンカチを持っていない
			DisplayMessage ("素手では割れない。");
		} else {
			//トンカチを持っている
			DisplayMessage ("貯金箱が割れて中から鍵が出てきた。");

			buttonPig.SetActive (false);
			buttonKey.SetActive (true);
			imageKeyIcon.GetComponent<Image> ().sprite = KeyPicture;

			doesHaveKey = true;
		}
	}

	//トンカチの絵をタップ
	public void PushButtonHammer(){
		buttonHammer.SetActive (false);
	}

	//鍵の絵をタップ
	public void PushButtonKey(){
		buttonKey.SetActive (false);
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
	void ChangeButtonColor(byte buttonNo){
		buttonColor [buttonNo]++;

		//「白」の時にボタンを押したら「緑」に
		if (buttonColor [buttonNo] > COLOR_WHITE) {
			buttonColor [buttonNo] = COLOR_GREEN;
		}
		//ボタンの画像を変更
		buttonLamp [buttonNo].GetComponent<Image> ().sprite =
			buttonPicture [buttonColor [buttonNo]];

		if ((buttonColor [0] == COLOR_BLUE) &&
		   (buttonColor [1] == COLOR_WHITE) &&
		   (buttonColor [2] == COLOR_RED)) {
			if (doesHaveHammer == false) {
				DisplayMessage ("金庫の中にトンカチが入っていた。");
				buttonHammer.SetActive (true);
				imageHammerIcon.GetComponent<Image> ().sprite = hammerPicture;

				doesHaveHammer = true;
			}
		}
	}
}
