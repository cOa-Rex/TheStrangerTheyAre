using System;
using System.Collections;
using UnityEngine;

namespace TheStrangerTheyAre;
public class SealSocket : OWItemSocket
{
    [SerializeField]
    public GameObject activeObjects;
    [SerializeField]
    public SealID sealSocketID;
    [SerializeField]
    public GameObject inactiveObjects;

    private bool _itemPlaced;
    public bool itemPlaced => _itemPlaced;

    public override void Awake()
    {
        base.Awake();
        _acceptableType = TheStrangerTheyAre.SealItemType;
        activeObjects.SetActive(false);
        inactiveObjects.SetActive(true);
    }

    public override bool PlaceIntoSocket(OWItem item)
    {
        if (base.PlaceIntoSocket(item))
        {
            item.transform.localScale = item.transform.localScale*2.5f;
            if (item.GetComponent<SealItem>().sealID == sealSocketID)
            {
                activeObjects.SetActive(true);
                inactiveObjects.SetActive(false);
                _itemPlaced = true;
                StartCoroutine(PlayAnimationAndProceed("ProjectionStart", false));
                return true;
            }
        }
        _itemPlaced = false;
        return false;
    }

    private IEnumerator PlayAnimationAndProceed(string animationName, bool isEndAnim)
    {
        // Get the animator component and play the animation
        Animator animator = activeObjects.GetComponent<Animator>();
        animator.Play(animationName, 0);

        // Wait until the animation is finished
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            !animator.IsInTransition(0)
        );

        // Code to execute after the animation finishes
        OnAnimationComplete(isEndAnim);
    }

    public void OnAnimationComplete(bool isEndAnim)
    {
        if (isEndAnim) {
            // should disable when it ends
        }
        else {
            activeObjects.GetComponent<Animator>().Play("ProjectionLoop", 0);
        }
    }

    public override OWItem RemoveFromSocket()
    {
        OWItem oWItem = base.RemoveFromSocket();

        _itemPlaced = false;
        if (activeObjects.activeSelf)
        {
            StartCoroutine(PlayAnimationAndProceed("ProjectionEnd", true));
            activeObjects.SetActive(false);
            inactiveObjects.SetActive(true);
        }
        return oWItem;
    }
}
