using UnityEngine;
using DistantLands.Cozy;
using DistantLands.Cozy.Data;

public class WeatherCyclerSimple : MonoBehaviour
{
    public WeatherProfile overlayProfile;
    public WeatherProfile partlyCloudyProfile;
    public WeatherProfile chemtrailsProfile;

    private int currentIndex = 0;

    public void CycleWeather()
    {
        switch (currentIndex)
        {
            case 0:
                CozyWeather.instance.weatherModule.ecosystem.SetWeather(overlayProfile);
                break;
            case 1:
                CozyWeather.instance.weatherModule.ecosystem.SetWeather(partlyCloudyProfile);
                break;
            case 2:
                CozyWeather.instance.weatherModule.ecosystem.SetWeather(chemtrailsProfile);
                break;
        }

        currentIndex = (currentIndex + 1) % 3;
    }
}
