using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Diagnostics;

public class PassPushedButton : MonoBehaviour
{
    [SerializeField] List<Button> NumberKeys;
    [SerializeField] List<Button> operaterKeys;
    [SerializeField] List<Text> texts;

    //このクラスからOnNextで別クラスに処理を渡せるようSubjectを用意
    private Subject<string> sendCalcSubject = new Subject<string>();
    private Subject<int> sendOperaterSubject = new Subject<int>();

    //なんだっけこれ…プロパティ？外部クラスからアクセス（getのみ）できるようにしておく
    public IObservable<string> NumberKeyClicked
    {
        get { return sendCalcSubject; }
    }

    public IObservable<int> OperaterKeyClicked
    {
        get { return sendOperaterSubject; }
    }

    void Awake()
    {
        for (int i = 0; i < NumberKeys.Count; i++)
        {
            SetMonitoringNumberKeyClicked(i);
        }
        for(int i =0; i < operaterKeys.Count; i++)
        {
            SetMonitoringOperatorKeyClicked(i);
        }
    }

    void SetMonitoringNumberKeyClicked(int i)
    {
        NumberKeys[i].onClick.AsObservable()
            .Subscribe(count => sendCalcSubject.OnNext(texts[i].text))
            .AddTo(gameObject);
    }

    void SetMonitoringOperatorKeyClicked(int i)
    {
        operaterKeys[i].onClick.AsObservable()
            .Subscribe(count => sendOperaterSubject.OnNext(i))
            .AddTo(gameObject);
    }
}
