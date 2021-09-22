using UnityEditor;
using UnityEngine;

public class EditorWindowSingleton<T> : EditorWindow where T : EditorWindow
{
    private static T m_Instance = null;
    public static T FindFirstInstance()
    {
        var windows = (T[])Resources.FindObjectsOfTypeAll(typeof(T));
        if (windows.Length == 0)
            return null;
        return windows[0];
    }

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstInstance();
                if (m_Instance == null)
                    m_Instance = GetWindow<T>();
            }
            return m_Instance;
        }
    }
}