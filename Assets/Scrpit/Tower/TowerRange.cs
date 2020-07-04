using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    public TowerSystem towersystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((towersystem.RedTeam && !collision.GetComponent<Creature>().RedTeam)||(!towersystem.RedTeam && collision.GetComponent<Creature>().RedTeam))
        {
            
            towersystem.isAttack = true;
            towersystem.TargetObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        towersystem.isAttack = false;
        towersystem.TargetObject = null;
    }
}
