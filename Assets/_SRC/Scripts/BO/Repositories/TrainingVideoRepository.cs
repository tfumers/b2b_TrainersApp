using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainingVideoRepository : Repository<TrainingVideo>, ILoadEntitiesFromLocal<TrainingVideo>
{
    [SerializeField] RuntimeAnimatorController[] trainingAnimatorController = new RuntimeAnimatorController[0];

    public override Task<bool> Initialize()
    {
        LoadEntitiesFromLocal();

        return Task.FromResult(true);
    }

    public override Task<RepositoryResponse<List<TrainingVideo>>> FindAll()
    {
        if (entities != null)
        {
            return Task.FromResult(new RepositoryResponse<List<TrainingVideo>>("Rep. Ok", entities));
        }
        else
        {
            entities = new List<TrainingVideo>();
            return Task.FromResult(new RepositoryResponse<List<TrainingVideo>>("Rep. False", entities));
        }
    }

    public override Task<RepositoryResponse<TrainingVideo>> FindById(long id)
    {
        foreach (TrainingVideo tv in entities)
        {
            if (tv.Id == id)
            {
                return Task.FromResult(new RepositoryResponse<TrainingVideo>("REP: Found.", tv));
            }
        }

        return Task.FromResult(new RepositoryResponse<TrainingVideo>("REP: Not Found. Default Value.", entities[0]));
    }

    public List<TrainingVideo> LoadEntitiesFromLocal()
    {
        if (trainingAnimatorController != null)
        {
            for (int i = 0; i < trainingAnimatorController.Length; i++)
            {
                TrainingVideo trainingVidep = new TrainingVideo(i, trainingAnimatorController[i].name, trainingAnimatorController[i]);
                SaveInRepository(trainingVidep);
            }
        }

        return entities;
    }

    internal Task<RepositoryResponse<TrainingVideo>> FindByName(string name)
    {
        foreach (TrainingVideo tv in entities)
        {
            if (tv.Name.Equals(name))
            {
                return Task.FromResult(new RepositoryResponse<TrainingVideo>("REP: Found.", tv));
            }
        }

        return Task.FromResult(new RepositoryResponse<TrainingVideo>("REP: Not Found. Default Value.", entities[0]));
    }

    protected override void SaveInRepository(TrainingVideo ent)
    {
        if (entities == null)
        {
            entities = new List<TrainingVideo>();
        }

        if (ent != null)
        {
            entities.Add(ent);
        }
    }
}
