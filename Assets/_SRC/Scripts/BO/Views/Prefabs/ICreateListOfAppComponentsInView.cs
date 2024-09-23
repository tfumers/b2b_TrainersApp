using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public interface ICreateListOfAppComponentsInView<AppComponent>
{
    public Task<List<AppComponent>> GetAppComponents(List<AppComponent> components, Transform scrollViewTransform);
}

public interface ICreateListOfInteractiveComponentsInView<InteractiveAppComponent>
{
    public Task<List<InteractiveAppComponent>> GetAppComponents(List<InteractiveAppComponent> components, Transform scrollViewTransform, UnityEvent OnClickedComponentEvent);
}

public interface IConfigureAppComponentInView<AppComponent>
{
    public Task<AppComponent> ConfigureAppComponent(AppComponent component);
}

public interface IConfigureInteractiveComponentInView<InteractiveAppComponent>
{
    public Task<InteractiveAppComponent> ConfigureInteractiveAppComponent(InteractiveAppComponent component, UnityEvent OnClickedComponentEvent);
}
