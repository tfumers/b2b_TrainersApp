using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{
    public abstract class Repository : MonoBehaviour
    {
        public abstract Task<bool> Initialize();

    }
    public abstract class Repository<Entity> : Repository
    {
        protected List<Entity> entities;

        public abstract Task<RepositoryResponse<Entity>> FindById(long id);

        public abstract Task<RepositoryResponse<List<Entity>>> FindAll();

        protected abstract void SaveInRepository(Entity ent);

        /*
        public async Task<bool> Create(Entity ent)
        {
            Task<ConnectorInResponse> connection = NewConnection(ent, urlDictionaries["CREATE"]);

            await connection;

            Entity obtainedEntity = ConvertToEntityFromResponse(connection.Result);

            SaveReturnedEntity(obtainedEntity);

            return (connection.Result.HttpStatus == 200);
        }

        public async Task<Entity> Read(Entity ent)
        {
            Task<ConnectorInResponse> connection = NewConnection(ent, urlDictionaries["READ"]);

            await connection;

            Entity obtainedEntity = ConvertToEntityFromResponse(connection.Result);

            SaveReturnedEntity(obtainedEntity);

            return obtainedEntity;
        }

        public async Task<bool> Update(Entity ent)
        {
            Task<ConnectorInResponse> connection = NewConnection(ent, urlDictionaries["UPDATE"]);

            await connection;

            Entity obtainedEntity = ConvertToEntityFromResponse(connection.Result);

            SaveReturnedEntity(obtainedEntity);

            return (connection.Result.HttpStatus == 200);
        }

        public async Task<bool> Delete(Entity ent)
        {
            Task<ConnectorInResponse> connection = NewConnection(ent, urlDictionaries["DELETE"]);

            await connection;

            Entity obtainedEntity = ConvertToEntityFromResponse(connection.Result);

            SaveReturnedEntity(obtainedEntity);

            return (connection.Result.HttpStatus == 200);
        }

        private bool SaveReturnedEntity(Entity entity)
        {
            if (entity != null)
            {
                entities.Add(entity);
                return true;
            }
            else
            {
                return false;
            }

        }*/
    }
}