using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public enum Content
    {
        Hp,
        Exp
    }

    public Content content;
    private Slider slider;
    private Param param;

    void Start ()
    {
        slider = GetComponent<Slider>();
        param = GameObject.FindWithTag("Player").GetComponent<Param>();
    }
	
	void Update ()
    {
        switch(content)
        {
            case Content.Hp:
                slider.value = 1.0f * param.hp / param.maxHp;
                break;
            case Content.Exp:
                slider.value = 1.0f * param.exp / 256.0f;
                break;
            default:
                slider.value = 0.0f;
                break;
        }
	}
}
