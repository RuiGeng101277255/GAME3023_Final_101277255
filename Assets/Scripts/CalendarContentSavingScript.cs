using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Singleton for the content saving and loading behaviours
public class CalendarContentSavingScript : MonoBehaviour
{
    private static CalendarContentSavingScript _Instance;

    //Singleton
    public static CalendarContentSavingScript Instance()
    {
        if (_Instance == null)
        {
            _Instance = new CalendarContentSavingScript();
        }

        return _Instance;
    }

    //Saves the content of calendar based on the specific year, month and the list of days within that month
    public void SaveContents(int year, int month, List<DayScript> days)
    {
        StreamWriter writer = new StreamWriter(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents-" + year + "-" + month + ".txt");
        string AllDetailsFromMonth = "";

        foreach (DayScript d in days)
        {
            AllDetailsFromMonth += (SaveLoadSignifiers.DaySeparator + d.dayNumber);

            //Special icon events
            AllDetailsFromMonth += (SaveLoadSignifiers.DetailSeparator + d.isDayHoliday);
            AllDetailsFromMonth += (SaveLoadSignifiers.DetailSeparator + d.isDayBirthday);
            AllDetailsFromMonth += (SaveLoadSignifiers.DetailSeparator + d.isDayLesson);

            //Actual details
            foreach (string s in d.DayDetails)
            {
                AllDetailsFromMonth += (SaveLoadSignifiers.DetailSeparator + s);
            }
        }

        writer.WriteLine(AllDetailsFromMonth);
        writer.Close();
    }

    public string LoadStringContentsByMonthAndYear(int year, int month)
    {
        //Loads the content of the files if it exist

        string loadedString = "";

        if (File.Exists(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents-" + year + "-" + month + ".txt"))
        {
            StreamReader reader = new StreamReader(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents-" + year + "-" + month + ".txt");
            loadedString = reader.ReadLine();
        }

        return loadedString;
    }
}

public class SaveLoadSignifiers
{
    //Signifiers used to divide the save/load data to be able to differentiate between each day and their components
    public static string DaySeparator = "[";
    public static string DetailSeparator = "]";
}