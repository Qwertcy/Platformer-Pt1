using Unity.VisualScripting;
using UnityEngine;

public class QuestionRotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Material material;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += new Vector2(0, 1.2f) * Time.deltaTime;
    }
}
