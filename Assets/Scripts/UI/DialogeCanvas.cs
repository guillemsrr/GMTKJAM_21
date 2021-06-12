using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogeCanvas : MonoBehaviour
{
    [SerializeField]
    public Animator _animator;
    [SerializeField]
    public Text _textUI;

    private Coroutine _desactivationCoroutine;
    private readonly int _hashActivePara = Animator.StringToHash("Active");

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void ActivateCanvasWithText(string text)
    {
        if (_desactivationCoroutine != null)
        {
            StopCoroutine(_desactivationCoroutine);
            _desactivationCoroutine = null;
        }

        gameObject.SetActive(true);
        _animator.SetBool(_hashActivePara, true);
        _textUI.text = text;
    }

    public void ActivateCanvasWithTextWithDelay(string phraseKey, float delayTime)
    {
        if (_desactivationCoroutine != null)
        {
            StopCoroutine(_desactivationCoroutine);
            _desactivationCoroutine = null;
        }

        gameObject.SetActive(true);
        _animator.SetBool(_hashActivePara, true);

        StartCoroutine(ActivateCanvas(phraseKey, delayTime));
    }

    IEnumerator SetAnimatorParameterWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool(_hashActivePara, false);
    }

    IEnumerator ActivateCanvas(string text, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(true);
        _animator.SetBool(_hashActivePara, true);
        _textUI.text = text;
    }

    public void DeactivateCanvasWithDelay(float delay)
    {
        _desactivationCoroutine = StartCoroutine(SetAnimatorParameterWithDelay(delay));
    }
}
