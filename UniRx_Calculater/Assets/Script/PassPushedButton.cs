using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

public class PassPushedButton : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    [SerializeField] List<Button> operaterKeys;
    [SerializeField] List<Text> texts;


    //このクラスからOnNextで別クラスに処理を渡せるようSubjectを用意
    private Subject<string> sendCalcSubject = new Subject<string>();
    private Subject<int> sendOperaterSubject = new Subject<int>();

    //なんだっけこれ…プロパティ？外部クラスからアクセス（getのみ）できるようにしておく
    public IObservable<string> KeyClicked
    {
        get { return sendCalcSubject; }
    }

    public IObservable<int> OperaterKeyClicked
    {
        get { return sendOperaterSubject; }
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
        for (int i = 0; i < buttons.Count; i++)
        {
            ButtonCheck(i);
        }
        for(int i =0; i < operaterKeys.Count; i++)
        {
            OperatorKeyChecked(i);
        }
    }

    void ButtonCheck(int i)
    {
        buttons[i].onClick.AsObservable()
            .Subscribe(count => sendCalcSubject.OnNext(texts[i].text))
            .AddTo(gameObject);
    }

    void OperatorKeyChecked(int i)
    {
        operaterKeys[i].onClick.AsObservable()
            .Subscribe(count => sendOperaterSubject.OnNext(i))
            .AddTo(gameObject);
    }
}
