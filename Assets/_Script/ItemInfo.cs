using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using System;
using NUnit.Framework;

public class ItemInfo : MonoBehaviour
{
    public GameManager game;
    public GameObject notEnough;
    private int[] whiteSpace = new int[5];
    public void SetString(string costBase = "null 4 2 66 egg 8")
    {
        whiteSpace[0] = costBase.IndexOf(" ", 0);

        whiteSpace[1] = costBase.IndexOf(" ", whiteSpace[0] + 1);

        whiteSpace[2] = costBase.IndexOf(" ", whiteSpace[1] + 1);

        whiteSpace[3] = costBase.IndexOf(" ", whiteSpace[2] + 1);

        whiteSpace[4] = costBase.IndexOf(" ", whiteSpace[3] + 1);


        string building = costBase.Substring(0, whiteSpace[0]);
        string wood = costBase.Substring(whiteSpace[0], whiteSpace[1] - whiteSpace[0]);
        string stone = costBase.Substring(whiteSpace[1], whiteSpace[2] - whiteSpace[1]);
        string iron = costBase.Substring(whiteSpace[2], whiteSpace[3] - whiteSpace[2]);
        string food = costBase.Substring(whiteSpace[3], whiteSpace[4] - whiteSpace[3]);
        string workers = costBase.Substring(whiteSpace[4]);
        gameObject.GetComponent<TMP_Text>().text = $"Building type: {building}\r\n\r\nWood: {wood}\tStone: {stone}\tIron: {iron}\r\nFood: {food}\tWorkers: {workers}";

        if (building != "House")
        {
            if (!game.BuildCheck(Convert.ToUInt32(wood), Convert.ToUInt32(stone), Convert.ToUInt32(iron), Convert.ToUInt32(food), Convert.ToUInt32(workers)))
            {
                notEnough.SetActive(true);
            }
            else
            {
                notEnough.SetActive(false);
            }
        }

        else
        {
            Debug.Log("Checking for house");
            if (!game.BuildCheck(Convert.ToUInt32(wood), Convert.ToUInt32(stone), Convert.ToUInt32(iron), Convert.ToUInt32(food), 0))
            {
                notEnough.SetActive(true);
            }
            else
            {
                notEnough.SetActive(false);
            }
        }

    }
}
