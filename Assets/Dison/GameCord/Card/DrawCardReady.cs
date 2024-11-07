using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardReady : MonoBehaviour
{
    private Animator ani_DrawCard = null;
    public GameObject showCard;

    public void DrawCardReadyAnimation()
    {
        ani_DrawCard.SetTrigger("drawcardready");
    }

    // Start is called before the first frame update
    void Start()
    {
        ani_DrawCard = UITool.FindGameComponent<Animator>("Canvas_DrawCard");
        showCard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
