using com.TresToGames.TrainersApp.BO_SuperClasses;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AvatarRepository : Repository<Avatar>, ILoadEntitiesFromLocal<Avatar>
{
    [SerializeField] Texture[] avatarTextures;

    public override Task<bool> Initialize()
    {
        LoadEntitiesFromLocal();

        return Task.FromResult(true);
    }

    public List<Avatar> LoadEntitiesFromLocal()
    {
        if (avatarTextures != null)
        {
            for (int i = 0; i < avatarTextures.Length; i++)
            {
                Avatar currAvatar = new Avatar(i + 1, avatarTextures[i]);
                SaveInRepository(currAvatar);
            }
        }

        return entities;
    }

    public override Task<RepositoryResponse<List<Avatar>>> FindAll()
    {
        if (entities != null)
        {
            return Task.FromResult(new RepositoryResponse<List<Avatar>>("Rep. Ok", entities));
        }
        else
        {
            entities = new List<Avatar>();
            return Task.FromResult(new RepositoryResponse<List<Avatar>>("Rep. False", entities));
        }
    }

    public override Task<RepositoryResponse<Avatar>> FindById(long id)
    {
        foreach (Avatar av in entities)
        {
            if (av.Id == id)
            {
                return Task.FromResult(new RepositoryResponse<Avatar>("REP: Found.", av));
            }
        }

        Debug.Log("Avatar not found");
        return Task.FromResult(new RepositoryResponse<Avatar>("REP: Not Found. Default Value.", entities[0]));
    }

    protected override void SaveInRepository(Avatar ent)
    {
        if (entities == null)
        {
            entities = new List<Avatar>();
        }

        if (ent != null)
        {
            entities.Add(ent);
        }
    }
}
