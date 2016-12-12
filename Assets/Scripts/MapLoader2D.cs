using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/*
=================================================
使い方：
①インスペクタから読み込みたいcsv形式のテキストを登録
②csv形式の数字に対応したブロックのゲームオブジェクトをインスペクタから登録
ex) *:空白、0:0番目のブロック、1:1番目のブロック、、、
③表示される
=================================================
*/

public class MapLoader2D : MonoBehaviour {

	//読み込むテキストアセット形式のマップ
	[SerializeField] private TextAsset textMap;
	//登録したいゲームオブジェクトを配列に代入
	[SerializeField] private GameObject[] block;
	[SerializeField] private float blockHeight = 0.5f;
	string[,] map;
	//Blockを格納するディクショナリ
	[SerializeField] private Dictionary<string, GameObject> dic = new Dictionary<string, GameObject> ();
	//行、列
	private int row,column;

	//一時変数
	int i,j;
	string str;

	//Playerを先に呼び出すため、Awakeを使う
	void Awake ()
	{
		CheckObject();
		MapLoad();
		MapCreate();
	}

	void CheckObject()
	{
		//Blockをdicに格納
		if (block.Length != 0){
			for (i=0; i<block.Length; i++){
				dic.Add (i.ToString(), block[i]);
			}
		}
	}

	//csvファイル（txtファイル)から読み込み
	void MapLoad()
	{
		//mapの大きさを調べる
		//charをそのままSplitの引数に指定するとエラーが出る...（ネットでは別の書式と認識されるからっぽい？）
		char[] separator = new char[]{'\n'};
		string[] lines = textMap.text.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
		string[] line = lines[0].Split(',');
		row = lines.Length;
		column = line.Length;
		map = new string[row,column];
		for (i=0; i<row; i++){
			line = lines[i].Split(',');
			for(j=0; j<column; j++){
				map[i,j] = line[j];
			}
		}
	}

	//読み込んだcsvファイル（txtファイル）からマップ作成
	void MapCreate()
	{
		for (i=0; i<row; i++) {
			for (j=0; j<column; j++) {
				if (map[i,j] == "*") continue;
				GameObject obj = (GameObject)Instantiate(dic[map[i,j]], new Vector3((float)j, blockHeight, (float)i), Quaternion.identity);
				//このオブジェクトの子として登録する
				obj.transform.parent = transform;
			}
		}
	}

	public string[,] GetMap()
	{
		return this.map;
	}

	public int GetRow()
	{
		return this.row;
	}

	public int GetColumn()
	{
		return this.column;
	}
}
