using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestroy : MonoBehaviour
{
    //public GameObject parent;

    /// <summary>
    /// 銷毀物件(動畫事件)
    /// </summary>
    public void DestroyObject()
    {
        Destroy(transform.gameObject);
    }
}
