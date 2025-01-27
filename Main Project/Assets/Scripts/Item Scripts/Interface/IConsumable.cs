using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable
{
    public void Consume();

    public void Consume(Stats stats);
}
