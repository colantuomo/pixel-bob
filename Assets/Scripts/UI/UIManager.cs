using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int health = 100;
    private float actualHealth = 100f;
    private PlayerCtrl playerCtrl;
    private float lifeWidth = 50f;
    private Animator lifebarAnim;

    public GameObject lifeBarContent;
    public Text lifebar;
    public Image life;
    public GameObject player;

    public Text timer;
    public bool hasFinishStage;

    public Text colletable;
    private ColletableManager colletableManager;
    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        colletableManager = GameObject.FindObjectOfType<ColletableManager>();
        lifebarAnim = lifeBarContent.GetComponent<Animator>();
        playerCtrl = player.GetComponent<PlayerCtrl>();
        health = playerCtrl.GetTotalHealth();
        SetLifeWidth();
    }

    void Update()
    {
        if (!gm.hasFinishStage)
        {
            string seconds = Mathf.Floor(Time.timeSinceLevelLoad % 60).ToString("00");
            string miliseconds = Mathf.Floor((Time.timeSinceLevelLoad * 1000) % 1000).ToString("00").Substring(0, 2);
            timer.text = seconds + ":" + miliseconds;
        }
        SetColletableValue();
        if (playerCtrl.health != actualHealth)
        {
            actualHealth = playerCtrl.health;
            SetLifebar();
        }
    }

    private void SetLifeWidth()
    {
        RectTransform rectTransform = life.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(lifeWidth, rectTransform.sizeDelta.y);
    }

    private void SetLifebar()
    {
        lifebarAnim.SetTrigger("hit");
        lifebar.text = actualHealth + " / " + health;
        RectTransform rectTransform = life.GetComponent<RectTransform>();
        lifeWidth = actualHealth / 2;
        rectTransform.sizeDelta = new Vector2(lifeWidth, rectTransform.sizeDelta.y);
    }

    private void SetColletableValue()
    {
        colletable.text = " X " + colletableManager.totalColletables;
    }
}
