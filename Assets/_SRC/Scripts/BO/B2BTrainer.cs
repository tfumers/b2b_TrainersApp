using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.TresToGames.TrainersApp.BO_SuperClasses;

public class B2BTrainer : MonoBehaviour
{
    public static B2BTrainer Instance;

    //Managers

    //WebConn
    public WebConnectionManager webConnectionManager;
    //Repositories
    public RepositoryManager repositoryManager;
    //Services
    public ServiceManager serviceManager;
    //Controller
    public ControllerManager controllerManager;
    //Component
    public ComponentManager componentManager;
    //View
    public ViewManager viewManager;
    //Error
    public NotificationManager notificationManager;

    private void Awake()
    {
        List<Manager> managers = new List<Manager>();

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Intancia principal creada");
        }

        if (repositoryManager == null)
        {
            repositoryManager = this.GetComponent<RepositoryManager>();
        }
        managers.Add(repositoryManager);

        if (serviceManager == null)
        {
            serviceManager = this.GetComponent<ServiceManager>();
        }
        managers.Add(serviceManager);

        if (controllerManager == null)
        {
            controllerManager = this.GetComponent<ControllerManager>();
        }
        managers.Add(controllerManager);

        if (webConnectionManager == null)
        {
            webConnectionManager = this.GetComponent<WebConnectionManager>();
        }
        managers.Add(webConnectionManager);

        if (notificationManager == null)
        {
            notificationManager = this.GetComponent<NotificationManager>();
        }
        managers.Add(notificationManager);

        if (viewManager == null)
        {
            viewManager = this.GetComponent<ViewManager>();
        }
        managers.Add(viewManager);

        if (componentManager == null)
        {
            componentManager = this.GetComponent<ComponentManager>();
        }
        managers.Add(componentManager);

        foreach (Manager m in managers)
        {
            m.Initialize();
        }

    }
}
