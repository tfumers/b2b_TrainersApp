using System.Text;

public class Training
{
    private long id;

    private string difficulty;

    private string[] categories = new string[0];

    private string name;

    private string description;

    private string videoUrl;

    private TrainingVideo trainingVideo;

    private string imageUrl;

    private TrainingImage trainingImage;

    private int estTimePerRep;

    private int estCaloriesPerRep;

    public Training()
    {
        this.id = 0;
        this.difficulty = "";
        this.name = "";
        this.description = "";
        this.videoUrl = "";
        this.imageUrl = "";
        this.estTimePerRep = 0;
        this.EstCaloriesPerRep = 0;
        this.trainingVideo = null;
        this.trainingImage = null;
    }

    public Training(SimpleJSON.JSONObject json)
    {
        this.id = json["id"];
        this.difficulty = json["difficulty"];
        this.name = json["name"];
        this.description = json["description"];
        this.videoUrl = json["videoUrl"];
        this.imageUrl = json["imageUrl"];
        this.estTimePerRep = json["estTimePerRep"].AsInt;
        this.estCaloriesPerRep = json["estCaloriesPerRep"].AsInt;
    }

    public void SetCategory(string cat)
    {
        if (cat.Length > 0)
        {
            string[] catContainer = new string[1];
            catContainer[0] = cat;

            categories = catContainer;
        }
    }

    public void AddCategory(string cat)
    {
        if (cat.Length > 0)
        {
            string[] catContainer = new string[categories.Length + 1];

            for(int i = 0; i < categories.Length; i++)
            {
                if(!cat.Equals(categories[i]))
                {
                    catContainer[i] = categories[i];
                }
                else
                {
                    return;
                }
            }

            catContainer[categories.Length + 1] = cat;

            categories = catContainer;
        }
    }

    public string GetCategoriesAsString()
    {
        StringBuilder catOut = new StringBuilder();
        for(int i = 0; i< categories.Length; i++)
        {
            if (i != 0)
            {
                catOut.Append(", ");
            }

            catOut.Append(categories[0]);
        }

        return catOut.ToString();
    }

    public long Id { get => id; set => id = value; }
    public string Difficulty { get => difficulty; set => difficulty = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string VideoUrl { get => videoUrl; set => videoUrl = value; }
    public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    public int EstTimePerRep { get => estTimePerRep; set => estTimePerRep = value; }
    public int EstCaloriesPerRep { get => estCaloriesPerRep; set => estCaloriesPerRep = value; }
    public TrainingVideo TrainingVideo { get => trainingVideo; set => trainingVideo = value; }
    public TrainingImage TrainingImage { get => trainingImage; set => trainingImage = value; }
    public string[] Categories { get => categories; set => categories = value; }

}
