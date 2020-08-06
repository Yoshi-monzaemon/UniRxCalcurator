using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections.ObjectModel;

public class FieldControl : MonoBehaviour
{

    //private Subject<int> 

    // Use this for initialization
    void Start()
    {
    }

    private void Awake()
    {
        var eventTrigger = this.gameObject.AddComponent<ObservableEventTrigger>();

        //PointerDown
        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(pointerEventData =>
            {
                Debug.Log("kottidekenti");
                
            })
            .AddTo(gameObject);
    }


}