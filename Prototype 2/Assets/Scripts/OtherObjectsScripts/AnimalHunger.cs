using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHunger : MonoBehaviour
{
    public Slider hungerSlider;
    public int amountToBeFed;

    private int currentFedAmount = 0;
    private LiveAndScoreCounter liveAndScoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        hungerSlider.maxValue = amountToBeFed;
        hungerSlider.value = 0;
        hungerSlider.fillRect.gameObject.SetActive(false);

        liveAndScoreCounter = GameObject.Find(nameof(LiveAndScoreCounter)).GetComponent<LiveAndScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void feedAnimal()
    {
        currentFedAmount++;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentFedAmount;

        if(currentFedAmount >= amountToBeFed)
        {
            liveAndScoreCounter.increaseScore();
            Destroy(gameObject, 0.1f);
        }
    }
}
