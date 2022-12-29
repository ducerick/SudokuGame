using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] Text TimeText;
    [SerializeField] Image Icon;

    // Start is called before the first frame update
    void Start()
    {
        TimeText.text = Clock.Instance.GetCurrentTime().text;


    }

    // Update is called once per frame
    void Update()
    {
        Icon.transform.rotation = Quaternion.Lerp(Icon.transform.rotation, Icon.transform.rotation *Quaternion.Euler(0, 0, 90), Time.deltaTime);
    }
   
}
