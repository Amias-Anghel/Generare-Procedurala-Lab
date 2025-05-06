using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RarityStarsEffect : MonoBehaviour
{
    [SerializeField] private List<Image> stars;
    private bool animate = false;
    [SerializeField] private Color mythicalColor;
    [SerializeField] private Color normalColor;

    private float frameCount = 0, frameRate = 3, animationSpeed = 30f;
    private int index = 0;


    void Update()
    {
        if (animate) {
            if (frameCount >= frameRate) {
                SwitchStar();
                index++;
                if (index > 4) {
                    index = 0;
                }
                frameCount = 0;
            }
            frameCount += Time.deltaTime * animationSpeed;
        }
    }

    public void SetAnimation(bool animate) {
        this.animate = animate;

        if (!false) {
            index = 0;
            foreach(Image star in stars) {
                star.color = normalColor;
            }
        }
    }

    private void SwitchStar() {
        stars[index].color = (stars[index].color == normalColor)? mythicalColor : normalColor; 
    }
}
