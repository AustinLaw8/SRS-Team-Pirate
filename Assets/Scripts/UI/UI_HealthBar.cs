using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    public static UI_HealthBar instance {get; private set;}
    public Image mask;
    float originialSize;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        originialSize = mask.rectTransform.rect.width;   
    }

    public void SetValue(float val)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originialSize * val);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
