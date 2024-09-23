using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar
{
    long id;
    [SerializeField] Texture texture;
    bool created = false;

    public long Id { get => id;}
    public Texture Image { get => texture; }
    public bool Created { get => created;}

    public Avatar()
    {
        this.id = 0;
    }

    public Avatar(long id, Texture avatarTexture)
    {
        this.id = id;
        this.texture = avatarTexture;
        this.created = true;
    }
}
