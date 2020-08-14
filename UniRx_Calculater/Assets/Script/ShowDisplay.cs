using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Diagnostics;

public class ShowDisplay : MonoBehaviour
{
    //メッセージのやりとり対象はInspector画面から設定する。
    public Text displayPanelText;
    private int operatorFlg = 0;
    [SerializeField] private PassPushedButton passPushedButton;
    [SerializeField] private Caculater calculater;

    void Awake()
    {
        //監視対象スクリプトからOnNextが飛んで来たら下記Subscribe処理を実行
        passPushedButton.NumberKeyClicked
            .Subscribe(KeyCode => ShowToDisPlay(KeyCode))
            .AddTo(gameObject);

        calculater.CalculateResult
            .Subscribe(result =>
            {
                displayPanelText.text = result;
                operatorFlg = 1;
            })
            .AddTo(gameObject);
    }

    private void ShowToDisPlay(string value)
    {
        //イコールの後に数値が入力されたとき
        if (operatorFlg == 1)
        {
            //小数点が入力されたら
            if (value == ".")
            {
                //小数点が既に存在していたら何もせずに終了
                if (displayPanelText.text.Contains(".") && value == ".") return;
                //表示中の数字に小数点をつける
                displayPanelText.text += value;
            }
            //整数が入力されたら
            else
            {
                //ディスプレイを一度クリアする
                displayPanelText.text = "";
            }
            operatorFlg = 0;
        }

        //小数点が二つ以上なら入力を無視する
        if (displayPanelText.text.Contains(".") && value == ".") return;

        //入力値が小数点でなければ先頭の0は消す
        else if (displayPanelText.text == "0" && value != ".")
        {
            displayPanelText.text = value;
        }
        else
        {
            displayPanelText.text += value;
        }
    }
}
