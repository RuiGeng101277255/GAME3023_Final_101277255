using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CalendarContentSavingScript : MonoBehaviour
{
    private static CalendarContentSavingScript _Instance;
    private static string DaySeparator = "[";
    private static string DetailsSeparator = "]";

    public static CalendarContentSavingScript Instance()
    {
        if (_Instance == null)
        {
            _Instance = new CalendarContentSavingScript();
        }

        return _Instance;
    }

    public void SetContents()
    {

    }

    public void SaveContents(int year, int month, List<DayScript> days)
    {
        StreamWriter writer = new StreamWriter(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents-" + year + "-" + month + ".txt");
        string AllDetailsFromMonth = "";

        foreach (DayScript d in days)
        {
            AllDetailsFromMonth += (DaySeparator + d.dayNumber);

            foreach (string s in d.DayDetails)
            {
                AllDetailsFromMonth += (DetailsSeparator + s);
            }
        }

        writer.WriteLine(AllDetailsFromMonth);
        writer.Close();
    }

    public string LoadStringContentsByMonthAndYear(int year, int month)
    {
        string loadedString = "";

        if (File.Exists(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents-" + year + "-" + month + ".txt"))
        {
            StreamReader reader = new StreamReader(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents-" + year + "-" + month + ".txt");
            loadedString = reader.ReadLine();
        }

        return loadedString;
    }
}