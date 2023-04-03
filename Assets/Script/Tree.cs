using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    static public int HPMax = 5;
    public int HP;
    public GameObject HPBar;

    RectTransform _canvas;
    RectTransform _ui;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.GetComponent<RectTransform>(), pos, null, out canvasPos);

        _ui.anchoredPosition = canvasPos;
    }

    public void ShowHP(bool v, Canvas canvas)
    {
        _canvas = canvas.GetComponent<RectTransform>();
        var go2 = Instantiate(HPBar, transform.position, Quaternion.identity, canvas.transform);
        go2.SetActive(true);

        var slider = go2.GetComponent<Slider>();
        HP = HPMax++;
        slider.minValue = 0;
        slider.maxValue = HP;
        slider.value = HP;

        _ui = go2.GetComponent<RectTransform>();

    }

    private void OnDisable()
    {
        _ui.gameObject.SetActive(false);
    }

    internal bool Damage(int v)
    {
        HP -= v;

        Debug.Log($"{HP}");

        _ui.gameObject.GetComponent<Slider>().value = HP;
        if(HP <= 0)
        {
            return false;
        }

        return true;
    }

    public bool IsDead()
    {
        return (HP <= 0);
    }
}
