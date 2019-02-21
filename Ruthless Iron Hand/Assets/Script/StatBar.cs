using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    private Image content;
    [SerializeField]
    private Text StatValue;

    private float currentFill;

    [SerializeField]
    private float lerpSpeed;
    private int currentValue;


    public int MyMaxValue
    {
        get;

        // return MyMaxValue;

        set;

        //MyMaxValue = value;

    }

    public int MyCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
            currentFill = (float)currentValue / MyMaxValue;
            StatValue.text = currentValue + "/" + MyMaxValue;
        }

    }


    // Use this for initialization
    void Awake()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }

    }
    public void Initialize(int currentValue, int maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
