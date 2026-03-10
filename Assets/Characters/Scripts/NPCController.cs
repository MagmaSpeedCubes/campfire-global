using System.Collections.Generic;

using UnityEngine;

using MagmaLabs;
using MagmaLabs.Animation;
using MagmaLabs.Controllers;


public class NPCController : MonoBehaviour
{
    [Tooltip("The chance your character is female")]
    [Range(0f, 1f)][SerializeField]private float genderRatio = 0.3f;
    [SerializeField]private SerializableDictionary<Item> shirts;
    [SerializeField]private SerializableDictionary<Item> maleHair;
    [SerializeField]private SerializableDictionary<Item> femaleHair;
    
    [SerializeField]private MonodirectionalCharacterAnimator shirtAnimator;
    [SerializeField]private MonodirectionalCharacterAnimator hairAnimator;
    [SerializeField]private AreaController areaController;

    public Character character;

    void Start()
    {

        if (character == null)
        {
            character = ScriptableObject.CreateInstance<Character>();
        }

        if (character.accessories == null)
        {
            character.accessories = new SerializableDictionary<Item>();
        }

        // pick gender based on ratio
        bool isMale = Random.value < genderRatio;

        // choose random hair item from appropriate dictionary
        var hairDict = isMale ? maleHair : femaleHair;
        Item chosenHair = GetRandomItem(hairDict);

        // choose random shirt item
        Item chosenShirt = GetRandomItem(shirts);

        if (chosenHair != null)
        {
            character.accessories.Set("hair", chosenHair);
            if (hairAnimator != null)
            {
                hairAnimator.SetItem(chosenHair);
            }
        }

        if (chosenShirt != null)
        {
            character.accessories.Set("shirt", chosenShirt);
            if (shirtAnimator != null)
            {
                shirtAnimator.SetItem(chosenShirt);
            }
        }
    }

    public void OnEnterRange(Collider2D other, string name){
        if(other.GetComponent<TopDown2DPlayerController>() == null){return;}

        areaController.gameObject.SetActive(false);
        List<Character> characterList = new List<Character>();
        characterList.Add(LevelManager.instance.levelRuntime.playerCharacter);
        characterList.Add(character);
        
        DialogueManager.instance.GenerateDialogue(characterList);
    }


    private static Item GetRandomItem(SerializableDictionary<Item> dictionary)
    {
        if (dictionary == null || dictionary.Count == 0)
        {
            return null;
        }

        int idx = Random.Range(0, dictionary.Count);
        return dictionary.Items[idx].value;
    }



}
