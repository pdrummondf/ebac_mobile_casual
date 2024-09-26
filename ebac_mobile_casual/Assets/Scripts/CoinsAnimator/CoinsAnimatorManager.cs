using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;

public class CoinsAnimatorManager : Singleton<CoinsAnimatorManager>
{
    public List<ItemCollectableCoin> itens;

    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        itens = new List<ItemCollectableCoin>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartAnimation();
        }
    }

    public void RegisterCoins(ItemCollectableCoin item)
    {
        if (!itens.Contains(item))
        {
            itens.Add(item);
            item.transform.localScale = Vector3.zero;
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(ScaleCoinByTime());
    }

    IEnumerator ScaleCoinByTime()
    {
        foreach (var item in itens)
        {
            item.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return null;

        for (int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    private void Sort()
    {
        itens = itens.OrderBy( x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
