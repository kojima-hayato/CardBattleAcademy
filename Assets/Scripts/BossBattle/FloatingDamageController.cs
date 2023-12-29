using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingDamageController : MonoBehaviour
{
    public GameObject floatingDamagePrefab;
    public GameObject parent;
    GameObject fdObj;
    TextMeshProUGUI floatingDamage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDamage(int damageValue)
    {
        Vector3 pos = parent.transform.position;
        pos.y -= 25;

        fdObj = Instantiate(floatingDamagePrefab, pos, Quaternion.identity, parent.transform);
        floatingDamage = fdObj.GetComponent<TextMeshProUGUI>();

        floatingDamage.text = damageValue.ToString();

        Invoke("HideDamage", 0.5f);
    }

    private void HideDamage()
    {
        Debug.Log("É_ÉÅÅ[ÉWÇâBÇ∑");

        Destroy(fdObj);
    }
}
