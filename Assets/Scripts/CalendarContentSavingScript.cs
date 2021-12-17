using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CalendarContentSavingScript : MonoBehaviour
{
    private static CalendarContentSavingScript _Instance;
    private static char DaySeparator = '[';
    private static char DetailsSeparator = ']';

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
        StreamWriter writer = new StreamWriter(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents" + year + "-" + month + "-" + ".txt");
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

    public List<DayScript> LoadContentsByMonthAndYear(int year, int month)
    {
        List<DayScript> loadedDays = null;

        if (File.Exists(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents" + year + "-" + month + "-" + ".txt"))
        {
            StreamReader reader = new StreamReader(Application.dataPath + Path.DirectorySeparatorChar + "/CalendarSaves/CalendarContents" + year + "-" + month + "-" + ".txt");
            string line = reader.ReadLine();
            string[] dayNdetails = line.Split(DaySeparator);

            for (int i = 0; i < dayNdetails.Length; i++)
            {
                if ((dayNdetails[i] != "") && (dayNdetails[i] != null))
                {
                    string[] details = dayNdetails[i].Split(DaySeparator);

                    DayScript targetDay = new DayScript();

                    targetDay.dayNumber = int.Parse(details[0]);

                    for (int d = 1; d < details.Length; d++)
                    {
                        targetDay.DayDetails.Add(details[d]);
                    }

                    loadedDays[i] = targetDay;
                }
            }
        }

        return loadedDays;
    }
}