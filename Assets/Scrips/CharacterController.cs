using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    const float actionRange = 1f;
    IClickable currentInteractionTarget;
    [SerializeField] Transform currentTarget;

    void Update()
    {
        if(currentInteractionTarget != null)
        {
            UndertakeClickable();
        }
    }

    void UndertakeClickable()
    {
        bool arrived = MoveToTarget();
        if (!arrived) return;

        bool actionSuccess = currentInteractionTarget.TryAction();
    }

    bool MoveToTarget()
    {
        Vector3 moveTargetVector = currentTarget.position - transform.position;
        if (Vector3.SqrMagnitude(moveTargetVector) > actionRange)
        {
            transform.Translate(moveTargetVector.normalized * Time.deltaTime * moveSpeed);
            return false;
        }

        //ARRIVED
        return true;
    }



    //If we click on something that isnt clickable, the current target wont be changed and we will just cancel our current job
    public void ClickedObject(GameObject clickedObject)
    {
        IClickable clickable = clickedObject.GetComponent<IClickable>();
        if (clickable != null)
        {
            currentTarget = clickedObject.transform;
            currentInteractionTarget = clickable;
        } else
        {
            currentTarget = null;
            currentInteractionTarget = null;
        }
    }
}
