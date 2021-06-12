using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogeCanvas : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public Text textUI;

    private Coroutine m_DeactivationCoroutine;
    private readonly int m_HashActivePara = Animator.StringToHash("Active");

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void ActivateCanvasWithText(string text)
    {
        if (m_DeactivationCoroutine != null)
        {
            StopCoroutine(m_DeactivationCoroutine);
            m_DeactivationCoroutine = null;
        }

        gameObject.SetActive(true);
        animator.SetBool(m_HashActivePara, true);
        textUI.text = text;
    }

    public void ActivateCanvasWithTextWithDelay(string phraseKey, float delayTime)
    {
        if (m_DeactivationCoroutine != null)
        {
            StopCoroutine(m_DeactivationCoroutine);
            m_DeactivationCoroutine = null;
        }

        gameObject.SetActive(true);
        animator.SetBool(m_HashActivePara, true);

        StartCoroutine(ActivateCanvas(phraseKey, delayTime));
    }

    IEnumerator SetAnimatorParameterWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(m_HashActivePara, false);
    }

    IEnumerator ActivateCanvas(string text, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(true);
        animator.SetBool(m_HashActivePara, true);
        textUI.text = text;
    }

    public void DeactivateCanvasWithDelay(float delay)
    {
        m_DeactivationCoroutine = StartCoroutine(SetAnimatorParameterWithDelay(delay));
    }
}
