using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Question
{
    #region Fields
    [SerializeField][TextArea(5, 15)]
    private string q;
    [SerializeField]
    private string a;
    [SerializeField]
    public Sprite i;
    public string[] ABCD = new string[4];
    [SerializeField][TextArea(5, 15)]
    public string Explaination;

    #endregion

    #region Properties

    public string Q
    {
        get
        {
            return q;
        }
        set
        {
            q = value;
        }
    }

    public string A
    {
        get
        {
            return a;
        }
        set
        {
            a = value;
        }
    }

    public Sprite I
    {
        get
        {
            return i;
        }
        set
        {
            i = value;
        }
    }
    #endregion

    #region Constructor
    Question(string question, string answer)
    {
        Q = question;
        A = answer;
    }

    #endregion
}
