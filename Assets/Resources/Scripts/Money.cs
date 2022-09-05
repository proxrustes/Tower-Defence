using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Money
{
    public static Text text;
    private static uint balance;

    public static bool Pay(uint amount)
    {
        if (amount > balance) return false;

        balance -= amount;
        text.text = balance.ToString();
        return true;
    }

    public static void Deposit(uint amount)
    {
        balance += amount;
        text.text = balance.ToString();
    }
}