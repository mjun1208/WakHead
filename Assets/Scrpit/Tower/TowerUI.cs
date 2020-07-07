using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public TowerSystem tower;
    Slider Hpbar;
    public Text Hptext;

    // Start is called before the first frame update
    void Start()
    {
        Hpbar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Hpbar.value = tower.TowerHp;
        Hptext.text = tower.TowerHp.ToString("0") + " / 100";
        gameObject.transform.position = Camera.main.WorldToScreenPoint(
            new Vector3(tower.gameObject.transform.position.x, tower.gameObject.transform.position.y + 5 , tower.gameObject.transform.position.z));
    }
} 