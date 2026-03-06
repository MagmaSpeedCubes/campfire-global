using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MagmaLabs;
using MagmaLabs.UI;
public class DialogueController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI mainTitleBlock;
    [SerializeField] private TMPEnhanced mainTextBlock;
    
    [SerializeField] private DialogueBlock activeBlock;

    [SerializeField] private TextMeshProUGUI[] advanceButtonText;

    private int index = 0;

    void Start(){
        AdvanceLine();//for testing
    }

    public void BeginDialogue(DialogueBlock newBlock)
    {
        activeBlock = newBlock;
        index = 0;
        AdvanceLine();
    }

    public void AdvanceLine()
    {
        Debug.Log("Advancing dialogue line");
        // display next line of the current dialogue block
        if (activeBlock == null || activeBlock.dialogue == null || activeBlock.dialogue.Count == 0)
            return;

        // show current line
        mainTextBlock.SetHiddenText(activeBlock.dialogue[index].message);
        StartCoroutine(mainTextBlock.WriteOnNormalized(1, 1));
        //Debug.Log("Message: " + activeBlock.dialogue[index].message);
        
        index++;

        // if we've reached or passed the last line, show continuation buttons
        if (index >= activeBlock.dialogue.Count)
        {
            ShowContinuationOptions();
        }
    }

    private void ShowContinuationOptions()
    {
        // clear all button texts first
        for (int i = 0; i < advanceButtonText.Length; i++)
        {
            advanceButtonText[i].text = string.Empty;
            advanceButtonText[i].transform.parent.gameObject.SetActive(false);
        }

        if (activeBlock == null || activeBlock.continuations == null || activeBlock.continuations.Count == 0)
        {
            // no continuations: conversation end
            EndDialogue();
            return;
        }

        int iIndex = 0;
        foreach (var kvp in activeBlock.continuations.Items)
        {
            if (iIndex >= advanceButtonText.Length){
                Debug.LogError("DialogueController does not have enough buttons to display all options");
                break; // not enough buttons
            }

            advanceButtonText[iIndex].text = kvp.key;
            advanceButtonText[iIndex].transform.parent.gameObject.SetActive(true);
            iIndex++;
        }
    }

    private void EndDialogue()
    {
        // simple placeholder: disable all buttons and optionally clear text
        for (int i = 0; i < advanceButtonText.Length; i++)
        {
            advanceButtonText[i].transform.parent.gameObject.SetActive(false);
        }
        // could trigger an event or callback here
        Debug.Log("Dialogue ended");
    }

    public void AdvanceDialogueBlock(int button)
    {
        if (activeBlock == null || activeBlock.continuations == null)
            return;

        if (button < 0 || button >= activeBlock.continuations.Count)
            return;

        // fetch block by index since dictionary
        var continuationList = activeBlock.continuations.Items;
        if (button >= continuationList.Count)
            return;

        DialogueBlock next = continuationList[button].value;
        if (next != null)
        {
            BeginDialogue(next);
            // immediately advance to first line
            AdvanceLine();
        }
        else
        {
            EndDialogue();
        }
    }


}
