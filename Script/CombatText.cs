using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hptext;
    public void OnInit(float dame)
    {
        hptext.text = dame.ToString();
        Invoke(nameof(OnDespawn), 1f);  
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
