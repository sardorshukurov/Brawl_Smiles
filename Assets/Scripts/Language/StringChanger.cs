using TMPro;
using UnityEngine;

//[RequireComponent(typeof(TextMeshProUGUI))]
public class StringChanger : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private int textID;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();    
    }

    public void Update()
    {
        text.text = Strings.Instance.GetString(textID);
    }
}
