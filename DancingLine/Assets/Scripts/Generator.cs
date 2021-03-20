using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator
{
    private GameObject _forwardBlock;
    private GameObject _leftBlock;
    private GameObject _finishBlock;
    private GameObject _coin;
    private GameObject _coinParent;

    public GameObject Parent { get; set; }
    public Vector3 CurrentPosition { get; private set; }
    public string LastBlockType { get; private set; }
    public float Offset { get; set; }
    public GameObject[] RoadMap { get; set; }

    public  Generator(GameObject forwardBlock, GameObject leftBlock, GameObject finishBlock, GameObject coin, GameObject coinParent)
    {
        _forwardBlock = forwardBlock;
        _leftBlock = leftBlock;
        _finishBlock = finishBlock;
        _coin = coin;
        _coinParent = coinParent;

        RoadMap = new GameObject[] { _forwardBlock, _leftBlock };
    }

    public void GenerateBlock()
    {
        int rnd = UnityEngine.Random.Range(0, RoadMap.Length);
        GameObject _block = RoadMap[rnd];
        string blockType = _block.name;
        Vector3 newPosition = Vector3.zero;
        if (LastBlockType == null)
        {
            GameObject.Instantiate(_forwardBlock.transform, Parent.transform);
            LastBlockType = _forwardBlock.name;
            return;
        }
        switch (blockType)
        {
            case "LeftBlock":
                if (LastBlockType == "LeftBlock")
                {
                    newPosition = CurrentPosition + new Vector3(-Offset, 0f, 0f);
                    GameObject.Instantiate(_block.transform, newPosition, Quaternion.Euler(0f, 90f, 0f), Parent.transform);
                    GenerateCoin(newPosition, blockType);
                }
                else
                {
                    newPosition = CurrentPosition + new Vector3((-Offset / 2) + 1, 0f, (Offset / 2) - 1);
                    GameObject.Instantiate(_block.transform, newPosition, Quaternion.Euler(0f, 90f, 0f), Parent.transform);
                    GenerateCoin(newPosition, blockType);
                }
                break;
            case "ForwardBlock":
                if (LastBlockType == "ForwardBlock")
                {
                    newPosition = CurrentPosition + new Vector3(0f, 0f, Offset);
                    GameObject.Instantiate(_block.transform, newPosition, Quaternion.identity, Parent.transform);
                    GenerateCoin(newPosition, blockType);
                } else
                {
                    newPosition = CurrentPosition + new Vector3(-(Offset / 2) + 1, 0f, (Offset / 2) - 1);
                    GameObject.Instantiate(_block.transform, newPosition, Quaternion.identity, Parent.transform);
                    GenerateCoin(newPosition, blockType);
                }
                break;
        }
        LastBlockType = blockType;
        CurrentPosition = newPosition;
    }

    public void GenerateCoin(Vector3 _position, string _blockType)
    {
        int rndCount = UnityEngine.Random.Range(1, 3);
        Vector3 _coinPos = Vector3.zero;
        switch (_blockType)
        {
            case "ForwardBlock":
                for (int i = 0; i < rndCount; i++)
                {
                    //Рандом смещения монетки на дороге
                    float rndPos = UnityEngine.Random.Range(-(Offset / 4) - 1, (Offset / 4) - 1);
                    _coinPos = _position + new Vector3(0f, 1f, rndPos);
                    GameObject.Instantiate(_coin.transform, _coinPos, Quaternion.Euler(0f,0f,90f), _coinParent.transform);
                    _coinPos = Vector3.zero;
                }
                break;
            case "LeftBlock":
                for (int i = 0; i < rndCount; i++)
                {
                    //Рандом смещения монетки на дороге
                    float rndPos = UnityEngine.Random.Range(-(Offset / 4) - 1, (Offset / 4) - 1);
                    _coinPos = _position + new Vector3(rndPos, 1f, 0f);
                    GameObject.Instantiate(_coin.transform, _coinPos, Quaternion.Euler(0f, 0f, 90f), _coinParent.transform);
                    _coinPos = Vector3.zero;
                }
                break;
        }
    }

    public void GenerateFinish()
    {
        Vector3 newPosition = Vector3.zero;
        float offsetFinishBlock = _finishBlock.transform.localScale.z / 2;
        if (LastBlockType == "LeftBlock")
        {
            newPosition = CurrentPosition + new Vector3(-(Offset / 2) - offsetFinishBlock, 0f, 0f);
            GameObject.Instantiate(_finishBlock.transform, newPosition, Quaternion.Euler(0f, 90f, 0f), Parent.transform);
        }
        else
        {
            newPosition = CurrentPosition + new Vector3(0f, 0f, Offset - offsetFinishBlock);
            GameObject.Instantiate(_finishBlock.transform, newPosition, Quaternion.Euler(0f, 90f, 0f), Parent.transform);
        }
    }
}
