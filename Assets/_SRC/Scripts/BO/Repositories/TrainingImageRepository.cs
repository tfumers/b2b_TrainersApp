using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainingImageRepository : Repository<TrainingImage>, ILoadEntitiesFromLocal<TrainingImage>
{
    [SerializeField] Sprite[] trainingImageSprite = new Sprite[0];

    public override Task<bool> Initialize()
    {
        LoadEntitiesFromLocal();

        return Task.FromResult(true);
    }

    public List<TrainingImage> LoadEntitiesFromLocal()
    {
        if (trainingImageSprite != null)
        {
            for (int i = 0; i < trainingImageSprite.Length; i++)
            {
                TrainingImage newTrainingImg = new TrainingImage(i, trainingImageSprite[i].name, trainingImageSprite[i]);
                SaveInRepository(newTrainingImg);
            }
        }

        return entities;
    }

    public override Task<RepositoryResponse<List<TrainingImage>>> FindAll()
    {
        if (entities != null)
        {
            return Task.FromResult(new RepositoryResponse<List<TrainingImage>>("Rep. Ok", entities));
        }
        else
        {
            entities = new List<TrainingImage>();
            return Task.FromResult(new RepositoryResponse<List<TrainingImage>>("Rep. False", entities));
        }
    }

    public override Task<RepositoryResponse<TrainingImage>> FindById(long id)
    {
        foreach (TrainingImage ti in entities)
        {
            if (ti.Id == id)
            {
                return Task.FromResult(new RepositoryResponse<TrainingImage>("REP: Found.", ti));
            }
        }

        return Task.FromResult(new RepositoryResponse<TrainingImage>("REP: Not Found. Default Value.", entities[0]));
    }



    internal Task<RepositoryResponse<TrainingImage>> FindByName(string name)
    {
        foreach (TrainingImage ti in entities)
        {
            if (ti.Name.Equals(name))
            {
                return Task.FromResult(new RepositoryResponse<TrainingImage>("REP: Found.", ti));
            }
        }

        return Task.FromResult(new RepositoryResponse<TrainingImage>("REP: Not Found. Default Value.", entities[0]));
    }

    protected override void SaveInRepository(TrainingImage ent)
    {
        if (entities == null)
        {
            entities = new List<TrainingImage>();
        }

        if (ent != null)
        {
            entities.Add(ent);
        }
    }
}
