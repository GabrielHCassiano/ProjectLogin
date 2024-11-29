using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScroll : MonoBehaviour
{

    [Range(0f, 30f)]
    public float ScrollSpeed = 2;

    [SerializeField]
    [Min(1)]
    int maxClones = 15;


    private float textPreferredWidth;
    private readonly LinkedList<RectTransform> textTransforms = new();

    // Start is called before the first frame update
    void Awake()
    {
        textTransforms.AddFirst((RectTransform)transform.GetChild(0));
        textPreferredWidth = textTransforms.First.Value.GetComponent<TextMeshProUGUI>().preferredWidth;

        CreateClones();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachTransformAtTheEnd(RectTransform rectTransform)
    {
        float lastTransPosX = textTransforms.Last.Value.localPosition.x;

        Vector3 newPos = rectTransform.localPosition;
        newPos.x = lastTransPosX + textPreferredWidth;

        rectTransform.localPosition = newPos;
    }

    public void CreateClones()
    {
        int clones = CalculateNecessaryClones();
        
        for (int i = 0; i <= clones; i++)
        {
            RectTransform clonesTransform = Instantiate(textTransforms.First.Value, transform);
            AttachTransformAtTheEnd(clonesTransform);
            textTransforms.AddLast(clonesTransform);
        }
    }

    public int CalculateNecessaryClones()
    {
        int clones = 0;
        RectTransform maskTransform = GetComponent<RectTransform>();

        do
        {
            clones++;
        }
        while (maskTransform.rect.width / (textPreferredWidth * clones) >= 1);

        return clones;
        
    }

}
