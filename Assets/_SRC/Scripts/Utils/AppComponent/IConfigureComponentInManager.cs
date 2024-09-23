using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace com.TresToGames.TrainersApp.Utils.AppComponent
{
    public interface IConfigureComponentInManager<AppComponent>
    {
        public Task<AppComponent> ConfigureComponent(AppComponent component);
    }
    public interface IConfigureInteractiveComponentInManager<InteractiveAppComponent>
    {
        public Task<InteractiveAppComponent> ConfigureComponent(InteractiveAppComponent component, UnityEvent onClickedEvent);
    }

    public interface IConfigureInteractiveComponentWithModelInManager<InteractiveAppComponent, Model>
    {
        public Task<InteractiveAppComponent> ConfigureComponent(InteractiveAppComponent component, UnityEvent<Model> onClickedEvent);
    }

    public interface ICreateComponentsInManager<AppComponent>
    {
        public Task<List<AppComponent>> CreateListOfComponents(List<AppComponent> listOfAppComponents, Transform scrollViewTransform);
    }

    public interface ICreateClickableComponentsInManager<AppComponent>
    {
        public Task<List<AppComponent>> CreateListOfComponents(List<AppComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent onClickedComponentEvent);
    }

    public interface ICreateClickableComponentWithModelInManager<AppComponent, Model>
    {
        public Task<List<AppComponent>> CreateListOfComponents(List<AppComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent<Model> onClickedComponentEvent);
    }
}

