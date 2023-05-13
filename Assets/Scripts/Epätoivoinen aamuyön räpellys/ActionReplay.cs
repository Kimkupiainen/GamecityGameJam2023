using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{
    private bool replaymode;
    private int currentreplayindex;
    private Rigidbody2D rb;
    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            replaymode = !replaymode;
        }
        if (replaymode)
        {
            SetTransform(0);
            rb.isKinematic = true;
        }
        else
        {
            SetTransform(actionReplayRecords.Count - 1);
            rb.isKinematic = false;
        }
    }
    private void FixedUpdate()
    {
        if (replaymode == false)
        {
            actionReplayRecords.Add(new ActionReplayRecord { position = transform.position});
        }
        else
        {
            int nextindex = currentreplayindex + 1;
            if (nextindex < actionReplayRecords.Count)
            {
                SetTransform(nextindex);
            }
        }
    }
    private void SetTransform(int index)
    {
        currentreplayindex = index;
        ActionReplayRecord actionReplayRecord = actionReplayRecords[index];
        transform.position = actionReplayRecord.position;
    }
}
