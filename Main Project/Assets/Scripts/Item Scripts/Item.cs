using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public List<BaseStat> Stats {  get; set; }
    public string ObjectSlug { get; set; }
    public string Description { get; set; }
    public string ActionName { get; set; }
    public string ItemName { get; set; }
    public bool ItemModifier { get; set; }

    public Item(List<BaseStat> stats, string objectSlug)
    {
        this.Stats = stats;
        this.ObjectSlug = objectSlug;
    }

    public Item(List<BaseStat> stats, string objectSlug, string description, string actionName, string itemName, bool itemModifier)
    {
        this.Stats = stats;
        this.ObjectSlug = objectSlug;
        this.Description = description;
        this.ActionName = actionName;
        this.ItemName = itemName;
        this.ItemModifier = itemModifier;
    }
}
