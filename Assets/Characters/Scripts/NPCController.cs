using System.Collections.Generic;

using UnityEngine;

using MagmaLabs;
using MagmaLabs.Animation;


public class NPCController : MonoBehaviour
{
    [Tooltip("The chance your character is female")]
    [Range(0f, 1f)][SerializeField]private float genderRatio = 0.3f;
    [SerializeField]private SerializableDictionary<List<Sprite>> shirts;
    [SerializeField]private SerializableDictionary<List<Sprite>> maleHair;
    [SerializeField]private SerializableDictionary<List<Sprite>> femaleHair;

    [SerializeField]private MonodirectionalAccessoryAnimator shirtAnimator;
    [SerializeField]private MonodirectionalAccessoryAnimator hairAnimator;

    void Start()
    {
        // pick gender based on ratio
        bool isMale = Random.value < genderRatio;

        // choose random hair list from appropriate dictionary
        var hairDict = isMale ? maleHair : femaleHair;
        if (hairDict != null && hairDict.Count > 0)
        {
            int idx = Random.Range(0, hairDict.Count);
            var hairList = hairDict.Items[idx].value;
            if (hairList != null && hairList.Count > 0)
            {
                // randomly decide orientation for hair
                bool leftFacing = Random.value < 0.5f;
                hairAnimator.SetSprites(hairList.ToArray(), leftFacing);
            }
        }

        // choose random shirt list
        if (shirts != null && shirts.Count > 0)
        {
            int idx = Random.Range(0, shirts.Count);
            var shirtList = shirts.Items[idx].value;
            if (shirtList != null && shirtList.Count > 0)
            {
                bool leftFacing = Random.value < 0.5f;
                shirtAnimator.SetSprites(shirtList.ToArray(), leftFacing);
            }
        }
    }





}
