using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _forwardBlock;
    [SerializeField] private GameObject _leftBlock;
    [SerializeField] private GameObject _finishBlock;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _coinParent;
    [SerializeField] private GameObject _parentGOForRoadBlocks;
    [SerializeField] private int _startBlocksCount;
    [SerializeField] private GameObject _coinCountTextGameObject;
    [SerializeField] private int _coinCountToWin;

    private Generator _generator;
    private int _coinCount;
    private Text _cointCountText;


    private void Awake()
    {
        _cointCountText = _coinCountTextGameObject.GetComponent<Text>();
        _coinCount = 0;
        _generator = new Generator(_forwardBlock, _leftBlock, _finishBlock, _coin, _coinParent);
        _generator.Offset = _forwardBlock.transform.localScale.z;
        _generator.Parent = _parentGOForRoadBlocks;
        for (int block = 0; block < _startBlocksCount; block++)
        {
            int rnd = UnityEngine.Random.Range(0, _generator.RoadMap.Length);
            _generator.GenerateBlock();
        }
        SimpleCoin.OnGetCoin += _generator.GenerateBlock;
        SimpleCoin.OnGetCoin += IsFinished;
        SimpleCoin.OnGetCoin += AddCoin;
    }

    private void AddCoin()
    {
        _coinCount += 1;
        _cointCountText.text = $"Coins: {_coinCount}";
    }

    private void IsFinished()
    {
        if (_coinCount >= _coinCountToWin)
        {
            _generator.GenerateFinish();
            Destroy(GameObject.Find("Coins"));
        }
    }
}
