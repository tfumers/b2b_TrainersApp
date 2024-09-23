using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingImage
{
    int id;

    string name;

    Sprite image;

    public TrainingImage(int id, string name, Sprite image)
    {
        this.id = id;
        this.name = name;
        this.image = image;
    }

    public int Id { get => id; }
    public string Name { get => name;}
    public Sprite Image { get => image;}
}
