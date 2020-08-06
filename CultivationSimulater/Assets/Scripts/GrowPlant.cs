using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Collections.ObjectModel;
using System;

public class GrowPlant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    //発芽
    public void sproutingPlant()
    {

    }

    //開花
    public void bloomingPlant()
    {

    } 

    //結実
    public void fruitingPlant()
    {

    }

    public void GrowingPlant()
    {
        Observable.Timer(TimeSpan.FromSeconds(5))
            .Subscribe(_ =>
            {
                Debug.Log("5secondsPassed");
            })
            .AddTo;
    }
}
