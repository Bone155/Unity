using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecision
{
    IDecision MakeDecision();
}

public class PrintDecision : IDecision
{
    public bool branch = false;

    public PrintDecision() { }

    public PrintDecision(bool branch)
    {
        this.branch = branch;
    }

    public IDecision MakeDecision()
    {
        Debug.Log(branch ? "Yes" : "No");
        return null;
    }

    public class SimpleDecisionTreeRunner : MonoBehaviour
    {
        public IDecision decisionTreeRoot;

        void Start()
        {
            decisionTreeRoot = new PrintDecision(true);

            decisionTreeRoot.MakeDecision();
        }
    }
}