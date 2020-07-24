using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class ShowDisplay : MonoBehaviour
{
    //メッセージのやりとり対象はInspector画面から設定する。
    public Text displayPanelText;
    private int operatorFlg = 0;
    [SerializeField] private PassPushedButton passPushedButton;
    [SerializeField] private Caculater calculater;

    // Start is called before the first frame update
    void Start()

    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        //監視対象スクリプトからOnNextが飛んで来たら下記Subscribe処理を実行
        passPushedButton.KeyClicked
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
        //演算記号が入力されたら一度ディスプレイをクリアする
        if (operatorFlg == 1)
        {
            displayPanelText.text = "";
            operatorFlg = 0;
        }
        //小数点が二つ以上
        if (displayPanelText.text.Contains(".")&& value ==".")
        {

        }
        //else 
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
