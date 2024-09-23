using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LocalFilesService : Service
{
    AvatarRepository avatarRepository;

    TrainingImageRepository trainingImageRepository;

    TrainingVideoRepository trainingVideoRepository;

    // Código Viejo
    /*
    NotificationManager notificationManager;

    [SerializeField] Texture[] avatartexture = new Texture[0];

    [SerializeField] Sprite[] trainingImageSprite = new Sprite[0];

    [SerializeField] RuntimeAnimatorController[] trainingAnimatorController = new RuntimeAnimatorController[0];

    List<Avatar> avatars;

    List<TrainingImage> trainingImages;

    List<TrainingVideo> trainingVideos;

    public Transform avatarTransform;

    [SerializeField] Avatar avatarPrefab;*/

    public override void Initialize()
    {
        avatarRepository = B2BTrainer.Instance.repositoryManager.avatarRepository;
        trainingImageRepository = B2BTrainer.Instance.repositoryManager.trainingImageRepository;
        trainingVideoRepository = B2BTrainer.Instance.repositoryManager.trainingVideoRepository;

        //notificationManager = B2BTrainer.Instance.notificationManager;

        //trainingImages = InitializeTrainingImages();

        //trainingVideos = InitializeTrainingVideos();

        //avatarRepository = B2BTrainer.Instance.repositoryManager.avatarRepository;

        //Debug.Log("Iniciado");
    }

    //nuevos metodos

    public async Task<ServiceResponse<Avatar>> GetAvatarById(long id)
    {
        Avatar newAvatar = null;

        bool completed;

        string message = "";

        Task<RepositoryResponse<Avatar>> getAvatar = avatarRepository.FindById(id);

        await getAvatar;

        if (getAvatar.Result.Returned != null)
        {
            newAvatar = getAvatar.Result.Returned;
            completed = true;
        }
        else
        {
            completed = false;
            message += getAvatar.Result.Message;
        }

        return new ServiceResponse<Avatar>(completed, message, newAvatar);
    }

    public async Task<ServiceResponse<Avatar>> GetAvatarById(string stringId)
    {
        Avatar newAvatar = null;

        bool completed;

        int id = 0;

        string message = "";

        try
        {
            id = int.Parse(stringId);
        }
        catch(Exception e)
        {
            //Debug.Log(Desde Local File Service: + e.Message);
        }

        Task<RepositoryResponse<Avatar>> getAvatar = avatarRepository.FindById(id);

        await getAvatar;

        if (getAvatar.Result.Returned != null)
        {
            newAvatar = getAvatar.Result.Returned;
            Debug.Log("Avatar obtenido, con id: " + id);
            completed = true;
        }
        else
        {
            completed = false;
            Debug.Log("Avatar no obtenido");
            message += getAvatar.Result.Message;
        }

        return new ServiceResponse<Avatar>(completed, message, newAvatar);
    }

    public Task<ServiceResponse<List<Avatar>>> GetAllAvatars()
    {
        List<Avatar> avatars = new List<Avatar>();

        bool completed;

        string message = "";

        Task<RepositoryResponse<List<Avatar>>> getAvatar = avatarRepository.FindAll();

        if (getAvatar.Result.Returned != null)
        {
            avatars = getAvatar.Result.Returned;
            completed = true;
        }
        else
        {
            completed = false;
            message += getAvatar.Result.Message;
        }

        return Task.FromResult(new ServiceResponse<List<Avatar>>(completed, message, avatars));
    }

    public async Task<ServiceResponse<TrainingImage>> GetTrainignImageByName(string name)
    {
        TrainingImage trainingImage = null;

        bool completed;

        string message = "";

        Task<RepositoryResponse<TrainingImage>> getTrainingImage = trainingImageRepository.FindByName(name);

        await getTrainingImage;

        if (getTrainingImage.Result.Returned != null)
        {
            trainingImage = getTrainingImage.Result.Returned;
            message += getTrainingImage.Result.Message;
            completed = true;
        }
        else
        {
            completed = false;
            message += getTrainingImage.Result.Message;
        }

        return new ServiceResponse<TrainingImage>(completed, message, trainingImage);
    }

    public async Task<ServiceResponse<TrainingVideo>> GetTrainignVideoByName(string name)
    {
        TrainingVideo trainingVideo = null;

        bool completed;

        string message = "";

        Task<RepositoryResponse<TrainingVideo>> getTrainingVideo = trainingVideoRepository.FindByName(name);

        await getTrainingVideo;

        if (getTrainingVideo.Result.Returned != null)
        {
            trainingVideo = getTrainingVideo.Result.Returned;
            message += getTrainingVideo.Result.Message;
            completed = true;
        }
        else
        {
            completed = false;
            message += getTrainingVideo.Result.Message;
        }

        return new ServiceResponse<TrainingVideo>(completed, message, trainingVideo);
    }

    public async Task<ServiceResponse<List<TrainingVideo>>> GetAllTrainingVideos()
    {
        List<TrainingVideo> trainingVideos = new List<TrainingVideo>();

        bool completed;

        string message = "";

        Task<RepositoryResponse<List<TrainingVideo>>> getTrainingVideo = trainingVideoRepository.FindAll();

        await getTrainingVideo;

        if (getTrainingVideo.Result.Returned != null)
        {
            trainingVideos = getTrainingVideo.Result.Returned;
            message += getTrainingVideo.Result.Message;
            completed = true;
        }
        else
        {
            completed = false;
            message += getTrainingVideo.Result.Message;
        }

        return new ServiceResponse<List<TrainingVideo>>(completed, message, trainingVideos);
    }

    public async Task<ServiceResponse<List<TrainingImage>>> GetAllTrainignImages()
    {
        List<TrainingImage> trainingImages = new List<TrainingImage>();

        bool completed;

        string message = "";

        Task<RepositoryResponse<List<TrainingImage>>> getTrainingImages = trainingImageRepository.FindAll();

        await getTrainingImages;

        if (getTrainingImages.Result.Returned != null)
        {
            trainingImages = getTrainingImages.Result.Returned;
            message += getTrainingImages.Result.Message;
            completed = true;
        }
        else
        {
            completed = false;
            message += getTrainingImages.Result.Message;
        }

        return new ServiceResponse<List<TrainingImage>>(completed, message, trainingImages);
    }

    //viejos metodos
    /*
    public List<Avatar> GetAvatars()
    {
        if (avatars != null)
        {
            return avatars;
        }

        notificationManager.GenericError("No hay avatares cargados. Falló Initializer");
        return new List<Avatar>();
    }

    public TrainingImage GetTrainingImageByName(string name)
    {
        if (trainingImages == null)
        {
            notificationManager.GenericError("Fallo en el training img, no está inicializado");
        }


        foreach(TrainingImage trnImg in trainingImages)
        {
            if(trnImg.Name == name)
            {
                return trnImg;
            }
        }

        if (trainingImages.Count > 0)
        {
            return trainingImages[0];
        }
        else
        {
            notificationManager.GenericError("No existe un objeto en el trainingImage por defecto");
            return null;
        }
    }

    public TrainingVideo GetTrainingVideoByName(string name)
    {
        if (trainingImages == null)
        {
            notificationManager.GenericError("Fallo en el training img, no está inicializado");
        }


        foreach (TrainingVideo trnVideo in trainingVideos)
        {
            if (trnVideo.Name == name)
            {
                return trnVideo;
            }
        }

        if (trainingVideos.Count > 0)
        {
            return trainingVideos[0];
        }
        else
        {
            notificationManager.GenericError("No existe un objeto en el trainingImage por defecto");
            return null;
        }
    }
    /*
    private void InitializeAvatars()
    {
        if (avatarPrefab != null)
        {
            avatars = new List<Avatar>();
            for(int i = 0; i < avatartexture.Length; i++)
            {
                Avatar currAvatar = Instantiate(avatarPrefab, avatarTransform);
                currAvatar.CreateAvatar(i + 1, avatartexture[i]);
                currAvatar.gameObject.name = "Avatar" + currAvatar.Id;
                avatars.Add(currAvatar);
            }
            return;
        }

        notificationManager.GenericError("The avatar prefab does not exist!");
        return;
    }

    private List<TrainingImage> InitializeTrainingImages()
    {
        List<TrainingImage> obtTrainingImages = new List<TrainingImage>();

        for(int i = 0; i < trainingImageSprite.Length; i++)
        {
            obtTrainingImages.Add(new TrainingImage(i, trainingImageSprite[i].name, trainingImageSprite[i]));
        }

        return obtTrainingImages;
    }

    private List<TrainingVideo> InitializeTrainingVideos()
    {
        List<TrainingVideo> obtainedTrainingVideos = new List<TrainingVideo>();
        for(int i = 0; i < trainingAnimatorController.Length; i++)
        {
            obtainedTrainingVideos.Add(new TrainingVideo(i, trainingAnimatorController[i].name, trainingAnimatorController[i]));
        }

        return obtainedTrainingVideos;
    }*/
}
