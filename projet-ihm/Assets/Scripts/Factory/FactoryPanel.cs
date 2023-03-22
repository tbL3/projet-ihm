using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FactoryPanel : MonoBehaviour
{
    public Button crossButton;
    public Button researchButton;
    public Button unityButton;
    static public GameObject unityPanel;
    static public GameObject researchPanel;
    [SerializeField] private Factory factory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        unityPanel = GameObject.Find("unityPanel");
        researchPanel = GameObject.Find("researchPanel");
        GameObject obj = GameObject.Find("crossButton");
        GameObject obj2 = GameObject.Find("unityButton");
        GameObject obj3= GameObject.Find("researchButton");
        crossButton = obj.GetComponent<Button>();
        unityButton = obj2.GetComponent<Button>();
        researchButton = obj3.GetComponent<Button>();
        crossButton.onClick.AddListener(factory.closeModal);
        unityButton.onClick.AddListener(openUnity);
        researchButton.onClick.AddListener(openResearch);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void openUnity()
    {
        unityPanel.SetActive(true);
        researchPanel.SetActive(false);
    }

    public static void openResearch()
    {     
        unityPanel.SetActive(false);
        researchPanel.SetActive(true);
    }

    //si les panel sont setActive(false) au moment ou le canvas est fermer alors à la reouveture de ce dernier une erreur va être lever
    //la fonction openBoth permet à la classe Factory de réactiver les deux panel avant de fermer le canvas
    public static void openBoth()
    {
        unityPanel.SetActive(true);
        researchPanel.SetActive(true);
    }
}
