using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialSectionGroup tutorialSectionsGroup;

    private void Start()
    {
        if (tutorialSectionsGroup == null
            || tutorialSectionsGroup.TutorialIsCompleted)
            
            gameObject.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            TutorialManager.Instance.StartTutorial(tutorialSectionsGroup.Clone());
        }
    }
}
