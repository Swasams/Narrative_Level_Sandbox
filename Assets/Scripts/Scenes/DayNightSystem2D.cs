using UnityEngine;


/*
- Creator:    Two TV Games (@gallighanmaker)
- Script:     Day And Night 2D System
- Unity:      2019 LTS Version
- Email:      leandrovieira92@gmail.com
- Github:     https://github.com/leandrovieiraa
*/

public enum DayCycles // Enum with day and night cycles, you can change or modify with whatever you want
{
    Sunrise = 0,
    Day = 1,
    Sunset = 2,
    Night = 3,
    Midnight = 4
}

public class DayNightSystem2D : MonoBehaviour
{
    public static DayNightSystem2D Instance;

    [Header("Controllers")]

    [Tooltip("Global light 2D component, we need to use this object to place light in all map objects")]
    [SerializeField] private GameObject globalLightSource;
    private UnityEngine.Rendering.Universal.Light2D globalLight; // global light

    [Tooltip("This is a current cycle time, you can change for private float but we keep public only for debug")]
    public float cycleCurrentTime = 0; // current cycle time
    
    [Tooltip("This is a cycle max time in seconds, if current time reach this value we change the state of the day and night cyles")]
    public float cycleMaxTime = 60; // duration of cycle

    [Tooltip("Enum with multiple day cycles to change over time, you can add more types and modify whatever you want to fits on your project")]
    public DayCycles dayCycle = DayCycles.Sunrise; // default cycle

    [Header("Cycle Colors")]
    
    [Tooltip("Sunrise color, you can adjust based on best color for this cycle")]
    public Color sunrise; // Eg: 6:00 at 10:00
    
    [Tooltip("(Mid) Day color, you can adjust based on best color for this cycle")]
    public Color day; // Eg: 10:00 at 16:00
    
    [Tooltip("Sunset color, you can adjust based on best color for this cycle")]
    public Color sunset; // Eg: 16:00 20:00
    
    [Tooltip("Night color, you can adjust based on best color for this cycle")]
    public Color night; // Eg: 20:00 at 00:00
    
    [Tooltip("Midnight color, you can adjust based on best color for this cycle")]
    public Color midnight; // Eg: 00:00 at 06:00

    [Header("Objects")]
    [Tooltip("Objects to turn on and off based on day night cycles, you can use this example for create some custom stuffs")]
    [SerializeField] private GameObject[] lights; // enable/disable in day/night states

    [Header("Day Cycle Settings")]
    [Tooltip("If you want to enable/disable the day/night cycle timer use this bool")]
    public bool cycleTimer = true; // enable/disable cycle timer
    [Tooltip("If you want to enable/disable the lights regardless of the day/night cycle use this bool")]
    public bool lightsStatus; // enable/disable cycle timer
    public bool isChase; // chase state
    public bool isDark; // basement state
    float percent;

    [Header("Day Cycle Persistence")]
    [Tooltip("If you want to enable/disable DontDestroyOnLoad use this bool")]
    [SerializeField] private bool persistenceOn; // enable/disable DontDestroyOnLoad

    private void Awake()
    {
        if (persistenceOn)
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        FindGlobalLight();
        FindMapLights();
    }

    void Start() 
    {
        dayCycle = DayCycles.Sunrise; // start with sunrise state
        globalLight.color = sunrise; // start global color at sunrise
    }

     void Update()
     {
        // Toggle Cycle Timer
        if (cycleTimer)
        {
            cycleCurrentTime += Time.deltaTime; // Update cycle time
        }

        // Check if cycle time reach cycle duration time
        if (cycleCurrentTime >= cycleMaxTime) 
        {
            cycleCurrentTime = 0; // back to 0 (restarting cycle time)
            dayCycle++; // change cycle state
        }

        // If reach final state we back to sunrise (Enum id 0)
        if(dayCycle > DayCycles.Midnight)
            dayCycle = 0;

        // percent, it's an value between current and max time to make a color lerp smooth
        percent = cycleCurrentTime / cycleMaxTime;

        if (!lightsStatus)
        {
            OutsideLighting();
        }
        else
        {
            if (isChase)
            {
                ChaseLighting();
            }
            else if (isDark)
            {
                DarkLighting();
            }
            else
            {
                InsideLighting();
            }
        }
     }

     public void ControlLightMaps(bool status)
     {
         // loop in light array of objects to enable/disable
         if(lights.Length > 0)
            foreach(GameObject _light in lights)
                _light.gameObject.SetActive(status);
     }

    public void FindGlobalLight()
    {
        globalLightSource = GameObject.FindWithTag("Global");
        globalLight = globalLightSource.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    public void FindMapLights()
    {
        lights = GameObject.FindGameObjectsWithTag("Lights");

        if (lights.Length == 0)
        {
            Debug.Log("No GameObjects are tagged as 'Lights'");
        }
    }

    public void CycleSetting()
    {
        // Sunrise state (you can do a lot of stuff based on every cycle state, like enable animals only in sunrise )
        if (dayCycle == DayCycles.Sunrise)
        {
            if (!lightsStatus)
            {
                ControlLightMaps(false); // disable map light (keep enable only at night)
            }

            globalLight.color = Color.Lerp(sunrise, day, percent);
        }

        // Mid Day state
        if (dayCycle == DayCycles.Day)
            globalLight.color = Color.Lerp(day, sunset, percent);

        // Sunset state
        if (dayCycle == DayCycles.Sunset)
            globalLight.color = Color.Lerp(sunset, night, percent);

        // Night state
        if (dayCycle == DayCycles.Night)
        {
            if (!lightsStatus)
            {
                ControlLightMaps(true); // enable map lights (disable only in day states)
            }

            globalLight.color = Color.Lerp(night, midnight, percent);
        }

        // Midnight state
        if (dayCycle == DayCycles.Midnight)
            globalLight.color = Color.Lerp(midnight, day, percent);
    }

    public void OutsideLighting()
    {
        globalLight.intensity = 1f;
        CycleSetting();
    }

    public void InsideLighting()
    {
        globalLight.color = Color.white;
        globalLight.intensity = .8f;
    }

    public void ChaseLighting()
    {
        isChase = true;
        globalLight.color = new Color (0.8301887f, 0.2459238f, 0.2606038f);
        globalLight.intensity = .5f;
    }

    public void DarkLighting()
    {
        isDark = true;
        globalLight.color = new Color (0.1f, 0.1f, 0.1f);
        globalLight.intensity = 0.7f;
    }
}
