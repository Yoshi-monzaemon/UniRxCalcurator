using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class Caculater : MonoBehaviour
{
    //メッセージのやりとり対象はInspector画面から設定する。
    public Text displayPanelText;
    [SerializeField] private PassPushedButton passPushedButton;
    private Nullable<double> calculatedValue;
    private Nullable<int> operationSymbol;
    private string displayText;

    private Subject<string> sendCalculateSubject = new Subject<string>();
    public IObservable<string> CalculateResult
    {
        get { return sendCalculateSubject; }
    }

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
        //演算記号の入力を検知
        passPushedButton.OperaterKeyClicked
            .Subscribe(operateKeyNum => Calculate(operateKeyNum))
            .AddTo(gameObject);
    }

    private void Calculate(int newOperationSymbol)
    {
        displayText = displayPanelText.text;

        //正負の入れ替えが選択されたとき
        if (newOperationSymbol == 7)
        {
            displayText = (double.Parse(displayText) * -1).ToString();
            sendCalculateSubject.OnNext(displayText);
        }
        //Clearが入力された場合
        else if (newOperationSymbol != 5)
        {
            //数値・演算記号が共に入力済みだったとき
            if (calculatedValue.HasValue && operationSymbol.HasValue)
            {
                //+
                if (operationSymbol == 0) calculatedValue += double.Parse(displayText);
                //-
                if (operationSymbol == 1) calculatedValue -= double.Parse(displayText);
                //×
                if (operationSymbol == 2) calculatedValue *= double.Parse(displayText);
                //÷
                if (operationSymbol == 3) calculatedValue /= double.Parse(displayText);
                //=
                if (operationSymbol == 4) calculatedValue = double.Parse(displayText);
                ////+-
                //if (operationSymbol == 7)
                //{
                //    calculatedValue = double.Parse(displayText);
                //    calculatedValue *= -1;
                //}
                //今回入力された演算記号を格納
                operationSymbol = newOperationSymbol;

                //AC（演算記号リセットのため、これだけ判定タイミングをずらす）
                if (operationSymbol == 6)
                {
                    calculatedValue = 0;
                    operationSymbol = null;
                }
                //showdisplayに計算結果を送信する
                Debug.Log(calculatedValue.ToString());
                sendCalculateSubject.OnNext(calculatedValue.ToString());
            }
            else
            {
                calculatedValue = double.Parse(displayText);
                operationSymbol = newOperationSymbol;
                sendCalculateSubject.OnNext(calculatedValue.ToString());
            }

        }
        //Cが入力されたとき、変数の値は書き換えずにディスプレイのみクリアする
        else sendCalculateSubject.OnNext("0");
    }
}
